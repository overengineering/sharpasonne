using System;

namespace Sharpasonne.Models
{
    /// <summary>
    /// Each value represents where the Top of a <see cref="Tile"/> exists when
    /// rotated and placed on the <see cref="Board"/>.
    /// </summary>
    /// <example>
    /// <see cref="Right"/> means that the top edge of a <see cref="Tile"/> has
    /// been rotated 90deg and now faces right.
    /// </example>
    public enum Orientation
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    public static class OrientationExtensions
    {
        /// <summary>
        /// Rotates a <see cref="Orientation"/> by the degree representation of
        /// the given <paramref name="modifierOrientation"/> clockwise.
        /// </summary>
        // TODO: change modifierOrientation into a Rotation type.
        public static Orientation Rotate(
            this Orientation baseOrientation,
                 Orientation modifierOrientation)
        {
            var baseAsInt = (int) baseOrientation;
            var modifierAsInt = (int) modifierOrientation;

            var newOrientation = (baseAsInt + modifierAsInt) % 4;

            return (Orientation) Enum.ToObject(typeof(Orientation), newOrientation);
        }

        /// <summary>
        /// Rotates a <see cref="Orientation"/> by the degree representation of
        /// the given <paramref name="modifierOrientation"/> anti-clockwise.
        /// </summary>
        public static Orientation RotateInverse(
            this Orientation baseOrientation,
                 Orientation modifierOrientation)
        {
            var baseAsInt = (int)baseOrientation;
            var modifierAsInt = (int)modifierOrientation;

            var newOrientation = (4 + (baseAsInt - modifierAsInt)) % 4;

            return (Orientation)Enum.ToObject(typeof(Orientation), newOrientation);
        }
    }
}