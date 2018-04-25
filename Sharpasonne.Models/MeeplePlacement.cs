using JetBrains.Annotations;

namespace Sharpasonne.Models
{
    /// <summary>
    /// The <see cref="IFeature"/> a <see cref="Meeple"/> claims on a
    /// <see cref="Tile"/>.
    /// </summary>
    public class MeeplePlacement
    {
        [NotNull]
        public Meeple  Meeple  { get; }

        // TODO: Change to IFeature... so it can be used with a Monastery.
        public Segment Segment { get; }

        public MeeplePlacement([NotNull] Meeple meeple, Segment segment)
        {
            this.Meeple = meeple;
            this.Segment = segment;
        }
    }
}