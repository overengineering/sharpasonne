namespace Sharpasonne.GameActions
{
    /// <summary>
    /// A command to change the state of the game.
    /// </summary>
    public interface IGameAction
    {
        IEngine Perform(IEngine engine);
    }
}
