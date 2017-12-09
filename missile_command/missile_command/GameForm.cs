using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace missile_command
{
	public partial class GameForm : Form
	{
		int fps = 0, frames = 0;
		long tickCount = Environment.TickCount;
		long elapsedTime = 0;

		List<GameObject> objectList = new List<GameObject>();

		// Methods
		public GameForm()
		{
			InitializeComponent();
			Main();
		}
		private void Main()
		{
			InitGame();

			// Unit tests?
			objectList.Add(GameObjectFactory.MakeBomb(new Point(0, 0), new Point(500, 1080), new Dimensions(1, 1), PType.ENEMY));
		}
		private void InitGame()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);

			// Maximize and hide everything else
			StartPosition = FormStartPosition.Manual;
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

			int height = Screen.PrimaryScreen.Bounds.Height;
			int width = Screen.PrimaryScreen.Bounds.Width;
			ClientSize = new Size(width, height);
		}
		private void Update()
		{
			while (true)
			{
				if (Environment.TickCount >= elapsedTime + 1000)
				{
					fps = frames;
					frames = 0;
					elapsedTime = Environment.TickCount;
					Console.Out.WriteLine("Frames: " + fps);
				}
				if (Environment.TickCount >= tickCount + 15)
				{
					tickCount = Environment.TickCount;
					this.Invalidate();
					break;
				}
			}
		}
		private void GameForm_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				for (int i = 0; i < objectList.Count; i++)
				{
					((Bomb)objectList[i]).Move();
					objectList[i].Draw(e.Graphics);
				}


				
				frames++;

				// Update after drawing
				Update();
			}
			catch (Exception ex)
			{
				Close();
			}
		}
	}
}
