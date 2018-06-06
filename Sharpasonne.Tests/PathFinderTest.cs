using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Sharpasonne.Models.Features;
using Sharpasonne.Rules;
using Xunit;
using System.Collections.Immutable;
using Moq;
using Optional.Unsafe;

namespace Sharpasonne.Tests
{
    public class PathFinderTest : UnitTest
    {
        [Fact]
        public void ACityOnTwoTiles_GettingAllFeatureTiles_ReturnsTwoTiles()
        {
            //Given
            var pathFinder = new PathFinder();
            var topTileCity = new City(ImmutableHashSet.Create(
                        Segment.Bottom
                    ),
                    hasShield: false);
            var aboveTile = new TileBuilder()
                .CreateTile(new []
                {
                    topTileCity,
                })
                .ValueOrFailure();

            var belowTile = new TileBuilder()
                .CreateTile(new []
                {
                    new City(ImmutableHashSet.Create(
                        Segment.Top
                    ),
                    hasShield: false),
                })
                .ValueOrFailure();

            var aboveAction = MakePlaceTile(0, 1, aboveTile);
            var belowAction = MakePlaceTile(0, 0, belowTile);
            var board = MakeBoard(aboveAction, belowAction);
        
            //When
            var cityTiles = pathFinder.FindFeatureTiles(
                aboveAction.Point,
                board,
                topTileCity);
            
            //Then
            Assert.Equal(2, cityTiles.ValueOrFailure().Count);
        }

        [Fact]
        public void FindFeatureTiles_Given_OneTileWithOneCityFeatureIsPlaced_Then_ReturnsOneTile()
        {
            var pathFinder = new PathFinder();
            var topTileCity = new City(ImmutableHashSet.Create(
                        Segment.Top
                    ),
                    hasShield: false);

            var tile = new TileBuilder()
                .CreateTile(new []
                {
                    new City(ImmutableHashSet.Create(
                        Segment.Top
                    ),
                    hasShield: false),
                })
                .ValueOrFailure();
            
            var placeTileGameAction = MakePlaceTile(0, 0, tile);
            var board = MakeBoard(placeTileGameAction);
        
            //When
            var cityTiles = pathFinder.FindFeatureTiles(
                placeTileGameAction.Point,
                board,
                topTileCity);
            
            //Then
            Assert.Equal(1, cityTiles.ValueOrFailure().Count);
        }
    }
}
