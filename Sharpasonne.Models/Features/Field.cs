using System.Collections.Immutable;

namespace Sharpasonne.Models.Features
{
    class Field : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}