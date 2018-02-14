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
    public class EngineTests : UnitTest<PlaceTileGameAction>
    {
        [Fact]
        public void When_CreatingAnEngine_Then_BoardIsNotNull()
        {
            var engine = new Engine(
                ImmutableQueue<IGameAction>.Empty,
                ImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>>.Empty);

            Assert.NotNull(engine.Board);
        }

        [Fact]
        public void Given_ARuleSetWithANonGameActionKey_When_CreatingAnEngine_Then_Throw()
        {
            var ruleSet = new Dictionary<Type, IImmutableList<IRule<IGameAction>>>
            {
                [typeof(string)] = ImmutableList.Create<IRule<IGameAction>>(new DummyRule())
            }.ToImmutableDictionary();


            var exception = Record.Exception(() => new Engine(
                ImmutableQueue<IGameAction>.Empty,
                ruleSet));

            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public void Given_ARuleSetWithAGameActionKey_When_CreatingAnEngine_Then_DontThrow()
        {
            var ruleSet = new Dictionary<Type, IImmutableList<IRule<IGameAction>>>
            {
                [typeof(DummyGameAction)] = ImmutableList.Create<IRule<IGameAction>>(new DummyRule())
            }.ToImmutableDictionary();


            var exception = Record.Exception(() => new Engine(
                ImmutableQueue<IGameAction>.Empty,
                ruleSet));

            Assert.Null(exception);
        }

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
        public void When_PlacingFirstTile_Then_ReturnsANewState()
        {
            // Arrange
            var rules = new Dictionary<Type, IImmutableList<IRule<IGameAction>>>()
            {
                [typeof(PlaceTileGameAction)] = ImmutableList<IRule<IGameAction>>.Empty
            };

            var engine = new Engine(rules.ToImmutableDictionary());

            // Act
            var newState = engine.Perform(MakePlaceTile(0, 0));

            // Assert
            Assert.True(newState.HasValue);
        }

        [Fact]
        public void When_PlacingFirstTile_Then_ReturnsNewStateWithSinglePlacedTile()
        {
            // Arrange
            var rules = new Dictionary<Type, IImmutableList<IRule<IGameAction>>>()
            {
                [typeof(PlaceTileGameAction)] = ImmutableList<IRule<IGameAction>>.Empty
            };

            var engine = new Engine(rules.ToImmutableDictionary());

            // Act
            var newState = engine.Perform(MakePlaceTile(0, 0));

            // Assert
            Assert.Equal(1, newState.ValueOrFailure().Board.ToImmutableDictionary().Count);
        }
    }
}
