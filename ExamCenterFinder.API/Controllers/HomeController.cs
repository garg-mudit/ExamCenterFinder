using System.ComponentModel.DataAnnotations;

using ExamCenterFinder.API.BusinessLogic.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ExamCenterFinder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //This is named as home controller as endpoint is supposed to be exposed at /availability as per requirement
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "availability")]
        public async Task<IActionResult> GetExamCenterSlotAvailability(string zipCode, int examDurationInMinutes, int maxDistanceFromCenterInMiles)
        {
            //Note: This can be replaced via global error handling
            try
            {
                var response = await _mediator.Send(new GetExamCenterSlotAvailibilityQuery(zipCode, examDurationInMinutes, maxDistanceFromCenterInMiles));

                return Ok(response);
            }
            catch (ValidationException ex)
            {
                //do error logging...

                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                //do error logging...

                return StatusCode(500, "Internal Server Error occurred.");
            }
        }
    }
}
