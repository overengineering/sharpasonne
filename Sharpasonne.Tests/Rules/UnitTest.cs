using System.Linq;
using Optional.Unsafe;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Xunit;

namespace Sharpasonne.Tests.Rules
{
    public abstract class UnitTest
    {
        protected void AssertPlaceTileTrue<TRule>(Engine engine, PlaceTileGameAction action)
            where TRule : IRule<PlaceTileGameAction>, new()
        {
            Assert.True(new TRule().Verify(engine, action));
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

