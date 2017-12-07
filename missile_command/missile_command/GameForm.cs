using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace missile_command
{
	public partial class GameForm : Form
	{
		Microsoft.DirectX.Direct3D.Device device;
		Microsoft.DirectX.DirectDraw.Surface surface;

		int fps = 0, frames = 0;
		long timeStarted = Environment.TickCount;
		Graphics g;

		List<GameObject> objectList = new List<GameObject>();

		// Methods
		public GameForm()
		{
			InitializeComponent();
			Main();
		}
		private void Main()
		{
			InitDevice();

			// Unit tests?
			//objectList.Add(GameObjectFactory.MakeBomb(new Point(0, 0), new Point(500, 1080), new Dimensions(1, 1), PType.ENEMY));
			//this.Invalidate();
		}
		private void InitForm()
		{
			// Maximize and hide everything else
			this.StartPosition = FormStartPosition.Manual;
			int height = Screen.PrimaryScreen.Bounds.Height;
			int width = Screen.PrimaryScreen.Bounds.Width;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.ClientSize = new Size(width, height);
		}
		private void InitDevice()
		{
			PresentParameters pp = new PresentParameters();
			pp.Windowed = true;
			pp.SwapEffect = SwapEffect.Discard;
			//pp.DeviceWindow.Width = Screen.PrimaryScreen.Bounds.Width;
			//pp.DeviceWindow.Height = Screen.PrimaryScreen.Bounds.Height;

			device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, pp);
		}
		private void StartThread()
		{

		}
		private void Play()
		{
			device.Clear(ClearFlags.Target, Color.Black, 0, 1);

			surface.DrawEllipse(10, 10, 10, 10);

			device.Present();

			/*
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
			//Console.Out.WriteLine(fps);*/
		}
		private void GameForm_Paint(object sender, PaintEventArgs e)
		{
			Play();
			/*
			//https://www.youtube.com/watch?v=AC9xeiiSD-o
			// This is the Frames thing that i am trying to get to work

			// Use directX and threading to elimate this.
			//https://www.youtube.com/watch?v=M4IywpwxdKg&list=PLizgKJ9dhxgwwSFCCLI8bqOrpEFCRcS9U
			try
			{
				g = e.Graphics;

				for (int i = 0; i < objectList.Count; i++)
				{
					objectList[i].Draw(g);
				}
				Play();
			}
			catch (Exception ex)
			{
				Close();
			}
			*/
		}
	}
}
