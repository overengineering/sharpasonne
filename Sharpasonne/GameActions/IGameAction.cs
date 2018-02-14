namespace Sharpasonne.GameActions
{
    public interface IGameAction
    {
        IEngine Perform(IEngine engine);
    }
}
