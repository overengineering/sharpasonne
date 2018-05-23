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

            var placement = new TilePlacement(tile, Rotation.None);

            // Act
            var actualEdge = placement.GetFeaturesAt(Edge.Top);

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

            var placement = new TilePlacement(tile, Rotation.None);

            // Act
            var actualEdge = placement.GetFeaturesAt(Edge.Right);

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

            var placement = new TilePlacement(tile, Rotation.Quarter);

            // Act
            var actualEdge = placement.GetFeaturesAt(Edge.Bottom);

            // Assert
            IFeature[] expectedEdge = {
                field,field,field
            };

            Assert.Equal(expectedEdge, actualEdge);
        }


    }
}
