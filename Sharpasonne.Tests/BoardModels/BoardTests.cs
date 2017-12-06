using Xunit;

using Sharpasonne.Models;

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

        /*[Fact]
        public void When_AddingATileToAnEmptyBoard_Then_BoardHasSameTile()
        {
            var board = new Board();
            board.Set()
        }*/
    }
}
