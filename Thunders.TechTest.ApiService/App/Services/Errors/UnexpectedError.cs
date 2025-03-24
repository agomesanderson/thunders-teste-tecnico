using Thunders.TechTest.ApiService.Shared;
using Thunders.TechTest.ApiService.Shared.Constants;

namespace Thunders.TechTest.ApiService.App.Services.Errors
{
    public class UnexpectedError(string Msg) : Error(Code, Msg)
    {
        private new const string Code = ErrorCodes.UnexpectedError;
    }
}
