using Microsoft.AspNetCore.Diagnostics;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // await httpContext.Response.WriteAsJsonAsync("Somethig went wrong. Please try again later.");

        var response = new ExceptionResponse
        {
            ExceptionMessage = exception.Message,
            StatusCode = httpContext.Response.StatusCode.ToString(),
            Title = "An error occurred while processing your request."
        };
        await httpContext.Response.WriteAsJsonAsync(response);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;


        return true;
    }
}
// httpContext is the current HTTP context of the request.
// exception is the exception that was thrown during request processing.
// cancellationToken is used to propagate notification that operations should be canceled.