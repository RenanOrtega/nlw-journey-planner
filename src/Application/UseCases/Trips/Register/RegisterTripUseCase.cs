using Communication.Requests;
using Communication.Responses;
using Exception.ExceptionsBase;
using Infrastructure;
using Infrastructure.Entities;

namespace Application.UseCases.Trips.Register;

public class RegisterTripUseCase
{
    public ResponseShortTripJson Execute(RequestRegisterTripJson request)
    {
        Validate(request);

        var dbContext = new JourneyDbContext();

        var entity = new Trip
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        dbContext.Trips.Add(entity);

        dbContext.SaveChanges();

        return new ResponseShortTripJson
        {
            EndDate = entity.EndDate,
            StartDate = entity.StartDate,
            Name = entity.Name,
            Id = entity.Id
        };
    }

    private static void Validate(RequestRegisterTripJson request)
    {
        var validator = new RegisterTripValidator();
        
        var validation = validator.Validate(request);

        if (validation.IsValid is false)
        {
            var errorMessages = validation.Errors
                .Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
