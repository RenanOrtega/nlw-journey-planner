using Communication.Responses;
using Exception.ExceptionsBase;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Trips.GetById;

public class GetTripByIdUseCase
{
    public ResponseTripJson Execute(Guid id)
    {
        var dbContext = new JourneyDbContext();

        var trip = dbContext
            .Trips
            .Include(trip => trip.Activities)
            .FirstOrDefault(trip => trip.Id == id);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        return new ResponseTripJson
        {
            Id = trip.Id,
            EndDate = trip.EndDate,
            StartDate = trip.StartDate,
            Name = trip.Name,
            Activities = trip.Activities.Select(activity => new ResponseActivityJson
            {
                Name = activity.Name,
                Id = activity.Id,
                Date = activity.Date,
                Status = (Communication.Enums.ActivityStatus)activity.Status
            }).ToList()
        };
    }
}
