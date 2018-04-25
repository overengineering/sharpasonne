using Sharpasonne.GameActions;

namespace Sharpasonne.Rules
{
    /// <summary>
    /// Determine if a <see cref="IGameAction"/> may be played.
    /// </summary>
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IRule<T2> where T2 : IGameAction
    {
        /// <inheritdoc cref="IRule{T2}"/>
        bool Verify<T1>(IEngine engine, T1 gameAction)
            where T1 : T2 ;
    }
}
