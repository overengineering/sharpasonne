using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography;
using Sharpasonne.GameActions;
using Sharpasonne.Rules;
using Xunit;
using Moq;
using Optional.Unsafe;
using Sharpasonne.Models;
using Sharpasonne.Models.Features;

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
