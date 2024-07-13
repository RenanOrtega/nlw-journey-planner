using Application.UseCases.Activities.Complete;
using Application.UseCases.Activities.Delete;
using Application.UseCases.Activities.Register;
using Application.UseCases.Trips.Delete;
using Application.UseCases.Trips.GetAll;
using Application.UseCases.Trips.GetById;
using Application.UseCases.Trips.Register;
using Communication.Requests;
using Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        var useCase = new RegisterTripUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripsUseCase();

        var trips = useCase.Execute();

        return Ok(trips);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var useCase = new GetTripByIdUseCase();

        var trip = useCase.Execute(id);

        return Ok(trip);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var useCase = new DeleteTripByIdUseCase();

        useCase.Execute(id);

        return NoContent();
    }

    [HttpPost("{tripId}/activity")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult RegisterActivity(
        [FromRoute] Guid tripId, 
        [FromBody] RequestRegisterActivityJson request)
    {
        var useCase = new RegisterActivityForTripUseCase();

        var response = useCase.Execute(tripId, request);

        return Created(string.Empty, response);
    }

    [HttpPut("{tripId}/activity/{activityId}/complete")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult CompleteActivity(
        [FromRoute] Guid tripId,
        [FromRoute] Guid activityId)
    {
        var useCase = new CompleteActivityForTripUseCase();

        useCase.Execute(tripId, activityId);

        return NoContent();
    }

    [HttpDelete("{tripId}/activity/{activityId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult DeleteActivity(
        [FromRoute] Guid tripId,
        [FromRoute] Guid activityId)
    {
        var useCase = new DeleteActivityForTripByIdUseCase();

        useCase.Execute(tripId, activityId);

        return NoContent();
    }
}
