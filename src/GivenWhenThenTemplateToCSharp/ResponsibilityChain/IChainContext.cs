namespace GivenWhenThenTemplateToCSharp.ResponsibilityChain
{
    // ReSharper disable once UnusedTypeParameter
    /// <summary>
    /// A bag which holds temporary objects while handling an input.
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public interface IChainContext<THandler, TIn, TOut>
        where THandler : IHandler<TIn, TOut>
    {
    }
}