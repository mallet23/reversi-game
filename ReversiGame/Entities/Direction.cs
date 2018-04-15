namespace ReversiGame.Entities
{
    public class Direction
    {
        public Direction(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
}