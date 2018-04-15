namespace ReversiGame.Entities
{
    public partial class Board
    {
        public static int MinWidth => 3;
        public static int MaxWidth => 26;
        public static int MinHeight => 0;
        public static int MaxHeight => 26;

        private DiscTypes[][] State { get; }
        public int Width { get; }
        public int Height { get; }

        public Board(DiscTypes[][] state)
        {
            State = state;
            Width = state.Length;
            Height = state[0].Length;
        }

        public Cell this[int row, int column] =>
            new Cell {Column = (char) ('A' + column), Row = row + 1, Disc = State[row][column]};
    }
}