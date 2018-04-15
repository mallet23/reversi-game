using System;
using System.IO;
using ReversiGame.Entities;
using ReversiGame.Helpers;

namespace ReversiGame
{
    public class Solution
    {
        public static string PlaceToken(string board)
        {
            using (var reader = new StringReader(board))
            {
                var settings = reader.ReadLine()?.Split(' ');
                if (settings?.Length != 2)
                {
                    throw new Exception("Wrong board settings!");
                }

                if (!int.TryParse(settings[0], out var width)
                    || width < Board.MinWidth || width > Board.MaxWidth)

                {
                    throw new Exception("Wrong board width!");
                }

                if (!int.TryParse(settings[1], out var height)
                    || height < Board.MinHeight || height > Board.MaxHeight)
                {
                    throw new Exception("Wrong board height!");
                }

                var state = new string[height][];
                string row;
                var i = 0;
                while ((row = reader.ReadLine()) != null)
                {
                    state[i] = row.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    i++;
                }
                if (state[0].Length != width)
                {
                    throw new Exception("Wrong board height!");
                }

                var gameBoard = BoardBilder.CreateBoard(width, height, state);

                var game =  new Game(gameBoard);

                var positions = game.FindGreedyPositions();

                return
                    $"Positions: {string.Join(", ", positions.Points)}. Amount of discs to take over: {positions.Count}";
            }
        }
        
    }
}