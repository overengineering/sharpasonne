using System.Linq;
using Optional.Unsafe;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Xunit;
using Moq;

namespace Sharpasonne.Tests.Rules
{
    public abstract class UnitTest<TGameAction>
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

        protected IEngine MockEngine(Board board)
        {
            var engine = new Mock<IEngine>();

            engine
                .Setup(mockEngine => mockEngine.Board)
                .Returns(board);

            return engine.Object;
        }

        public PlaceTileGameAction MakePlaceTile(
            int x,
            int y)
        {
            var tile = new TileBuilder()
                .CreateTile(Enumerable.Empty<IFeature>())
                .ValueOrFailure();

            return new PlaceTileGameAction(
                new Point(x, y),
                new Placement(tile, Orientation.Top)
            );
        }
    }
}

