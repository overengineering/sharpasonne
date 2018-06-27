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

    public static class SegmentConstants
    {
        public static readonly IImmutableDictionary<Segment, Segment> AdjacentSegments =
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
            }
                .ToImmutableDictionary();

        public static readonly ImmutableDictionary<Edge, IImmutableList<Segment>> SegmentEdges =
            new Dictionary<Edge, IImmutableList<Segment>>
            {
                [Edge.Top]    = ImmutableList.Create(Segment.TopLeft,     Segment.Top,    Segment.TopRight),
                [Edge.Right]  = ImmutableList.Create(Segment.RightBottom, Segment.Right,  Segment.RightTop),
                [Edge.Bottom] = ImmutableList.Create(Segment.BottomLeft,  Segment.Bottom, Segment.BottomRight),
                [Edge.Left]   = ImmutableList.Create(Segment.LeftTop,     Segment.Left,   Segment.LeftBottom),
            }
                .ToImmutableDictionary();

    }
}