namespace ExamCenterFinder.API.Objects.Dtos
{
    public class AvailabilityDto
    {
        public int AvailabilityId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime StartTime { get; set; }
        public int Seats { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanceMiles { get; set; }

        public static AvailabilityDto CreateDto(ExamCenterDto ec, DateTime minimumPossibleTestStartTime)
        {

            var startTime = minimumPossibleTestStartTime > ec.SlotDetails.StartTime ? minimumPossibleTestStartTime : ec.SlotDetails.StartTime;

            return new AvailabilityDto()
            {

                AvailabilityId = ec.SlotDetails.AvailabilityId,
                Name = ec.ExamCenterName,
                Address = ec.Address,
                DistanceMiles = ec.DistanceMiles,
                Latitude = ec.Latitude,
                Longitude = ec.Longitude,
                Seats = ec.Seats,
                StartTime = startTime
            };
        }
    }
}
