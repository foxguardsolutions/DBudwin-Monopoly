namespace Monopoly
{
    using NUnit.Framework;

    [TestFixture]
    public class BoardTest
    {
        private readonly Board board = new Board();

        [Test(Description = "Make sure spaces are created and sorted in the right order")]
        public void TestCreateBoard()
        {
            for (int i = 0; i < board.Spaces.Count; i++)
            {
                Assert.AreEqual(i, board.Spaces[i].Position);
            }
        }

        [TestCase(Result = Board.NUMBER_OF_SPACES, Description = "Make sure the correct number of spaces are created")]
        public int TestNumberOfBoardSpaces()
        {
            return board.Spaces.Count;
        }
    }
}
