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
    public class HasAdjacentTileRuleTests : UnitTest<PlaceTileGameAction>
    {
        [Fact]
        public void Given_BoardIsEmpty_When_PlacingATile_Then_Validates()
        {
            AssertTrue<HasAdjacentTileRule>(new Engine(), MakePlaceTile(0, 0));
        }

        [Fact]
        public void Given_AdjacentTile_When_PlacingATile_Then_Validates()
        {
            var action = MakePlaceTile(0, 0);
            var board = MakeBoard(action);

            AssertTrue<HasAdjacentTileRule>(
                MockEngine(board),
                MakePlaceTile(1, 0)
            );
        }

        [Fact]
        public void Given_NoAdjacentTiles_When_PlacingATile_Then_DoesNotValidate()
        {
            var action = MakePlaceTile(0, 0);
            var board = MakeBoard(action);

            AssertFalse<HasAdjacentTileRule>(
                MockEngine(board),
                MakePlaceTile(2, 0)
            );
        }
    }
}
