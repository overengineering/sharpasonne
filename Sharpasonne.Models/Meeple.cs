namespace Sharpasonne.Models
{
    public class Meeple
    {
        public Meeple(int player)
        {
            Player = player;
        }

        public int Player { get; }
    }
}