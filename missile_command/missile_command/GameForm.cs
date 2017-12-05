using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace missile_command
{
    using System.Drawing;
    public partial class GameForm : Form
	{
		private Graphics g;
        private List<Bomb> bombList = new List<Bomb>();

		public GameForm()
		{
			InitializeComponent();
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
		}

		private void GameForm_Load(object sender, EventArgs e)
		{
			try
			{

				// Maximize and hide everything else
				this.StartPosition = FormStartPosition.Manual;
				int height = Screen.PrimaryScreen.Bounds.Height;
				int width = Screen.PrimaryScreen.Bounds.Width;
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

				this.ClientSize = new Size(width, height);
				//this.ClientSize = new Size(1366, 768);

				//Bring them back in when it is all done.
				//MakeMenus(); // Seperate File
				//AddPlayerObjects(); // Seperate File
				//AddBuildings(); // Seperate File

				// Once everything is initialized, enable timer.
				GameTimer.Enabled = true;
                // Unit tests?
                bombList.Add(BombFactory.MakeBomb(new Point(0, 0), new Point(500, 1080), new Dimensions(1, 1), g, PType.ENEMY));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void GameForm_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				g = e.Graphics;

                for (int i = 0; i < bombList.Count; i++)
                {
                    bombList[i].Move();
                    bombList[i].Draw();
                }
			}
			catch (Exception ex)
			{
                Close();
			}
		}

		private void GameTimer_Tick(object sender, EventArgs e)
		{
            this.Invalidate(false);
		}
	}
}
