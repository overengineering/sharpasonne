using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    internal interface IFeature
    {
        IImmutableSet<Segment> Connections { get; }
    }

    class Monastery : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}