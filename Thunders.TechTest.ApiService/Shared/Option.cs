namespace Thunders.TechTest.ApiService.Shared
{
    public abstract record Option<T>
    {
        public static Option<T> Some(T value) => new Some<T>(value);

        public static Option<T> None => new None<T>();

        public abstract bool HasValue { get; }

        public abstract T Value { get; }

        public T Or(T defaultValue) => HasValue ? Value : defaultValue;

        public T Or(Func<T> alternative) => HasValue ? Value : alternative();

        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none)
        {
            return HasValue ? some(Value) : none();
        }

        public abstract Option<TOut> Map<TOut>(Func<T, TOut> map);

        public static implicit operator Option<T>(T? value)
        {
            return value is null
              ? None
              : Some(value);
        }
    }

    public record Some<TSome> : Option<TSome>
    {
        public Some(TSome value) => Value = value;

        public override bool HasValue => Value is not null;

        public override TSome Value { get; }

        public override Option<TOut> Map<TOut>(Func<TSome, TOut> map) => Option<TOut>.Some(map(Value));
    }

    public record None<TNone> : Option<TNone>
    {
        public override bool HasValue => false;

        public override TNone Value => default!;

        public override Option<TOut> Map<TOut>(Func<TNone, TOut> map) => Option<TOut>.None;
    }
}
