using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sharpasonne.Models.Features
{
    /// <inheritdoc/>
    public class Monastery : IFeature
    {
        [NotNull]
        public IImmutableSet<Segment> Connections { get; } =
            ImmutableHashSet<Segment>.Empty;
    }
}
