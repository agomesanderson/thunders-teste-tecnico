namespace Thunders.TechTest.ApiService.Shared
{
    public class Error
    {
        public Error(string code, string message, string? parameter = null)
        {
            Code = code;
            Message = message;
            Parameter = parameter;
        }

        public string Code { get; }

        public string Message { get; }

        public string? Parameter { get; }
    }
}
