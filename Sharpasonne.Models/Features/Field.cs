using System.Collections.Immutable;

namespace Sharpasonne.Models.Features
{
    public class Field : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }

        public Field(IImmutableSet<Segment> connections)
        {
            Connections = connections;
        }
    }
}

