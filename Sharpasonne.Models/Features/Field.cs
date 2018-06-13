using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace Sharpasonne.Models.Features
{
    /// <inheritdoc/>
    public class Field : IFeature
    {
        [NotNull]
        public IImmutableSet<Segment> Connections { get; }

        public Field([NotNull] IEnumerable<Segment> connections)
        {
            Connections = connections.ToImmutableHashSet();
        }

        public Field([NotNull] params Segment[] connections)
            : this(connections.AsEnumerable())
        {
        }
    }
}

