using System.Collections.Generic;
using System.Linq;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Sharpasonne.Rules;
using Xunit;

namespace Sharpasonne.Tests.Rules
{
    public class UniqueTileInstanceRuleTests : UnitTest<PlaceTileGameAction>
    {
        [Fact]
        public void When_BoardIsEmpty_Then_True()
        {
            this.AssertTrue<UniqueTileInstanceRule>(new Engine(), this.MakePlaceTile(0, 0));
        }

        [Fact]
        public void Given_BoardHasInstanceofTile_When_PlaceSameInstanceAgain_Then_False()
        {
            var action = MakePlaceTile(0, 0);
            var board = new[] { action }.ToDictionary(
                a => a.Point,
                a => a.Placement
            );

            var engine = this.MockEngine(new Board(board));

            var secondPlaceTileGameAction = new PlaceTileGameAction(
                new Point(0, 0),
                new Placement(action.Placement.Tile, Orientation.Top)
            );

            this.AssertFalse<UniqueTileInstanceRule>(engine, secondPlaceTileGameAction);
        }
    }
}
