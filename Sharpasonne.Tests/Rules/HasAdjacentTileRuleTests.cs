using Sharpasonne.GameActions;
using Sharpasonne.Rules;
using Xunit;
using Moq;
using Sharpasonne.Models;

namespace Sharpasonne.Tests.Rules
{
    public class HasAdjacentTileRuleTests : RuleUnitTest<PlaceTileGameAction>
    {
        [Fact]
        public void Given_BoardIsEmpty_When_PlacingATile_Then_Validates()
        {
            var mockEngine = new Mock<IEngine>();
            mockEngine
                .Setup(e => e.Board)
                .Returns(new Board());

            AssertTrue<HasAdjacentTileRule>(mockEngine.Object, MakePlaceTile(0, 0));
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
