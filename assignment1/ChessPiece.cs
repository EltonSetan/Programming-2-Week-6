using System;

namespace ChessGameAssignment
{
    public class ChessPiece
    {
        public ChessPieceColor Color { get; }
        public ChessPieceType Type { get; }

        public ChessPiece(ChessPieceColor color, ChessPieceType type)
        {
            Color = color;
            Type = type;
        }

        public bool IsValidMove(Position current, Position target)
        {
            int rowDiff = Math.Abs(target.Row - current.Row);
            int colDiff = Math.Abs(target.Column - current.Column);

            switch (Type)
            {
                case ChessPieceType.Pawn:
                    // Pawns can only move forward one or two squares on their first move
                    // and one square forward on subsequent moves
                    if (colDiff == 0 && ((Color == ChessPieceColor.White && target.Row == current.Row + 1) || (Color == ChessPieceColor.Black && target.Row == current.Row - 1)))
                    {
                        return true;
                    }
                    // Pawns can capture pieces diagonally, one square forward
                    if (rowDiff == 1 && colDiff == 1)
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.Rook:
                    // Rooks can move any number of squares horizontally or vertically
                    if (rowDiff == 0 || colDiff == 0)
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.Knight:
                    // Knights can move two squares in one direction and one square in the other direction
                    if ((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2))
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.Bishop:
                    // Bishops can move any number of squares diagonally
                    if (rowDiff == colDiff)
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.Queen:
                    // Queens can move any number of squares horizontally, vertically, or diagonally
                    if (rowDiff == 0 || colDiff == 0 || rowDiff == colDiff)
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.King:
                    // Kings can move one square in any direction
                    if (rowDiff <= 1 && colDiff <= 1)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }

            return false;
        }
    }
}
