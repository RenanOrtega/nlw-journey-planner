
using Exception.ExceptionsBase;
using Infrastructure;

namespace Application.UseCases.Activities.Delete;

public class DeleteActivityForTripByIdUseCase
{
    public void Execute(Guid tripId, Guid activityId)
    {
        var dbContext = new JourneyDbContext();

        var activity = dbContext
            .Activities
            .FirstOrDefault(activity => activity.Id == activityId && activity.TripId == tripId);

        if (activity == null)
        {
            throw new NotFoundException(ResourceErrorMessages.ACTIVITY_NOT_FOUND);
        }

        dbContext.Activities.Remove(activity);
        dbContext.SaveChanges();
    }
}
