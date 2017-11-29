using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    internal interface IFeature
    {
        IImmutableSet<Segment> Connections { get; }
    }
}