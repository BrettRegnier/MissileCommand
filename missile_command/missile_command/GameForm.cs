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
		// TODO make wave game mode

		private Random rand = new Random();
		private Point gameBounds;

		private int fps = 0, frames = 0;
		private long tickCount = Environment.TickCount;
		private long elapsedTime = Environment.TickCount;

		List<List<GameObject>> objectLists = new List<List<GameObject>>();

		// Methods
		public GameForm()
		{
			InitializeComponent();
			Main();
		}

		private void Main()
		{
			InitGame();
			CreatePlayers();
			this.Invalidate(); // Start the game.
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
			gameBounds = new Point(width - Utils.SCREEN_OFFSET, height - Utils.SCREEN_OFFSET);

			for (int i = 0; i < 3; i++)
				objectLists.Add(new List<GameObject>());
		}
		private void CreatePlayers()
		{
			// Make players.... 
			// TODO make logic for creating players objects.
			List<Player> pl = new List<Player>();
			Point ori = new Point(200, 200);
			Player p = GameObjectFactory.MakePlayer(ori, PType.PLAYER1);
			p.TurretShoot += P_TurretShoot;
			objectLists[(int)ListType.PLAYER].Add(p);
			pl.Add(p);

			KeypressHandler.Instance().Initialize(pl);
		}
		private void P_TurretShoot(Point origin, Point destination)
		{
			// TODO maybe need to pass in a player type.
			Bomb b = GameObjectFactory.MakeBomb(origin, destination, PType.PLAYER1);
			b.DestroyBomb += DestroyGameObject;
			objectLists[(int)PType.PLAYER1].Add(b);
		}
		private void Loop()
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
		private void UpdateGame(object sender, PaintEventArgs e)
		{
			try
			{
				SpawnEnemies();
				for (int i = 0; i < objectLists.Count; i++)
				{
					for (int j = 0; j < objectLists[i].Count; j++)
					{
						objectLists[i][j].Draw(e.Graphics);
					}
				}
				frames++;

				Player.DrawBase(e.Graphics);
				KeypressHandler.Instance().MoveCursor();

				// Update after drawing
				Loop();
			}
			catch (Exception ex)
			{
			}
		}
		private void SpawnEnemies()
		{
			// TODO complex calculation based on ticks to determine when the next spawn is.
			if (Environment.TickCount >= elapsedTime + 1000)
			{
				Point spawnPoint = new Point(rand.Next(0, gameBounds.X), 0);
				// TODO make a list of guaranteed points
				Point destination = new Point(rand.Next(0, gameBounds.X), gameBounds.Y);
				Bomb bmb = GameObjectFactory.MakeBomb(spawnPoint, destination, PType.ENEMY);
				bmb.DestroyBomb += DestroyGameObject;
				objectLists[(int)ListType.E_BOMB].Add(bmb);
			}
		}
		private void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			KeypressHandler.Instance().KeyDown(e);
		}
		private void GameForm_KeyUp(object sender, KeyEventArgs e)
		{
			KeypressHandler.Instance().KeyUp(e);
		}
		private void DestroyGameObject(ListType lt, GameObject gameObject)
		{
			objectLists[(int)lt].Remove(gameObject);
		}
	}
}
