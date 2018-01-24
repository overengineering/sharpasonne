using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace Sharpasonne.Models
{
    public class Tile
    {
        public IImmutableList<IFeature> Features { get; }

        internal Tile([NotNull] IEnumerable<IFeature> features)
        {
            this.Features = features.ToImmutableList();
        }

        public IFeature[] GetEdge(Orientation direction)
        {
            var segments = this.GetSegments(direction);
            var features = segments.Select(segment =>
                this.Features.First(feature =>
                    feature.Connections.Contains(segment)
                )
            );

            return features.ToArray();
        }

        protected Segment[] GetSegments(Orientation direction)
        {
            switch (direction)
            {
                case Orientation.Top:
                    return new[]
                        {
                            Segment.TopLeft,
                            Segment.Top,
                            Segment.TopRight,
                        };
                case Orientation.Bottom:
                    return new[]
                    {
                        Segment.BottomLeft,
                        Segment.Bottom,
                        Segment.BottomRight,
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
