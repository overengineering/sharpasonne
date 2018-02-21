using Moq;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Sharpasonne.Rules;
using Xunit;

namespace Sharpasonne.Tests.Rules
{
    public abstract class RuleUnitTest<TGameAction> : UnitTest
        where TGameAction : IGameAction
    {
        protected void AssertTrue<TRule>(IEngine engine, TGameAction action)
            where TRule : IRule<TGameAction>, new()
        {
            Assert.True(new TRule().Verify(engine, action));
        }

        protected void AssertFalse<TRule>(IEngine engine, TGameAction action)
            where TRule : IRule<TGameAction>, new()
        {
            Assert.False(new TRule().Verify(engine, action));
        }
    }
}