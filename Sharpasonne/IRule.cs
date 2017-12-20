using Sharpasonne.Models;

namespace Sharpasonne
{
    public interface IRule
    {
        bool Verify<T>(Engine engine, T gameAction)
            where T : IGameAction;
    }
}
