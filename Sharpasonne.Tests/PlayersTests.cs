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
    public class PlayersTests : UnitTest
    {
        [Fact]
        public void Given_ZeroPlayers_When_CreatingPlayers_Then_NoneIsArgumentOutOfRangeException()
        {
            Players.Create(0).MatchNone(exception => Assert.IsType<ArgumentOutOfRangeException>(exception));
        }

        [Fact]
        public void Given_1Player_When_CreatingPlayers_Then_NoneIsArgumentOutOfRangeException()
        {
            Players.Create(1).MatchNone(exception => Assert.IsType<ArgumentOutOfRangeException>(exception));
        }

        [Fact]
        public void Given_2Players_When_CreatingPlayers_Then_SomeIsReturned()
        {
            Assert.NotNull(Players.Create(2).ValueOrFailure());
        }

        [Fact]
        public void Given_6Players_When_CreatingPlayers_Then_NoneIsArgumentOutOfRangeException()
        {
            Players.Create(6).MatchNone(exception => Assert.IsType<ArgumentOutOfRangeException>(exception));
        }

        [Fact]
        public void Given_NumberOutsideRange_When_FindingNextPlayer_Then_Is1()
        {
            Assert.Equal(1, Players.Create(2).ValueOrFailure().NextPlayer(0));
            Assert.Equal(1, Players.Create(2).ValueOrFailure().NextPlayer(3));
        }

        [Fact]
        public void Given_Player1_When_FindingNextPlayer_Then_Is2()
        {
            Assert.Equal(2, Players.Create(2).ValueOrFailure().NextPlayer(1));
        }

        [Fact]
        public void Given_LastPlayerInBound_When_FindingNextPlayer_Then_Is1()
        {
            int count = 4;
            Assert.Equal(1, Players.Create(count).ValueOrFailure().NextPlayer(count));
        }
    }
}
