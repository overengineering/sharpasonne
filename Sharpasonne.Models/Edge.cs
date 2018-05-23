using System;

namespace Sharpasonne.Models
{
    /// <summary>
    /// Relative edge for orienting a tile.
    /// </summary>
    public enum Edge
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    public static class EdgeExtensions
    {
        /// <summary>
        /// Rotates an <see cref="Edge"/> by the degree representation of
        /// the given <paramref name="rotation"/> clockwise.
        /// </summary>
        public static Edge RotateClockwise(
            this Edge edge,
            Rotation  rotation)
        {
            var edgeAsInt     = (int)edge;
            var rotationAsInt = (int)rotation;

            var newEdge = (edgeAsInt + rotationAsInt) % 4;

            return (Edge)Enum.ToObject(typeof(Edge), newEdge);
        }

        /// <summary>
        /// Rotates an <see cref="Edge"/> by the degree representation of
        /// the given <paramref name="rotation"/> anti-clockwise.
        /// </summary>
        public static Edge RotateAntiClockwise(
            this Edge edge,
            Rotation  rotation)
        {
            var edgeAsInt     = (int)edge;
            var rotationAsInt = (int)rotation;

            var newEdge = (4 + (edgeAsInt - rotationAsInt)) % 4;

            return (Edge)Enum.ToObject(typeof(Edge), newEdge);
        }
    }
}