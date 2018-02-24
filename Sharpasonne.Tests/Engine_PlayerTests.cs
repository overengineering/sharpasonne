using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Optional.Unsafe;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Sharpasonne.Rules;
using Sharpasonne.Tests.Rules;
using Moq;
using Xunit;

namespace Sharpasonne.Tests
{
    public class Engine_PlayerTests : UnitTest<PlaceTileGameAction>
    {
        protected Engine CreateEngine(int players)
        {
            var mockPlayers = new Mock<Players>();

            mockPlayers.SetupGet(e => e.Count).Returns(players);

            var option = Engine.Create(
                ImmutableQueue<IGameAction>.Empty,
                new Dictionary<Type, IImmutableList<IRule<IGameAction>>> {
                    [typeof(PlaceTileGameAction)] = ImmutableList<IRule<IGameAction>>.Empty
                }.ToImmutableDictionary(),
                mockPlayers.Object
            );

            return option.ValueOrFailure();
        }

        [Fact]
        public void Given_2Players_When_FirstTurn_Then_NextPlayerIsFirstPlayer()
        {
            Assert.Equal(1, CreateEngine(players: 2).CurrentPlayerTurn);
        }

        [Fact]
        public void Given_2Players_When_SecondTurn_Then_NextPlayerIsSecondPlayer()
        {
            var firstTurn = CreateEngine(2);
            var secondTurn = firstTurn.Perform(MakePlaceTile(0, 0)).ValueOrFailure();
            Assert.Equal(2, secondTurn.CurrentPlayerTurn);
        }

        [Fact]
        public void Given_2Players_When_ThirdTurn_Then_NextPlayerIsFirstPlayerAgain()
        {
            var firstTurn = CreateEngine(2);
            var secondTurn = firstTurn.Perform(MakePlaceTile(0, 0)).ValueOrFailure();
            var thirdTurn = secondTurn.Perform(MakePlaceTile(0, 1)).ValueOrFailure();
            Assert.Equal(1, thirdTurn.CurrentPlayerTurn);
        }
    }
}
