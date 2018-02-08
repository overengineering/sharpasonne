using System;

namespace Sharpasonne.Models
{
    public enum Orientation
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    public static class OrientationExtensions
    {
        public static Orientation Rotate(
            this Orientation baseOrientation,
                 Orientation modifierOrientation)
        {
            var baseAsInt = (int) baseOrientation;
            var modifierAsInt = (int) modifierOrientation;

            var newOrientation = (baseAsInt + modifierAsInt) % 4;

            return (Orientation) Enum.ToObject(typeof(Orientation), newOrientation);
        }

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