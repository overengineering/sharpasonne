using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
                ImmutableDictionary<Type, IImmutableList<IRule>>.Empty);

            Assert.NotNull(engine.Board);
        }

        [Fact]
        public void Given_ARuleSetWithANonGameActionKey_When_CreatingAnEngine_Then_Throw()
        {
            var ruleSet = new Dictionary<Type, IImmutableList<IRule>>
            {
                [typeof(string)] = ImmutableList.Create<IRule>(new DummyRule())
            }.ToImmutableDictionary();


            var exception = Record.Exception(() => new Engine(
                ImmutableQueue<IGameAction>.Empty,
                ruleSet));

            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

        [Fact]
        public void Given_ARuleSetWithAGameActionKey_When_CreatingAnEngine_Then_DontThrow()
        {
            var ruleSet = new Dictionary<Type, IImmutableList<IRule>>
            {
                [typeof(DummyGameAction)] = ImmutableList.Create<IRule>(new DummyRule())
            }.ToImmutableDictionary();


            var exception = Record.Exception(() => new Engine(
                ImmutableQueue<IGameAction>.Empty,
                ruleSet));

            Assert.Null(exception);
        }

        class DummyRule : IRule
        {
            public bool Verify<T>(Engine engine, T gameAction) where T : IGameAction
            {
                throw new System.NotImplementedException();
            }
        }

        class DummyGameAction : IGameAction
        {
        }
    }
}
