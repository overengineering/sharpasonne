using System.Collections.Immutable;

namespace Sharpasonne.Models.Features
{
    public class Field : IFeature
    {
        public Field(IImmutableSet<Segment> connections)
        {
            Connections = connections;
        }

        public IImmutableSet<Segment> Connections { get; }
    }
}