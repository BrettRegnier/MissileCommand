﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace missile_command
{
	public partial class Window : Form
	{
		private State currState;
		private State nextState;

		// TODO move into gameManager
		// TODO make wave game mode
		private int fps = 0, frames = 0;
		private long tickCount = Environment.TickCount;
		private long elapsedTime = Environment.TickCount;

		public Window()
		{
			this.InitializeComponent();
			Main();
		}
		private void Main()
		{
			InitForm();
			LoadContent();

			this.Invalidate(); // Start the game.
		}
		private void InitForm()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);

			// Maximize and hide everything else
			StartPosition = FormStartPosition.Manual;
			FormBorderStyle = FormBorderStyle.None;

			this.ClientSize = Utils.gameBounds;
		}
		private void LoadContent()
		{
			currState = new MenuState(this);
		}
		public void NextState(State state)
		{
			nextState = state;
		}
		private void Loop()
		{
			try
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
						this.Update();
						this.Invalidate();
						break;
					}
				}
			}
			catch (NotImplementedException ex)
			{
				MessageBox.Show(ex.Message);
				Loop();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				this.Close();
			}
		}
		private new void Update()
		{
			if (nextState != null)
			{
				currState = nextState;
				nextState = null;
			}

			currState.Update(elapsedTime);
			currState.PostUpdate(elapsedTime);
		}
		private void Draw(object sender, PaintEventArgs e)
		{
			currState.Draw(e.Graphics);
			frames++;

			// Update after drawing
			Loop();
		}
		private void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			KeypressHandler.Instance.KeyDown(e);
		}
		private void GameForm_KeyUp(object sender, KeyEventArgs e)
		{
			KeypressHandler.Instance.KeyUp(e);
		}
	}
}
