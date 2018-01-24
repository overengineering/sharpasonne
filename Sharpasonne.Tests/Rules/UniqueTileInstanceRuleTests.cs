using Sharpasonne.GameActions;
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
    }
}
