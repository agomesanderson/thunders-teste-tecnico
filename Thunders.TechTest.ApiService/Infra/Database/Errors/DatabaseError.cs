using Thunders.TechTest.ApiService.Shared;
using Thunders.TechTest.ApiService.Shared.Constants;

namespace Thunders.TechTest.ApiService.Infra.Database.Errors
{
    public class DatabaseError(string Msg) : Error(Code, Msg)
    {
        private new const string Code = ErrorCodes.DbError;
    }
}
