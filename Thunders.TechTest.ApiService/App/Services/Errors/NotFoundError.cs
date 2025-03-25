using Thunders.TechTest.ApiService.Shared;
using Thunders.TechTest.ApiService.Shared.Constants;

namespace Thunders.TechTest.ApiService.App.Services.Errors
{
    public class NotFoundError(string Msg) : Error(Code, Msg)
    {
        private new const string Code = ErrorCodes.NotFound;
    }
}
