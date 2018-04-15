using ReversiGame.Entities;

namespace ReversiGame.Helpers
{
    public class BoardBilder
    {
        public static Board CreateBoard(int width, int height, string[][] state)
        {
            var boardState = new DiscTypes[height][];

            for (var i = 0; i < height; i++)
            {
                boardState[i] = new DiscTypes[width];

                for (var j = 0; j < width; j++)
                {
                    boardState[i][j] = state[i][j].ToDiscType();
                }
            }

            return new Board(boardState);
        }
    }
}
