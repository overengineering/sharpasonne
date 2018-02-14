using System.Linq;
using Moq;
using Sharpasonne.GameActions;
using Sharpasonne.Rules;
using Xunit;
using Sharpasonne.Models;

namespace Sharpasonne.Tests.Rules
{
    public class SpaceIsEmptyRuleTests : UnitTest<PlaceTileGameAction>
    {
        [Fact]
        public void When_TileIsEmpty_Then_True()
        {
            var mockEngine = new Mock<IEngine>();
            mockEngine
                .Setup(e => e.Board)
                .Returns(new Board());

            AssertTrue<SpaceIsEmptyRule>(mockEngine.Object, MakePlaceTile(0, 0));
        }

        [Fact]
        public void When_TileIsNotEmpty_Then_False()
        {
            var action = MakePlaceTile(0, 0);
            var board  = MakeBoard(action);
            var engine = MockEngine(board);

            AssertFalse<SpaceIsEmptyRule>(engine, action);
        }
    }
}
