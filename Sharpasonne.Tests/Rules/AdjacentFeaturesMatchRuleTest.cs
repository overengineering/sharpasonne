using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Sharpasonne.Models.Features;
using Sharpasonne.Rules;
using Xunit;
using System.Collections.Immutable;
using Moq;
using Optional.Unsafe;

namespace Sharpasonne.Tests.Rules
{
    public class AdjacentFeaturesMatchRuleTest : RuleUnitTest<PlaceTileGameAction>
    {
        [Fact]
        public void Given_Empty_Then_True()
        {
            var mockEngine = new Mock<IEngine>();
            mockEngine
                .Setup(e => e.Board)
                .Returns(new Board());

            AssertTrue<AdjacentFeaturesMatchRule>(mockEngine.Object, MakePlaceTile(0, 0));
        }
        
        [Fact]
        public void Given_OneSouthernNeighbour_When_FeaturesMatch_Then_True()
        {
            var aboveTile = new TileBuilder()
                .CreateTile(new []
                {
                    new Field(ImmutableHashSet.Create(
                        Segment.BottomLeft,
                        Segment.Bottom,
                        Segment.BottomRight
                    )),
                })
                .ValueOrFailure();

            var belowTile = new TileBuilder()
                .CreateTile(new []
                {
                    new Field(ImmutableHashSet.Create(
                        Segment.TopLeft,
                        Segment.Top,
                        Segment.TopRight
                    )),
                })
                .ValueOrFailure();

            var aboveAction = MakePlaceTile(0, 1, aboveTile);
            var belowAction = MakePlaceTile(0, 0, belowTile);
            var board = MakeBoard(aboveAction);
            var engine = MockEngine(board);

            AssertTrue<AdjacentFeaturesMatchRule>(engine, belowAction);
        }

        [Fact]
        public void Given_OneSouthernNeighbour_When_FeaturesDoNotMatch_Then_False()
        {
            var aboveTile = new TileBuilder()
                .CreateTile(new IFeature[]
                {
                    new City(
                        ImmutableHashSet.Create(Segment.BottomLeft),
                        hasShield: true
                    ),
                    new Field(ImmutableHashSet.Create(
                        Segment.Bottom,
                        Segment.BottomRight
                    )),
                })
                .ValueOrFailure();

            var belowTile = new TileBuilder()
                .CreateTile(new[]
                {
                    new Field(ImmutableHashSet.Create(
                        Segment.TopLeft,
                        Segment.Top,
                        Segment.TopRight
                    )),
                })
                .ValueOrFailure();

            var aboveAction = MakePlaceTile(0, 1, aboveTile);
            var belowAction = MakePlaceTile(0, 0, belowTile);
            var board = MakeBoard(aboveAction);
            var engine = MockEngine(board);

            AssertFalse<AdjacentFeaturesMatchRule>(engine, belowAction);
        }

        [Fact]
        public void Given_OneEasternNeighbour_When_FeaturesDoNotMatch_Then_False()
        {
            var leftTile = new TileBuilder()
                .CreateTile(new IFeature[]
                {
                    new City(
                        ImmutableHashSet.Create(Segment.RightBottom),
                        hasShield: true
                    ),
                    new Field(ImmutableHashSet.Create(
                        Segment.Right,
                        Segment.RightTop
                    )),
                })
                .ValueOrFailure();

            var rightTile = new TileBuilder()
                .CreateTile(new[]
                {
                    new Field(ImmutableHashSet.Create(
                        Segment.LeftTop,
                        Segment.Left,
                        Segment.LeftBottom
                    )),
                })
                .ValueOrFailure();

            var leftAction = MakePlaceTile(0, 0, leftTile);
            var rightAction = MakePlaceTile(1, 0, rightTile);
            var board = MakeBoard(leftAction);
            var engine = MockEngine(board);

            AssertFalse<AdjacentFeaturesMatchRule>(engine, rightAction);
        }

        [Fact]
        public void Given_OneRotatedNeighbour_When_FeaturesMatch_Then_True()
        {
            var leftTile = new TileBuilder()
                .CreateTile(new IFeature[]
                {
                    new Field(ImmutableHashSet.Create(
                        Segment.RightTop,
                        Segment.Right,
                        Segment.RightBottom
                    )),
                })
                .ValueOrFailure();

            var rightTile = new TileBuilder()
                .CreateTile(new[]
                {
                    new Field(ImmutableHashSet.Create(
                        Segment.TopLeft,
                        Segment.Top,
                        Segment.TopRight
                    )),
                })
                .ValueOrFailure();

            var leftAction  = MakePlaceTile(0, 0, leftTile,  Rotation.None);
            var rightAction = MakePlaceTile(1, 0, rightTile, Rotation.ThreeQuarter);
            var board       = MakeBoard(leftAction);
            var engine      = MockEngine(board);

            AssertTrue<AdjacentFeaturesMatchRule>(engine, rightAction);
        }

        [Fact]
        public void Given_TwoRotatedNeighbours_When_FeaturesMatch_Then_True()
        {
            var topTile = new TileBuilder()
                .CreateTile(new IFeature[]
                {
                    new Field(ImmutableHashSet.Create(
                        Segment.RightTop,
                        Segment.Right,
                        Segment.RightBottom
                    )),
                })
                .ValueOrFailure();

            var centreTile = new TileBuilder()
                .CreateTile(new[]
                {
                    new Field(ImmutableHashSet.Create(
                        Segment.TopLeft,
                        Segment.Top,
                        Segment.TopRight,
                        Segment.BottomLeft,
                        Segment.Bottom,
                        Segment.BottomRight
                    )),
                })
                .ValueOrFailure();

            var bottomTile = new TileBuilder()
                .CreateTile(new[]
                {
                    new Field(ImmutableHashSet.Create(
                        Segment.RightTop,
                        Segment.Right,
                        Segment.RightBottom
                    )),
                })
                .ValueOrFailure();

            var topAction    = MakePlaceTile(0, 1, topTile,    Rotation.Quarter);
            var centreAction = MakePlaceTile(0, 0,  centreTile, Rotation.Half);
            var bottomAction = MakePlaceTile(0, -1,  bottomTile, Rotation.ThreeQuarter);
            var board        = MakeBoard(topAction, bottomAction);
            var engine       = MockEngine(board);

            AssertTrue<AdjacentFeaturesMatchRule>(engine, centreAction);
        }
    }
}