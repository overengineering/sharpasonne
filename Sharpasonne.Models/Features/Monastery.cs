using System.Collections.Immutable;

namespace Sharpasonne.Models.Features
{
    public class Monastery : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }

        public Monastery(IImmutableSet<Segment> connections)
        {
            Connections = connections;
        }
    }
}
