using Application.UseCases.Trips.GetAll;
using Application.UseCases.Trips.Register;
using Communication.Requests;
using Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        try
        {
            var useCase = new RegisterTripUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
        catch (JourneyException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ResourceErrorMessages.UNKNOWN_ERROR);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripsUseCase();

        var trips = useCase.Execute();

        return Ok(trips);
    }
}
