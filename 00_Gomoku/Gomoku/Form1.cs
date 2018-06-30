using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Gomoku
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private Game game = new Game();






        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Piece piece = game.PlaceAPiece(e.X, e.Y);
            if (piece != null)
            {
                this.Controls.Add(piece);
                 
                if (game.Winner == PieceType.Black)
                    MessageBox.Show("黑色獲勝");
                else if (game.Winner == PieceType.White)
                    MessageBox.Show("白色獲勝");

            }


        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game.CanBePlaced(e.X, e.Y))
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;

        }
    }
}
