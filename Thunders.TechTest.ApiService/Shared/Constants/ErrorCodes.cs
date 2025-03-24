namespace Thunders.TechTest.ApiService.Shared.Constants
{
    public static class ErrorCodes
    {
        private const string Prefix = "TTT";
        public const string DatabaseError = $"{Prefix}001";
        public const string UnexpectedError = $"{Prefix}002";
    }
}
