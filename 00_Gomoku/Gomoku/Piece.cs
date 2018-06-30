using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Gomoku
{
    abstract class Piece : PictureBox
    {
        private static int PICTUREBOX_SIZE = 32;

        public Piece(int x, int y)
        {
            this.BackColor = Color.Transparent; 
            this.Size = new Size(PICTUREBOX_SIZE, PICTUREBOX_SIZE);
            this.Location = new Point(x - PICTUREBOX_SIZE / 2, y - PICTUREBOX_SIZE / 2);
        }

        public abstract PieceType GetPieceType();

    }
}
