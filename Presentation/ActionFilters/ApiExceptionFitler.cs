using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters;

public class ApiExceptionFitler : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is ConflictException)
            context.Result = new ObjectResult(new { message = context.Exception.Message }) { StatusCode = 409 };
        else if (context.Exception is NotFoundException)
            context.Result = new ObjectResult(new { message = context.Exception.Message }) { StatusCode = 404 };
        else
            context.Result = new ObjectResult(new { message = "Internal Server Error" }) { StatusCode = 500 };

        return Task.CompletedTask;
    }
}
