namespace GivenWhenThenTemplateToCSharp.ResponsibilityChain
{
    // ReSharper disable once UnusedTypeParameter
    public interface IChainContext<THandler, TIn, TOut>
        where THandler : IHandler<TIn, TOut>
    {
    }
}