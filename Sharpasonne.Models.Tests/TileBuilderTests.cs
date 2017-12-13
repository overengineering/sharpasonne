using System.Linq;
using Xunit;

namespace Sharpasonne.Models.Tests
{
    public class TileBuilderTests
    {
        [Fact]
        void When_CreatingEmptyTile_Then_ReturnsSome()
        {
            var maybeTile = new TileBuilder()
                .CreateTile(Enumerable.Empty<IFeature>());

            Assert.True(maybeTile.HasValue);
        }
    }
}
