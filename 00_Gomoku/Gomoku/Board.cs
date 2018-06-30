using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gomoku
{
    class Board
    {

        public static readonly int NODE_COUNT = 15;
        private static readonly int OFFSET = 22;
        private static readonly int NODE_RADIUS = 10;
        private static readonly int DISTANCE = 35;

        private static readonly Point NO_MATCH_POINT = new Point(-1, -1);
        private Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT];

        private Point lastPlacedNode = NO_MATCH_POINT;
        public Point LastPlacedNode { get { return lastPlacedNode; } } 


        public PieceType GetPieceType(int nodeIX,int nodeIY)
        {
            if (pieces[nodeIX, nodeIY] == null)
                return PieceType.None;

            return pieces[nodeIX, nodeIY].GetPieceType();
        }

        public bool CanBePlaced(int x,int y)
        {
            /// 找最靠近Node的點
            Point nodeId = findTheColsetNode(x, y);
            /// 沒有靠近Node的點回傳 false
            if (nodeId == NO_MATCH_POINT)
                return false;
            /// 檢查棋子是否存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return false;

            return true;
        }

        public Piece PlaceAPiece(int x,int y,PieceType type)
        {
            /// 找最靠近Node的點
            Point nodeId = findTheColsetNode(x, y);
            /// 沒有靠近Node的點回傳 false
            if (nodeId == NO_MATCH_POINT)
                return null;
            /// 檢查棋子是否存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return null;

            Point newPoint = shiftProcess(nodeId);

            /// 根據type產生對應的棋子
            if (type == PieceType.Black)
                pieces[nodeId.X,nodeId.Y] = new BlackPiece(newPoint.X, newPoint.Y);
            else if(type == PieceType.White) 
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(newPoint.X, newPoint.Y);

            lastPlacedNode = nodeId;
            return pieces[nodeId.X, nodeId.Y];
        }

        private Point shiftProcess(Point nodeId)
        {
            int newX = OFFSET + nodeId.X * DISTANCE;
            int newY = OFFSET + nodeId.Y * DISTANCE;

            return new Point(newX, newY);

        }

        private  Point findTheColsetNode(int x,int y)
        {
            int NodeX = findTheColsetNode(x);
            if (NodeX == -1 || NodeX >= NODE_COUNT)
                return NO_MATCH_POINT;

            int NodeY = findTheColsetNode(y);
            if (NodeY == -1 || NodeY >= NODE_COUNT)
                return NO_MATCH_POINT; 

            return new Point(NodeX, NodeY);
        }
        private int findTheColsetNode(int pos)
        {
            pos -= OFFSET;
            int ans = pos / DISTANCE;
            int res = pos % DISTANCE;

            if (res <= NODE_RADIUS)
                return ans;
            else if (res >= DISTANCE - NODE_RADIUS)
                return ans + 1;
            else
                return -1;
        }

    }
}
