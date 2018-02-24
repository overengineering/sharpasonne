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
    public class PlayersTests : UnitTest<PlaceTileGameAction>
    {
        [Fact]
        public void Given_ZeroPlayers_When_CreatingPlayers_Then_NonIsArgumentOutOfRangeException()
        {
            Players.Create(0).MatchNone(exception => Assert.IsType<ArgumentOutOfRangeException>(exception));
        }

        [Fact]
        public void Given_1Player_When_CreatingPlayers_Then_NonIsArgumentOutOfRangeException()
        {
            Players.Create(1).MatchNone(exception => Assert.IsType<ArgumentOutOfRangeException>(exception));
        }

        [Fact]
        public void Given_2Players_When_CreatingPlayers_Then_SomeIsReturned()
        {
            Assert.NotNull(Players.Create(2).ValueOrFailure());
        }

        [Fact]
        public void Given_6Players_When_CreatingPlayers_Then_NonIsArgumentOutOfRangeException()
        {
            Players.Create(6).MatchNone(exception => Assert.IsType<ArgumentOutOfRangeException>(exception));
        }
    }
}
