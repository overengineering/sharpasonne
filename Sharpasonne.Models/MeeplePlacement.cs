namespace Sharpasonne.Models
{
    public class MeeplePlacement
    {
        public Meeple  Meeple  { get; }
        public Segment Segment { get; }

        public MeeplePlacement(Meeple meeple, Segment segment)
        {
            Meeple  = meeple;
            Segment = segment;
        }
    }
}