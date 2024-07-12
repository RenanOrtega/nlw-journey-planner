using Exception.ExceptionsBase;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Trips.Delete;

public class DeleteTripByIdUseCase
{
    public void Execute(Guid id)
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

        dbContext.Trips.Remove(trip);
        dbContext.SaveChanges();
    }
}
