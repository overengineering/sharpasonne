using Sharpasonne.GameActions;

namespace Sharpasonne.Rules
{
    public interface IRule<T2> where T2 : IGameAction
    {
        bool Verify<T1>(IEngine engine, T1 gameAction)
            where T1 : T2 ;
    }
}
