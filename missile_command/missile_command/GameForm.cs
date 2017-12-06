using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX;

namespace missile_command
{
    using System.Drawing;
    public partial class GameForm : Form
    {
        int fps = 0, frames = 0;
        long timeStarted = Environment.TickCount;

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
                Main();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void Main()
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
            bombList.Add(GameObjectFactory.MakeBomb(new Point(0, 0), new Point(500, 1080), new Dimensions(1, 1), g, PType.ENEMY));
            this.Invalidate();
        }

        private void Play()
        {
            while (true)
            {
                if (frames >= 60)
                {
                    if (Environment.TickCount >= timeStarted)
                    {
                        Console.Out.WriteLine(Environment.TickCount - timeStarted);
                        Console.Out.WriteLine("Frames: " + frames);
                        fps = frames;
                        frames = 0;
                        timeStarted = Environment.TickCount;
                        break;
                    }
                }
                frames++;
                this.Invalidate();
            }
            //Console.Out.WriteLine(fps);
        }

		private void GameForm_Paint(object sender, PaintEventArgs e)
		{
            //https://www.youtube.com/watch?v=AC9xeiiSD-o
            // This is the Frames thing that i am trying to get to work

            // Use directX and threading to elimate this.
            //https://www.youtube.com/watch?v=M4IywpwxdKg&list=PLizgKJ9dhxgwwSFCCLI8bqOrpEFCRcS9U
            try
            {
				g = e.Graphics;

                for (int i = 0; i < bombList.Count; i++)
                {
                    bombList[i].Move();
                    bombList[i].Draw(g);
                }

                Play();
			}
			catch (Exception ex)
			{
                Close();
			}
		}

		private void GameTimer_Tick(object sender, EventArgs e)
		{
            //this.Invalidate(false);
		}
	}
}
