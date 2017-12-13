using System.Linq;
using Sharpasonne;
using Sharpasonne.Models;
using Xunit;

namespace Sharparsonne.Models.Tests
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
