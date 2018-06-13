using System.Collections.Generic;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Sharpasonne.Models.Features;
using Sharpasonne.Rules;
using Xunit;
using System.Collections.Immutable;
using System.Linq;
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

        [Fact]
        public void FindFeatureTiles_Given_TwoTilesWithDisconnectedSingleCityFeatures_Then_ReturnsOneTile()
        {
            var pathFinder = new PathFinder();

            var topTile = new TileBuilder()
                .CreateTile(
                    new City(connections: Segment.Top))
                .ValueOrFailure();

            var bottomTile = new TileBuilder()
                .CreateTile(
                    new City(connections: Segment.Bottom))
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
        public void FindFeaturesTiles_Given_TwoConnectedAndOneDisconnectedTileCities_thenReturnsTwoTiles()
        {
            var pathFinder = new PathFinder();

            var topTile = new TileBuilder()
                .CreateTile(
                    new City(connections: Segment.Top))
                .ValueOrFailure();

            var middleTile = new TileBuilder()
                .CreateTile(
                    new City(connections: Segment.Bottom))
                .ValueOrFailure();

            var bottomTile = new TileBuilder()
                .CreateTile(
                    new City(connections: Segment.Top))
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
    }
}
