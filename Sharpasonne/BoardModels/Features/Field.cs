using System.Collections.Immutable;

namespace Sharpasonne.BoardModels.Features
{
    class Field : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}