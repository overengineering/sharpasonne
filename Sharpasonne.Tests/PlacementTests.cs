using System.Collections.Immutable;
using Optional.Unsafe;
using Sharpasonne.Models;
using Sharpasonne.Models.Features;
using Sharpasonne.Rules;
using Xunit;

namespace Sharpasonne.Tests
{
    public class PlacementTests
    {
        [Fact]
        public void Given_AnUnrotatedTile_When_GettingTopEdge_Then_ReturnsTopEdge()
        {
            // Arrange
            var field = new Field(ImmutableHashSet.Create(
                Segment.TopLeft,
                Segment.Top,
                Segment.TopRight
            ));

            var tile = new TileBuilder()
                .CreateTile(new IFeature[]
                {
                    field,
                })
                .ValueOrFailure();

            var placement = new Placement(tile, Orientation.Top);

            // Act
            var actualEdge = placement.GetEdge(Orientation.Top);

            // Assert
            IFeature[] expectedEdge = {
                field,field,field
            };

            Assert.Equal(expectedEdge, actualEdge);
        }

        [Fact]
        public void Given_AnUnrotatedTile_When_GettingRightEdge_Then_ReturnsRightEdge()
        {
            // Arrange
            var field = new Field(ImmutableHashSet.Create(
                Segment.RightTop,
                Segment.Right,
                Segment.RightBottom
            ));

            var tile = new TileBuilder()
                .CreateTile(new IFeature[]
                {
                    field,
                })
                .ValueOrFailure();

            var placement = new Placement(tile, Orientation.Top);

            // Act
            var actualEdge = placement.GetEdge(Orientation.Right);

            // Assert
            IFeature[] expectedEdge = {
                field,field,field
            };

            Assert.Equal(expectedEdge, actualEdge);
        }

        [Fact]
        public void Given_ARightRotatedTile_When_GettingRightEdge_Then_ReturnsBottomEdge()
        {
            // Arrange
            var field = new Field(ImmutableHashSet.Create(
                Segment.RightTop,
                Segment.Right,
                Segment.RightBottom
            ));

            var tile = new TileBuilder()
                .CreateTile(new IFeature[]
                {
                    field,
                })
                .ValueOrFailure();

            var placement = new Placement(tile, Orientation.Right);

            // Act
            var actualEdge = placement.GetEdge(Orientation.Bottom);

            // Assert
            IFeature[] expectedEdge = {
                field,field,field
            };

            Assert.Equal(expectedEdge, actualEdge);
        }
    }
}
