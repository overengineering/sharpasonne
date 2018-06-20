using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sharpasonne.Models
{
    /// <summary>
    /// Each 1/12th of the playable boundary sections of a <see cref="Tile"/>.
    /// </summary>
    public enum Segment
    {
        TopLeft,
        Top,
        TopRight,
        RightTop,
        Right,
        RightBottom,
        BottomRight,
        Bottom,
        BottomLeft,
        LeftBottom,
        Left,
        LeftTop,
    }

    public static class SegmentExtensions
    {
        public static IImmutableDictionary<Segment, Segment> AdjacentSegments =
            new Dictionary<Segment, Segment>
            {
                [Segment.TopLeft]     = Segment.BottomLeft,
                [Segment.Top]         = Segment.Bottom,
                [Segment.TopRight]    = Segment.BottomRight,
                [Segment.RightTop]    = Segment.LeftTop,
                [Segment.Right]       = Segment.Left,
                [Segment.RightBottom] = Segment.LeftBottom,
                [Segment.BottomRight] = Segment.TopLeft,
                [Segment.Bottom]      = Segment.Top,
                [Segment.BottomLeft]  = Segment.TopRight,
                [Segment.LeftBottom]  = Segment.RightBottom,
                [Segment.Left]        = Segment.Right,
                [Segment.LeftTop]     = Segment.RightTop,
            }.ToImmutableDictionary();
    }
}