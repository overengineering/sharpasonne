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
            AssertTrue<UniqueTileInstanceRule>(new Engine(), MakePlaceTile(0, 0));
        }

        [Fact]
        public void Given_BoardHasInstanceofTile_When_PlaceSameInstanceAgain_Then_False()
        {
            var firstAction  = MakePlaceTile(0, 0);
            var board        = MakeBoard(firstAction);
            var engine       = MockEngine(board);
            var secondAction = MakePlaceTile(0, 0, firstAction.Placement.Tile);

            AssertFalse<UniqueTileInstanceRule>(engine, secondAction);
        }
    }
}
