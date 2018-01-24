using System.Collections.Immutable;

namespace Sharpasonne.Models.Features
{
    public class Road : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }

        public Road(IImmutableSet<Segment> connections)
        {
            Connections = connections;
        }
    }
}
