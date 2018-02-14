using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Sharpasonne.GameActions;
using Sharpasonne.Rules;
using Xunit;

namespace Sharpasonne.Tests
{
    public class EngineTests
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
        }
    }
}
