using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sharpasonne.Models.Tests
{
    public class OrientationExtensionsTests
    {
        [Fact]
        public void Given_AnUnrotatedOrientation_WhenRotatingByTop_Then_ReturnSameOrientation()
        {
            Assert.Equal(Orientation.Top, Orientation.Top.Rotate(Orientation.Top));
        }

        [Fact]
        public void WhenRotatingByEVERYTHING_Then_IsCorrect()
        {
            Assert.Equal(Orientation.Right, Orientation.Top.Rotate(Orientation.Right));
            Assert.Equal(Orientation.Top, Orientation.Right.Rotate(Orientation.Left));
            Assert.Equal(Orientation.Bottom, Orientation.Right.Rotate(Orientation.Right));
            Assert.Equal(Orientation.Top, Orientation.Bottom.Rotate(Orientation.Bottom));
            Assert.Equal(Orientation.Bottom, Orientation.Left.Rotate(Orientation.Left));
        }

        [Fact]
        public void WhenRotatingInverseByEVERYTHING_Then_IsCorrect()
        {
            Assert.Equal(Orientation.Left, Orientation.Top.RotateInverse(Orientation.Right));
            Assert.Equal(Orientation.Bottom, Orientation.Right.RotateInverse(Orientation.Left));
            Assert.Equal(Orientation.Top, Orientation.Right.RotateInverse(Orientation.Right));
            Assert.Equal(Orientation.Top, Orientation.Bottom.RotateInverse(Orientation.Bottom));
            Assert.Equal(Orientation.Top, Orientation.Left.RotateInverse(Orientation.Left));
        }
    }
}
