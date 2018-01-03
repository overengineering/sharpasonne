using Sharpasonne.GameActions;
using Sharpasonne.Rules;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Xunit;

namespace Sharpasonne.Tests.Rules
{
    public class SpaceIsEmptyRuleTests
    {
        [Fact]
        public void When_TileIsEmpty_Then_True()
        {
            Assert.True(new SpaceIsEmptyRule().Verify(new Engine(), new PlaceTileGameAction()));
        }

        [Fact]
        public void When_TileIsEmpty_Then_False()
        {
            var engine = new Engine();

            Assert.True(new SpaceIsEmptyRule().Verify(engine, new PlaceTileGameAction()));
        }
    }
}
