using System.ComponentModel.DataAnnotations;

using ExamCenterFinder.API.Data.Context;
using ExamCenterFinder.API.Objects;
using ExamCenterFinder.API.Objects.Dtos;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ExamCenterFinder.API.BusinessLogic.Queries
{
    public class GetExamCenterSlotAvailibilityQuery : IRequest<List<AvailabilityDto>>
    {
        private string _zipCode;
        private int _examDurationInMinutes;
        private int _maxDistanceFromCenterInMiles;

        public GetExamCenterSlotAvailibilityQuery(string zipCode, int examDurationInMinutes, int maxDistanceFromCenterInMiles)
        {
            _zipCode = zipCode;
            _examDurationInMinutes = examDurationInMinutes;
            _maxDistanceFromCenterInMiles = maxDistanceFromCenterInMiles;
        }

        public class Handler : IRequestHandler<GetExamCenterSlotAvailibilityQuery, List<AvailabilityDto>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<AvailabilityDto>> Handle(GetExamCenterSlotAvailibilityQuery request, CancellationToken cancellationToken)
            {
                if (!ZipCodeHelper.ZipCodes.ContainsKey(request._zipCode))
                {
                    throw new ValidationException("Invalid zip code. Please enter a valid zip code.");
                }

                //Making an Assumption about exam duration limits
                if (request._examDurationInMinutes > 360 || request._examDurationInMinutes < 60)
                {
                    throw new ValidationException("Invalid exam duration. Please enter a duration between 60 and 360 minutes.");
                }

                //Making an Assumption about maxDistanceFromCenterInMiles limits
                if (request._maxDistanceFromCenterInMiles < 1)
                {
                    throw new ValidationException("Invalid max distance. Minimum acceptable value for max distance from an exam center is 1 mile.");
                }

                var zipCodeCenterPoint = ZipCodeHelper.ZipCodes[request._zipCode];


                var minimumPossibleTestStartTime = DateTime.UtcNow.AddMinutes(5);

                var examCentersData = await GetAvailableExamCenters(zipCodeCenterPoint, request._examDurationInMinutes, request._maxDistanceFromCenterInMiles, minimumPossibleTestStartTime);
                var response = examCentersData.Select(ec => AvailabilityDto.CreateDto(ec, minimumPossibleTestStartTime)).ToList();

                return response;
            }

            private async Task<List<ExamCenterDto>> GetAvailableExamCenters(ZipCodeCenterPoint zipCodeCenterPoint, int examDurationInMinutes, int maxDistanceFromCenterInMiles, DateTime minimumPossibleTestStartTime)
            {
                return await _context.ExamCenters
                .Where(ec =>
                        _context.CalculateDistanceMiles(ec.Latitude, ec.Longitude, zipCodeCenterPoint.Latitude, zipCodeCenterPoint.Longitude)
                                                < maxDistanceFromCenterInMiles &&
                        ec.ExamCenterSlots
                            .Any(ecs =>
                                !ecs.IsFilled &&
                                ecs.EndTime.AddMinutes(-examDurationInMinutes) >= ecs.StartTime &&
                                ecs.EndTime.AddMinutes(-examDurationInMinutes) >= minimumPossibleTestStartTime
                            )
                    )
                    .Select(ec => new ExamCenterDto
                    {
                        ExamCenterName = ec.Name,
                        Address = ec.Address,
                        DistanceMiles = _context.CalculateDistanceMiles(ec.Latitude, ec.Longitude, zipCodeCenterPoint.Latitude, zipCodeCenterPoint.Longitude),
                        Latitude = ec.Latitude,
                        Longitude = ec.Longitude,
                        Seats = ec.MaximumSeatingCapacity,
                        SlotDetails = ec.ExamCenterSlots
                            .Where(ecs =>
                                !ecs.IsFilled &&
                                ecs.EndTime.AddMinutes(-examDurationInMinutes) >= ecs.StartTime &&
                                ecs.EndTime.AddMinutes(-examDurationInMinutes) >= minimumPossibleTestStartTime
                            )
                            .OrderBy(ecs => ecs.StartTime)
                            .Select(ecs => new SlotDetailsDto
                            {
                                AvailabilityId = ecs.SlotId,
                                StartTime = ecs.StartTime,
                            })
                            .First(),
                    }).ToListAsync();
            }
        }
    }
}
