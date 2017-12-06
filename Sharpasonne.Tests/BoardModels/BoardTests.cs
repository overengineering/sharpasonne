using Xunit;

using Sharpasonne.BoardModels;

namespace Sharpasonne.Tests.BoardModels
{
    public class BoardTests
    {
        [Fact]
        public void Given_ADefaultBoard_Then_IsEmpty()
        {
            var board = new Board();

            var grid = board.ToImmutableDictionary();

            Assert.Empty(grid);
        }
    }
}
