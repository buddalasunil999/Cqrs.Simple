namespace Cqrs.Simple
{
    public interface IHandleQuery<in TArguments, out TResult>
        where TArguments : IQuery
    {
        TResult Handle(TArguments message);
    }
}