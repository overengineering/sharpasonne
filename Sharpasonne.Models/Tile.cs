using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace Sharpasonne.Models
{
    /// <summary>
    /// A Carcassonne Tile.
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// Unique set of 
        /// </summary>
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

        /// <summary>
        /// Order of the contents matters in order to match the edges in 
        /// AdjacentFeaturesMatch rule. 
        /// </summary>
        protected Segment[] GetSegments(Orientation direction)
        {
            // TODO: Test because of the order issue
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
                case Orientation.Left:
                    return new[]
                    {
                        Segment.LeftTop,
                        Segment.Left,
                        Segment.LeftBottom,
                    };
                case Orientation.Right:
                    return new[]
                    {
                        Segment.RightTop,
                        Segment.Right,
                        Segment.RightBottom,
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
