using Sharpasonne;
using Sharpasonne.Models;

namespace Sharpasonne.Rules
{
    public class SpaceIsEmptyRule : IRule
    {
        public bool Verify<T>(Engine engine, T gameAction)
            where T : IGameAction
        {
            return true;
        }
    }
}
