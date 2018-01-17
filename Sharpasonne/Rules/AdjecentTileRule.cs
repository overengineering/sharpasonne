using Sharpasonne.GameActions;

namespace Sharpasonne.Rules
{
    abstract class AdjecentTileRule : IRule<PlaceTileGameAction>
    {
        public abstract bool Verify<T1>(IEngine engine, T1 gameAction) where T1 : PlaceTileGameAction;
    }
}
