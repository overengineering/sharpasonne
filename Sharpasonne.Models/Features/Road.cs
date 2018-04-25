using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sharpasonne.Models.Features
{
    /// <inheritdoc/>
    public class Road : IFeature
    {
        [NotNull]
        public IImmutableSet<Segment> Connections { get; }

        public Road([NotNull] IImmutableSet<Segment> connections)
        {
            this.Connections = connections;
        }
    }
}
