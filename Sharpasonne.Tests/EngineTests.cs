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
            var engine = Engine
                .Create(
                    ImmutableQueue<IGameAction>.Empty,
                    ImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>>.Empty,
                    Players.Create(2).ValueOrFailure())
                .ValueOrFailure();

            Assert.NotNull(engine.Board);
        }

        [Fact]
        public void Given_ARuleSetWithANoneGameActionKey_When_CreatingAnEngine_Then_None()
        {
            var ruleSet = new Dictionary<Type, IImmutableList<IRule<IGameAction>>> {
                [typeof(string)] = ImmutableList.Create<IRule<IGameAction>>(new DummyRule())
            };

            var option = Engine.Create(
                ImmutableQueue<IGameAction>.Empty,
                ruleSet.ToImmutableDictionary(),
                Players.Create(2).ValueOrFailure());

            Assert.False(option.HasValue);
            option.MatchNone((exception) => Assert.IsType<ArgumentOutOfRangeException>(exception));
        }

        [Fact]
        public void Given_ARuleSetWithAGameActionKey_When_CreatingAnEngine_Then_Some()
        {
            var ruleSet = new Dictionary<Type, IImmutableList<IRule<IGameAction>>> {
                [typeof(DummyGameAction)] = ImmutableList.Create<IRule<IGameAction>>(new DummyRule())
            };

            var option = Engine.Create(
                ImmutableQueue<IGameAction>.Empty,
                ruleSet.ToImmutableDictionary(),
                Players.Create(2).ValueOrFailure());

            Assert.True(option.HasValue);
        }

        [Fact]
        public void When_PlacingFirstTile_Then_ReturnsANewState()
        {
            // Arrange
            var rules = new Dictionary<Type, IImmutableList<IRule<IGameAction>>> {
                [typeof(PlaceTileGameAction)] = ImmutableList<IRule<IGameAction>>.Empty
            };

            var engine = Engine
                .Create(
                    ImmutableQueue<IGameAction>.Empty,
                    rules.ToImmutableDictionary(),
                    Players.Create(2).ValueOrFailure())
                .ValueOrFailure();

            // Act
            var newState = engine.Perform(MakePlaceTile(0, 0));

            // Assert
            Assert.True(newState.HasValue);
        }

        [Fact]
        public void When_PlacingFirstTile_Then_ReturnsNewStateWithSinglePlacedTile()
        {
            // Arrange
            var rules = new Dictionary<Type, IImmutableList<IRule<IGameAction>>> {
                [typeof(PlaceTileGameAction)] = ImmutableList<IRule<IGameAction>>.Empty
            };

            var engine = Engine
                .Create(
                    ImmutableQueue<IGameAction>.Empty,
                    rules.ToImmutableDictionary(),
                    Players.Create(2).ValueOrFailure())
                .ValueOrFailure();

            // Act
            var newState = engine.Perform(MakePlaceTile(0, 0));

            // Assert
            Assert.Equal(1, newState.ValueOrFailure().Board.ToImmutableDictionary().Count);
        }
    }
}
