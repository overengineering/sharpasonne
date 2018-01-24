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
    public class AdjecentTileRuleTests : UnitTest<PlaceTileGameAction>
    {
        [Fact]
        public void Given_BoardIsEmpty_When_PlacingATile_Then_Validates()
        {
            AssertTrue<AdjecentTileRule>(new Engine(), MakePlaceTile(0, 0));
        }

        [Fact]
        public void Given_AdjecentTile_When_PlacingATile_Then_Validates()
        {
            var action = MakePlaceTile(0, 0);
            var board = new[] {action}.ToDictionary(
                a => a.Point,
                a => a.Placement
            );

            AssertTrue<AdjecentTileRule>(
                MockEngine(new Board(board)),
                MakePlaceTile(1, 0)
            );
        }

        [Fact]
        public void Given_NoAdjecentTiles_When_PlacingATile_Then_DoesNotValidate()
        {
            var action = MakePlaceTile(0, 0);
            var board = new[] {action}.ToDictionary(
                a => a.Point,
                a => a.Placement
            );

            AssertFalse<AdjecentTileRule>(
                MockEngine(new Board(board)),
                MakePlaceTile(2, 0)
            );
        }
    }
}
