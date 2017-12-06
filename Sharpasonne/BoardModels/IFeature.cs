using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    public interface IFeature
    {
        IImmutableSet<Segment> Connections { get; }
    }
}