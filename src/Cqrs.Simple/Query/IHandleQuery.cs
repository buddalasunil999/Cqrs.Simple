namespace Cqrs.Simple
{
    public interface IHandleQuery<TArguments, TResult>
        where TArguments : IQuery
    {
        TResult Handle(TArguments message);
    }
}