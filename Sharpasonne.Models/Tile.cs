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

        public IFeature[] GetEdge(Edge edge)
        {
            var segments = this.GetSegments(edge);
            var features = segments
                .Select(segment =>
                    this.Features.FirstOrDefault(feature =>
                        feature.Connections.Contains(segment)))
                .Where(feature => feature != null);

            return features.ToArray();
        }

        /// <summary>
        /// Order of the contents matters in order to match the edges in 
        /// AdjacentFeaturesMatch rule. 
        /// </summary>
        protected Segment[] GetSegments(Edge edge)
        {
            // TODO: Test because of the order issue
            switch (edge)
            {
                case Edge.Top:
                    return new[]
                    {
                        Segment.TopLeft,
                        Segment.Top,
                        Segment.TopRight,
                    };
                case Edge.Bottom:
                    return new[]
                    {
                        Segment.BottomLeft,
                        Segment.Bottom,
                        Segment.BottomRight,
                    };
                case Edge.Left:
                    return new[]
                    {
                        Segment.LeftTop,
                        Segment.Left,
                        Segment.LeftBottom,
                    };
                case Edge.Right:
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