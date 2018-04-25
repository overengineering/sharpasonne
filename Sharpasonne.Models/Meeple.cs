namespace Sharpasonne.Models
{
    /// <summary>
    /// A player's token.
    /// </summary>
    public class Meeple
    {
        public Meeple(int player)
        {
            this.Player = player;
        }

        /// <summary>
        /// 1-indexed number of the Player.
        /// </summary>
        public int Player { get; }
    }
}