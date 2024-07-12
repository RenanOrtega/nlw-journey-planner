using Communication.Responses;
using Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is JourneyException journeyException)
        {
            context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();

            var responseErrorsJson = new ResponseErrorsJson(journeyException.GetErrorMessages());

            context.Result = new ObjectResult(responseErrorsJson);
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var responseErrorsJson = new ResponseErrorsJson([ResourceErrorMessages.UNKNOWN_ERROR]);

            context.Result = new ObjectResult(responseErrorsJson);
        }
    }
}
