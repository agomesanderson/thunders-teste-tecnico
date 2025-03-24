using System.Collections.ObjectModel;

namespace Thunders.TechTest.ApiService.Shared
{
    public class Result<T>
    {
        public Option<T> Data { get; private init; } = Option<T>.None;

        public ReadOnlyCollection<Error> Errors { get; private init; } = new(new List<Error>());

        public bool IsSuccess => IsFailure is false;

        public bool IsFailure => Errors.Count > 0;

        public static Result<T> Fail(Error error) => new() { Errors = new ReadOnlyCollection<Error>(new List<Error> { error }) };

        public static Result<T> Fail(IList<Error> errors) => new() { Errors = new ReadOnlyCollection<Error>(errors) };

        public static Result<T> Ok() => new();

        public static Result<T> Ok(T value) => new() { Data = Option<T>.Some(value) };

        public TMatch Match<TMatch>(Func<Option<T>, TMatch> onSuccess, Func<IReadOnlyCollection<Error>, TMatch> onFailure)
        {
            return IsSuccess ? onSuccess(Data) : onFailure(Errors);
        }
    }

    public class Result
    {
        public ReadOnlyCollection<Error> Errors { get; private init; } = new(new List<Error>());

        public bool IsSuccess => IsFailure is false;

        public bool IsFailure => Errors.Count > 0;

        public static Result Fail(Error error) => new() { Errors = new ReadOnlyCollection<Error>(new List<Error> { error }) };

        public static Result Fail(IList<Error> errors) => new() { Errors = new ReadOnlyCollection<Error>(errors) };

        public static Result Ok() => new();

        public T Match<T>(Func<T> onSuccess, Func<IReadOnlyCollection<Error>, T> onFailure)
        {
            return IsSuccess ? onSuccess() : onFailure(Errors);
        }
    }
}
