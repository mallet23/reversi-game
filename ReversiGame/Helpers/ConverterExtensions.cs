using System.ComponentModel;
using ReversiGame.Entities;

namespace ReversiGame.Helpers
{
    public static class ConverterExtensions
    {
        public static DiscTypes ToDiscType(this string symbol)
        {
            switch (symbol)
            {
                case ".":
                    return DiscTypes.Empty;
                case "X":
                    return DiscTypes.X;
                case "O":
                    return DiscTypes.O;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
