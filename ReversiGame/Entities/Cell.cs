namespace ReversiGame.Entities
{
    public partial class Board
    {
        public struct Cell
        {
            public char Column { get; set; }
            public int Row { get; set; }
            public DiscTypes Disc { get; set; }

            public override string ToString()
            {
                return Column + Row.ToString();
            }
        }
    }
}