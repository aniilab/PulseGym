using System.Net;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.Exceptions;

namespace PulseGym.API.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException exception)
            {
                await SetExceptionResponseAsync(context, HttpStatusCode.NotFound, exception.Message);
            }
            catch (BadInputException exception)
            {
                await SetExceptionResponseAsync(context, HttpStatusCode.BadRequest, exception.Message);
            }
            catch (InvalidMembershipProgramException exception)
            {
                await SetExceptionResponseAsync(context, HttpStatusCode.MethodNotAllowed, exception.Message);
            }
            catch (UnauthorizedException exception)
            {
                await SetExceptionResponseAsync(context, HttpStatusCode.Unauthorized, exception.Message);
            }
            catch (Exception exception)
            {
                await SetExceptionResponseAsync(context, HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        public async Task SetExceptionResponseAsync(HttpContext context, HttpStatusCode statusCode, string detail)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Type = statusCode.ToString(),
                Title = statusCode.ToString(),
                Detail = detail
            };

            string json = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(json);
        }
    }
}
