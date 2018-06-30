using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
#using asd
namespace Gomoku
{
    class Game
    { 
        private Board board = new Board();
        private PieceType currentPlayer = PieceType.Black;
        private PieceType winner = PieceType.None;
        public PieceType Winner { get { return winner; } }

        public bool CanBePlaced(int x, int y)
        {
            return board.CanBePlaced(x, y);
        }

        public Piece PlaceAPiece(int x, int y)
        {
            Piece piece = board.PlaceAPiece(x, y, currentPlayer);
            if (piece != null)
            {
                /// 檢查下棋者是否獲勝
                CheckWinner();

                /// 交換選手
                if (currentPlayer == PieceType.Black)
                    currentPlayer = PieceType.White;
                else
                    currentPlayer = PieceType.Black; 
                return piece;
            }

            return null;
        }


        public void CheckWinner()
        {

            int centerX = board.LastPlacedNode.X;
            int centerY = board.LastPlacedNode.Y; 
            for (int xDir = -1; xDir <= 1; xDir++)
            {
                for (int yDir = -1; yDir <= 1; yDir++)
                {
                    if (xDir == 0 && yDir == 0)
                        continue;

                    int count = 1;
                    while (count < 5)
                    {
                        int targetX = centerX + count*xDir;
                        int targetY = centerY + count*yDir;

                        if (targetX >= Board.NODE_COUNT || targetX < 0 ||
                            targetY >= Board.NODE_COUNT || targetY < 0 ||
                            board.GetPieceType(targetX, targetY) != currentPlayer)
                            break;

                        count++;

                    }
                    // 檢查是否有五顆棋子
                    if (count == 5)
                        winner = currentPlayer;

                }
            }

            






        }

    }
}
