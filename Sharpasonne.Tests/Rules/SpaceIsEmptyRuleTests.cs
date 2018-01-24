﻿using System.Linq;
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
            AssertTrue<SpaceIsEmptyRule>(new Engine(), MakePlaceTile(0, 0));
        }

        [Fact]
        public void When_TileIsNotEmpty_Then_False()
        {
            var action = MakePlaceTile(0, 0);
            var board = new[] {action}.ToDictionary(
                a => a.Point,
                a => a.Placement
            );

            AssertFalse<SpaceIsEmptyRule>(
                MockEngine(new Board(board)),
                action
            );
        }
    }
}
