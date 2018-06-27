using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Optional.Unsafe;
using Sharpasonne.Models;
using Sharpasonne.Models.Features;
using Xunit;
using static Sharpasonne.Models.Segment;

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
                        Bottom
                    ),
                    hasShield: false);
            var aboveTile = new TileBuilder()
                .CreateTile(topTileCity)
                .ValueOrFailure();

            var belowTile = new TileBuilder()
                .CreateTile(new City(ImmutableHashSet.Create(
                        Top
                    ),
                    hasShield: false))
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
                        Top
                    ),
                    hasShield: false);

            var tile = new TileBuilder()
                .CreateTile(new City(ImmutableHashSet.Create(
                        Top
                    ),
                    hasShield: false))
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

        [Fact]
        public void FindFeatureTiles_Given_TwoTilesWithDisconnectedSingleCityFeatures_Then_ReturnsOneTile()
        {
            var pathFinder = new PathFinder();

            var topTile = new TileBuilder()
                .CreateTile(
                    new City(connections: Top))
                .ValueOrFailure();

            var bottomTile = new TileBuilder()
                .CreateTile(
                    new City(connections: Bottom))
                .ValueOrFailure();

            var placeTopTileGameAction = MakePlaceTile(0, 0, topTile);
            var placeBottomTileGameAction = MakePlaceTile(0, -1, bottomTile);
            var board = MakeBoard(placeTopTileGameAction, placeBottomTileGameAction);

            //When
            var cityTiles = pathFinder.FindFeatureTiles(
                placeBottomTileGameAction.Point,
                board,
                bottomTile.Features.FirstOrDefault());

            //Then
            Assert.Equal(1, cityTiles.ValueOrFailure().Count);
        }

        [Fact]
        public void FindFeaturesTiles_Given_TwoConnectedAndOneDisconnectedTileCities_Then_ReturnsTwoTiles()
        {
            var pathFinder = new PathFinder();

            var topTile = new TileBuilder()
                .CreateTile(new City(connections: Top))
                .ValueOrFailure();

            var middleTile = new TileBuilder()
                .CreateTile(new City(connections: Bottom))
                .ValueOrFailure();

            var bottomTile = new TileBuilder()
                .CreateTile(new City(connections: Top))
                .ValueOrFailure();

            var placeTopTileGameAction    = MakePlaceTile(0, 1,  topTile);
            var placeMiddleTileGameAction = MakePlaceTile(0, 0, middleTile);
            var placeBottomTileGameAction = MakePlaceTile(0, -1, bottomTile);

            var board = MakeBoard(
                placeTopTileGameAction,
                placeMiddleTileGameAction,
                placeBottomTileGameAction);

            //When
            var cityTiles = pathFinder.FindFeatureTiles(
                placeBottomTileGameAction.Point,
                board,
                bottomTile.Features.FirstOrDefault());

            //Then
            Assert.Equal(2, cityTiles.ValueOrFailure().Count);
        }

        [Fact]
        public void FindFeatureTiles_Given_FeatureSplittingInTwo_ThreeFeaturesAndTwoTilesAreReturned()
        {
            var pathFinder = new PathFinder();

            var leftTile = new TileBuilder()
                .CreateTile(new City(RightBottom, RightTop))
                .ValueOrFailure();
            var rightTile = new TileBuilder()
                .CreateTile(new City(LeftBottom), new City(LeftTop))
                .ValueOrFailure();

            // When
            var placeLeftTileGameAction = MakePlaceTile(0, 0, leftTile);
            var placeRightTileGameAction = MakePlaceTile(1, 0, rightTile);

            var board = MakeBoard(placeLeftTileGameAction, placeRightTileGameAction);

            var featureTiles = pathFinder.FindFeatureTiles(
                placeLeftTileGameAction.Point,
                board,
                leftTile.Features.FirstOrDefault())
                .ValueOrFailure();

            // Then
            var expectedFeatureTiles = new Dictionary<IFeature, Tile>
            {
                { leftTile.Features.First(), leftTile},
                { rightTile.Features.First(), rightTile},
                { rightTile.Features.ElementAt(1), rightTile},
            };
            Assert.Equal(expectedFeatureTiles, featureTiles);
        }
    }
}
