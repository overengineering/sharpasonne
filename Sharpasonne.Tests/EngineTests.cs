using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Optional.Unsafe;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Sharpasonne.Rules;
using Sharpasonne.Tests.Rules;
using Xunit;

namespace Sharpasonne.Tests
{
    public class EngineTests : UnitTest
    {
        class DummyRule : IRule<IGameAction>
        {
            public bool Verify<T1>(IEngine engine, T1 gameAction) where T1 : IGameAction
            {
                throw new NotImplementedException();
            }
        }

        class DummyGameAction : IGameAction
        {
            public IEngine Perform(IEngine engine)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void When_CreatingAnEngine_Then_BoardIsNotNull()
        {
            var engine = new Engine(
                new RuleMapBuilder(),
                Players.Create(2).ValueOrFailure());

            Assert.NotNull(engine.Board);
        }

        [Fact]
        public void When_PlacingFirstTile_Then_ReturnsANewState()
        {
            // Arrange
            var engine = new Engine(
                new RuleMapBuilder().Set(ImmutableList<IRule<PlaceTileGameAction>>.Empty),
                Players.Create(2).ValueOrFailure());

            // Act
            var newState = engine.Perform(MakePlaceTile(0, 0));

            // Assert
            Assert.True(newState.HasValue);
        }

        [Fact]
        public void When_PlacingFirstTile_Then_ReturnsNewStateWithSinglePlacedTile()
        {
            // Arrange
            var engine = new Engine(
                new RuleMapBuilder().Set(ImmutableList<IRule<PlaceTileGameAction>>.Empty),
                Players.Create(2).ValueOrFailure());

            // Act
            var newState = engine.Perform(MakePlaceTile(0, 0));

            // Assert
            Assert.Equal(1, newState.ValueOrFailure().Board.ToImmutableDictionary().Count);
        }
    }
}
