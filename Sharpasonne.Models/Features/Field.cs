using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sharpasonne.Models.Features
{
    /// <inheritdoc/>
    public class Field : IFeature
    {
        [NotNull]
        public IImmutableSet<Segment> Connections { get; }

        public Field([NotNull] IImmutableSet<Segment> connections)
        {
            Connections = connections;
        }
    }
}

