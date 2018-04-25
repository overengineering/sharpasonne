using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sharpasonne.Models
{
    /// <summary>
    /// A distinct component of a tile.
    /// </summary>
    public interface IFeature
    {
        /// <summary>
        /// <see cref="Segment"/>s that this feature is on.
        /// </summary>
        [NotNull]
        IImmutableSet<Segment> Connections { get; }
    }
}

