using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace missile_command
{
	public partial class GameForm : Form
	{
		// TODO make wave game mode

		private Random rand = new Random();

		private int fps = 0, frames = 0;
		private long tickCount = Environment.TickCount;
		private long elapsedTime = Environment.TickCount;

		LandMass lm;
		List<GameObject> lObject;
		List<Player> lPlayer;

		// Methods
		public GameForm()
		{
			InitializeComponent();
			Main();
		}

		private void Main()
		{
			// TODO make menu
			// TODO make pause menu
			InitForm();
			InitGame();
			InitPlayers(1);
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
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

			ClientSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
		}
		private void InitGame()
		{
			lm = new LandMass(); 
			lObject = new List<GameObject>();
			lPlayer = new List<Player>();
		}
		private void InitPlayers(int numPlayers)
		{
			for (int i = 0; i < numPlayers; i++)
			{
				Point ori = new Point((Utils.gameBounds.Width / 3) * (i + 1), 200);
				PType pt = PType.PLAYER;
				Account ap = Account.P1;
				if (i == 1)
					ap = Account.P2;
				else if (i == 2)
					ap = Account.P3;

				Player p = new Player(ori, pt, ap);
				lPlayer.Add(p);
			}

			Player currentPlayer = lPlayer[0];
			for (int i = 0; i < 3; i++)
			{
				if (numPlayers == 2)
				{
					if (i == 1)
						currentPlayer = lPlayer[i];
				}
				else if (numPlayers == 3)
					currentPlayer = lPlayer[i];

				Turret t = GameObjectFactory.MakeTurret(new Point(0, 0), PType.PLAYER, currentPlayer.GetAccount());
				t.TurretShoot += P_TurretShoot;
				lObject.Add(t);
				currentPlayer.AttachTurret(t);
			}

			KeypressHandler.Instance().Initialize(lPlayer);
		}
		private void P_TurretShoot(Point origin, Point destination, Account a)
		{
			Bomb b = GameObjectFactory.MakeBomb(origin, destination, PType.PLAYER, a);
			b.DestroyBomb += DestroyGameObject;
			lObject.Add(b);
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
				// TODO uncomment
				//SpawnEnemies();
				for (int i = 0; i < lObject.Count; i++)
					lObject[i].Draw(e.Graphics);
				for (int i = 0; i < lPlayer.Count; i++)
					lPlayer[i].Draw(e.Graphics);
				CollisionDetector();

				frames++;
				KeypressHandler.Instance().MoveCursor();

				// Update after drawing
				Loop();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void CollisionDetector()
		{
			for (int i = 0; i < lObject.Count; i++)
			{
				for (int j = 0; j < lObject.Count; j++)
				{
					if (lObject[i] != lObject[j])
					{
						// if i is enemy and j is not or if i is not enemy and j is
						bool IandJ = lObject[i].GetPlayerType() == PType.ENEMY && lObject[j].GetPlayerType() != PType.ENEMY;
						bool JandI = lObject[i].GetPlayerType() != PType.ENEMY && lObject[j].GetPlayerType() == PType.ENEMY;
						if (IandJ || JandI)
						{
							bool collided = CheckCollision(lObject[i], lObject[j]);
							if (collided)
							{
								lObject[i].Collided();
							}
						}
					}
				}
			}
		}
		private bool CheckCollision(GameObject collider, GameObject collidee)
		{
			// If this was viewed as a square,
			// the way to view it is checks in this order of collider right, left, bottom, top
			if (collider.GetPosition().X + collider.GetDimension().Width < collidee.GetPosition().X)
				return false;
			if (collidee.GetPosition().X + collidee.GetDimension().Width < collider.GetPosition().X)
				return false;
			if (collider.GetPosition().Y + collider.GetDimension().Height < collidee.GetPosition().Y)
				return false;
			if (collidee.GetPosition().Y + collidee.GetDimension().Height < collider.GetPosition().Y)
				return false;

			return true;
		}
		private void SpawnEnemies()
		{
			// TODO complex calculation based on ticks to determine when the next spawn is.
			if (Environment.TickCount >= elapsedTime + 1000)
			{
				Point spawnPoint = new Point(rand.Next(0, Utils.gameBounds.Width), 0);
				// TODO make a list of guaranteed points
				Point destination = new Point(rand.Next(0, Utils.gameBounds.Width), Utils.gameBounds.Height);
				Bomb bmb = GameObjectFactory.MakeBomb(spawnPoint, destination, PType.ENEMY, Account.ENEMY);
				bmb.DestroyBomb += DestroyGameObject;
				lObject.Add(bmb);
			}
		}
		private void SpawnTest()
		{
			Point spawnPoint = new Point(rand.Next(0, Utils.gameBounds.Width), 0);
			Point destination = new Point(Utils.gameBounds.Width / 2, Utils.gameBounds.Height);
			Bomb bmb = GameObjectFactory.MakeBomb(spawnPoint, destination, PType.ENEMY, Account.ENEMY);
			bmb.DestroyBomb += DestroyGameObject;
			lObject.Add(bmb);
		}
		private void MassTest()
		{
			for (int i = 0; i < 100; i++)
			{
				Point spawnPoint = new Point(rand.Next(0, Utils.gameBounds.Width), 0);
				Point destination = new Point(Utils.gameBounds.Width / 2, Utils.gameBounds.Height);
				Bomb bmb = GameObjectFactory.MakeBomb(spawnPoint, destination, PType.ENEMY, Account.ENEMY);
				bmb.DestroyBomb += DestroyGameObject;
				lObject.Add(bmb);
			}
		}
		private void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			KeypressHandler.Instance().KeyDown(e);
			if (e.KeyCode == Keys.H)
				SpawnTest();
			if (e.KeyCode == Keys.J)
				MassTest();
		}
		private void GameForm_KeyUp(object sender, KeyEventArgs e)
		{
			KeypressHandler.Instance().KeyUp(e);
		}
		private void DestroyGameObject(GameObject gameObject)
		{
			lObject.Remove(gameObject);
		}
	}
}
