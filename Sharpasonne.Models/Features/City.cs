using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sharpasonne.Models.Features
{
    /// <inheritdoc />
    public class City : IFeature
    {
        [NotNull]
        public IImmutableSet<Segment> Connections { get; }

        public bool HasShield { get; }

        public City([NotNull] IImmutableSet<Segment> connections, bool hasShield)
        {
            Connections = connections;
            HasShield = hasShield;
        }
    }
}
