using Sharpasonne.GameActions;

namespace Sharpasonne.Rules
{
    public class UniqueTileInstanceRule : IRule<PlaceTileGameAction>
    {
        public bool Verify<T1>(IEngine engine, T1 gameAction) where T1 : PlaceTileGameAction
        {
            return true;
        }
    }
}
