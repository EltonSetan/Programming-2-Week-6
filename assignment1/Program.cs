using System;

namespace ChessGameAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            ChessGame game = new ChessGame();
            game.InitChessboard();
            game.PlayChess();
        }
    }
}
