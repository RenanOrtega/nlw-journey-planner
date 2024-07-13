using Communication.Requests;
using Communication.Responses;
using Infrastructure.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Exception.ExceptionsBase;
using Journey.Infrastructure.Enums;
using FluentValidation.Results;

namespace Application.UseCases.Activities.Register;

public class RegisterActivityForTripUseCase
{
    public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
    {
        var dbContext = new JourneyDbContext();

        var trip = dbContext
            .Trips
            .FirstOrDefault(trip => trip.Id == tripId);

        Validate(trip, request);
        
        var activity = new Activity
        {
            Name = request.Name,
            Date = request.Date,
            TripId = tripId
        };

        dbContext.Activities.Add(activity);
        dbContext.SaveChanges();

        return new ResponseActivityJson
        {
            Date = activity.Date,
            Id = activity.Id,
            Name = activity.Name,
            Status = (Communication.Enums.ActivityStatus)activity.Status
        };
    }

    private static void Validate(Trip? trip, RequestRegisterActivityJson request)
    {
        if (trip == null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        var validator = new RegisterActivityValidator();

        var validation = validator.Validate(request);

        if (!(request.Date >= trip.StartDate && request.Date <= trip.EndDate))
        {
            validation.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATE_NOT_WITHIN_TRAVEL_PERIOD));
        }

        if (!validation.IsValid)
        {
            var errorMessages = validation.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
