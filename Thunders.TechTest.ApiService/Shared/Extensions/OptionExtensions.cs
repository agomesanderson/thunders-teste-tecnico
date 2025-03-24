namespace Thunders.TechTest.ApiService.Shared.Extensions
{
    public static class OptionExtensions
    {
        public static Option<T> ToOption<T>(this T? value) where T : struct
        {
            return value.HasValue ? Option<T>.Some(value.Value) : Option<T>.None;
        }

        public static Option<T> ToOption<T>(this T? value) where T : class
        {
            return value is not null && !(value is string str && string.IsNullOrWhiteSpace(str))
              ? Option<T>.Some(value)
              : Option<T>.None;
        }

        public static T? ToNullableValue<T>(this Option<T> option) where T : struct
        {
            return option.HasValue
              ? option.Value
              : null;
        }

        public static T? GetValueOrNull<T>(this Option<T> option) where T : class
        {
            return option.HasValue
              ? option.Value
              : null;
        }
    }
}
