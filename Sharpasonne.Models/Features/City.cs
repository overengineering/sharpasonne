using System.Collections.Immutable;

namespace Sharpasonne.Models.Features
{
    public class City : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }

        public bool HasShield { get; }

        public City(IImmutableSet<Segment> connections, bool hasShield)
        {
            Connections = connections;
            HasShield = hasShield;
        }
    }
}
