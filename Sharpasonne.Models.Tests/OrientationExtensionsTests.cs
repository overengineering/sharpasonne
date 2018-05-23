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
            Assert.Equal(Edge.Top, Edge.Top.RotateClockwise(Rotation.None));
        }

        [Fact]
        public void WhenRotatingByEVERYTHING_Then_IsCorrect()
        {
            Assert.Equal(Edge.Right, Edge.Top.RotateClockwise(Rotation.Quarter));
            Assert.Equal(Edge.Top, Edge.Right.RotateClockwise(Rotation.ThreeQuarter));
            Assert.Equal(Edge.Bottom, Edge.Right.RotateClockwise(Rotation.Quarter));
            Assert.Equal(Edge.Top, Edge.Bottom.RotateClockwise(Rotation.Half));
            Assert.Equal(Edge.Bottom, Edge.Left.RotateClockwise(Rotation.ThreeQuarter));
        }

        [Fact]
        public void WhenRotatingInverseByEVERYTHING_Then_IsCorrect()
        {
            Assert.Equal(Edge.Left, Edge.Top.RotateAntiClockwise(Rotation.Quarter));
            Assert.Equal(Edge.Bottom, Edge.Right.RotateAntiClockwise(Rotation.ThreeQuarter));
            Assert.Equal(Edge.Top, Edge.Right.RotateAntiClockwise(Rotation.Quarter));
            Assert.Equal(Edge.Top, Edge.Bottom.RotateAntiClockwise(Rotation.Half));
            Assert.Equal(Edge.Top, Edge.Left.RotateAntiClockwise(Rotation.ThreeQuarter));
        }
    }
}
