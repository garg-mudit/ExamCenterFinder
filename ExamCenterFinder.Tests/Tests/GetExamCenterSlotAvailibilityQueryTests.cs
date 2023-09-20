using System.ComponentModel.DataAnnotations;

using ExamCenterFinder.API.BusinessLogic.Queries;

using NUnit.Framework;

namespace ExamCenterFinder.Tests.Tests
{
    [TestFixture]
    internal class GetExamCenterSlotAvailibilityQueryTests : BaseTest
    {
        public GetExamCenterSlotAvailibilityQueryTests()
        {
        }

        [Test]
        public async Task GetAvailableExamCenters_InvalidZipCode_ReturnsBadRequest()
        {
            // Arrange
            var zipCode = "99999"; // Invalid ZIP code
            var examDurationInMinutes = 120;
            var maxDistanceFromCenterInMiles = 10;

            // Act
            var handler = new GetExamCenterSlotAvailibilityQuery.Handler(DbContext);
            var request = new GetExamCenterSlotAvailibilityQuery(zipCode, examDurationInMinutes, maxDistanceFromCenterInMiles);

            object result;
            try
            {
                result = await handler.Handle(request, default);
            }
            catch (ValidationException ex)
            {
                result = ex;
            }
            catch (Exception ex)
            {
                result = ex;
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result is ValidationException && ((ValidationException)result).Message == "Invalid zip code. Please enter a valid zip code.");
        }

        [Test]
        public async Task GetAvailableExamCenters_InvalidExamDurationInMinutes_ReturnsBadRequest()
        {
            // Arrange
            var zipCode = "11111";
            var examDurationInMinutes = 400; // Invalid Exam Duration In Minutes
            var maxDistanceFromCenterInMiles = 10;

            // Act
            var handler = new GetExamCenterSlotAvailibilityQuery.Handler(DbContext);
            var request = new GetExamCenterSlotAvailibilityQuery(zipCode, examDurationInMinutes, maxDistanceFromCenterInMiles);

            object result;
            try
            {
                result = await handler.Handle(request, default);
            }
            catch (ValidationException ex)
            {
                result = ex;
            }
            catch (Exception ex)
            {
                result = ex;
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result is ValidationException && ((ValidationException)result).Message == "Invalid exam duration. Please enter a duration between 60 and 360 minutes.");
        }

        [Test]
        public async Task GetAvailableExamCenters_InvalidMaxDistanceFromCenterInMiles_ReturnsBadRequest()
        {
            // Arrange
            var zipCode = "11111";
            var examDurationInMinutes = 120;
            var maxDistanceFromCenterInMiles = -1; // Invalid Max Distance From Center In Miles

            // Act
            var handler = new GetExamCenterSlotAvailibilityQuery.Handler(DbContext);
            var request = new GetExamCenterSlotAvailibilityQuery(zipCode, examDurationInMinutes, maxDistanceFromCenterInMiles);

            object result;
            try
            {
                result = await handler.Handle(request, default);
            }
            catch (ValidationException ex)
            {
                result = ex;
            }
            catch (Exception ex)
            {
                result = ex;
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result is ValidationException && ((ValidationException)result).Message == "Invalid max distance. Minimum acceptable value for max distance from an exam center is 1 mile.");
        }

    }
}
