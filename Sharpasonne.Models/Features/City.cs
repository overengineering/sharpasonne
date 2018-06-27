using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace Sharpasonne.Models.Features
{
    /// <inheritdoc />
    public class City : IFeature
    {
        [NotNull]
        public IImmutableSet<Segment> Connections { get; }

        public bool HasShield { get; }

        public City([NotNull] IEnumerable<Segment> connections, bool hasShield)
        {
            Connections = connections.ToImmutableHashSet();
            HasShield = hasShield;
        }

        public City([NotNull] params Segment[] connections)
            : this(connections.AsEnumerable(), false)
        {
        }
    }
}
