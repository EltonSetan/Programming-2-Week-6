using System;

namespace ChessGameAssignment
{
    public class ChessGame
    {
        private ChessPiece[,] chessboard;
        private const int BoardSize = 8;
        private const int DisplayOffset = 1;

        public void InitChessboard()
        {
            chessboard = new ChessPiece[BoardSize, BoardSize];
            PutChessPieces();
        }

        public void PutChessPieces()
        {
            ChessPieceType[] order = { ChessPieceType.Rook, ChessPieceType.Knight, ChessPieceType.Bishop, ChessPieceType.Queen, ChessPieceType.King, ChessPieceType.Bishop, ChessPieceType.Knight, ChessPieceType.Rook };

            for (int i = 0; i < 8; i++)
            {
                chessboard[1, i] = new ChessPiece(ChessPieceColor.White, ChessPieceType.Pawn);
                chessboard[6, i] = new ChessPiece(ChessPieceColor.Black, ChessPieceType.Pawn);
                chessboard[0, i] = new ChessPiece(ChessPieceColor.White, order[i]);
                chessboard[7, i] = new ChessPiece(ChessPieceColor.Black, order[i]);
            }
        }

        public void DisplayChessboard()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            for (int row = 0; row < 8; row++)
            {
                Console.Write($"{8 - row} ");
                for (int col = 0; col < 8; col++)
                {
                    Console.BackgroundColor = (row + col) % 2 == 0 ? ConsoleColor.Gray : ConsoleColor.DarkYellow;
                    DisplayChessPiece(chessboard[row, col]);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }

            // Print column letters
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  ");
            for (char c = 'a'; c <= 'h'; c++)
            {
                Console.Write($" {c} ");
            }
            Console.WriteLine();
        }


        public Position String2Position(string pos)
        {
            if (pos.Length != 2 || pos[0] < 'a' || pos[0] > 'h' || pos[1] < '1' || pos[1] > '8')
            {
                throw new ArgumentException($"invalid position {pos}");
            }

            int column = pos[0] - 'a';
            int row = 8 - int.Parse(pos[1].ToString());

            return new Position(row, column);
        }

        public bool CheckMove(Position from, Position to)
        {
            ChessPiece movingPiece = chessboard[from.Row, from.Column];
            ChessPiece targetPiece = chessboard[to.Row, to.Column];

            if (movingPiece == null)
            {
                throw new InvalidOperationException("No chess piece at the starting position.");
            }

            if (targetPiece != null && movingPiece.Color == targetPiece.Color)
            {
                throw new InvalidOperationException("Cannot capture your own piece.");
            }

            if (!movingPiece.IsValidMove(from, to))
            {
                throw new InvalidOperationException("Invalid move for the selected piece.");
            }

            // Add any additional rules or checks, such as checking for a check or illegal moves

            return true;
        }

        public void DoMove(Position from, Position to)
        {
            if (CheckMove(from, to))
            {
                ChessPiece movingPiece = chessboard[from.Row, from.Column];
                chessboard[to.Row, to.Column] = movingPiece;
                chessboard[from.Row, from.Column] = null;

                // Handle special moves like en passant, castling, and pawn promotion
                // Keep track of game state
            }
        }


        public void PlayChess()
        {
            while (true)
            {
                try
                {
                    DisplayChessboard();
                    Console.WriteLine("Enter a move (e.g. a2 a3):");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "stop")
                    {
                        break;
                    }

                    string[] splitInput = input.Split(' ');

                    if (splitInput.Length != 2)
                    {
                        throw new ArgumentException("Invalid input. Expected format: a2 a3");
                    }

                    Position from = String2Position(splitInput[0]);
                    Position to = String2Position(splitInput[1]);

                    ChessPiece piece = chessboard[from.Row, from.Column];
                    if (piece == null)
                    {
                        Console.WriteLine("No chess piece at from-position");
                        continue;
                    }

                    if (!piece.IsValidMove(from, to))
                    {
                        Console.WriteLine($"Invalid move for chess piece {piece.Type}");
                        continue;
                    }

                    ChessPiece targetPiece = chessboard[to.Row, to.Column];
                    if (targetPiece != null && targetPiece.Color == piece.Color)
                    {
                        Console.WriteLine("Can not take a chess piece of same color");
                        continue;
                    }

                    Console.WriteLine($"move from {splitInput[0]} to {splitInput[1]}");

                    DoMove(from, to);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



        private void DisplayChessPiece(ChessPiece chessPiece)
        {
            if (chessPiece == null)
            {
                Console.Write("   ");
                return;
            }

            Console.ForegroundColor = chessPiece.Color == ChessPieceColor.White ? ConsoleColor.White : ConsoleColor.Black;
            char symbol = chessPiece.Type.ToString()[0];

            if (chessPiece.Type == ChessPieceType.Knight)
            {
                symbol = 'N';
            }

            Console.Write($" {symbol} ");
        }
    }
}