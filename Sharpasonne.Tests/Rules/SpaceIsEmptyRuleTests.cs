using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sharpasonne.GameActions;
using Sharpasonne.Rules;
using Xunit;
using Moq;
using Optional.Unsafe;
using Sharpasonne.Models;
using Sharpasonne.Models.Features;

namespace Sharpasonne.Tests.Rules
{
    public class SpaceIsEmptyRuleTests
    {
        [Fact]
        public void When_TileIsEmpty_Then_True()
        {
            var tile = new TileBuilder()
                .CreateTile(Enumerable.Empty<IFeature>())
                .ValueOrFailure();
            var centerPoint = new Point(0, 0);
            var placement = new Placement(tile, Orientation.Top);
            Assert.True(new SpaceIsEmptyRule().Verify(new Engine(), new PlaceTileGameAction(centerPoint, placement)));
        }

        [Fact]
        public void When_TileIsNotEmpty_Then_False()
        {
            var tile = new TileBuilder()
                .CreateTile(Enumerable.Empty<IFeature>())
                .ValueOrFailure();

            var centerPoint = new Point(0, 0);
            var placement = new Placement(tile, Orientation.Top);
            var board =
                new Dictionary<Point, Placement>
                {
                    [centerPoint] = placement
                }
                .ToImmutableDictionary();

            var engine = new Mock<IEngine>();
            engine
                .Setup(mockEngine => mockEngine.Board)
                .Returns(new Board(board));

            var placeTileGameAction = new PlaceTileGameAction(centerPoint, placement);
            Assert.False(new SpaceIsEmptyRule().Verify(engine.Object, placeTileGameAction));
        }
    }
}
