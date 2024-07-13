using Communication.Requests;
using Exception.ExceptionsBase;
using FluentValidation;

namespace Application.UseCases.Activities.Register;

public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
{
    public RegisterActivityValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
    }
}
