using System.Collections.Immutable;

namespace Sharpasonne.Models
{
    public interface IFeature
    {
        IImmutableSet<Segment> Connections { get; }
    }
}