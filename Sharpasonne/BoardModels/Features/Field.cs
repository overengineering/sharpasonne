using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    class Field : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}