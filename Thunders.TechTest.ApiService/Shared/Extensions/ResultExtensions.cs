using Microsoft.AspNetCore.Mvc;
using System.Net;
using Thunders.TechTest.ApiService.Shared.Constants;

namespace Thunders.TechTest.ApiService.Shared.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToHttpErrors(this IReadOnlyCollection<Error> errors)
        {
            return errors.ToFailureResult();
        }

        private static ObjectResult ToFailureResult(this IReadOnlyCollection<Error> errors)
        {
            var httpErrors = errors.Select(MapToStatusCode);

            return new ObjectResult(errors)
            {
                StatusCode = httpErrors.Max(),
            };
        }

        private static int MapToStatusCode(Error error)
        {
            var hasStatusCode = ErrorCodeToHttpStatusCode.TryGetValue(error.Code, out var httpStatusCode);
            return hasStatusCode ? (int)httpStatusCode : (int)HttpStatusCode.InternalServerError;
        }

        private static readonly Dictionary<string, HttpStatusCode> ErrorCodeToHttpStatusCode = new()
        {
            [ErrorCodes.DatabaseError] = HttpStatusCode.InternalServerError,
            [ErrorCodes.UnexpectedError] = HttpStatusCode.InternalServerError,
            [ErrorCodes.NotFound] = HttpStatusCode.NotFound
        };
    }
}
