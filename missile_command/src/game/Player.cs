using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	class Player
	{		
		private List<Turret> lTurrets = new List<Turret>();

		private Reticle cursor;
		private PType pType;
		private ETag tag;

		private int fireCount;
		private int coolingDownCount;
		private bool coolingDown;
		private bool noActiveTurrets;

		public ETag GetTag() { return tag; }
		public PType GetPType() { return pType; }

		public Player(PType p, ETag a)
		{
			cursor = new Reticle(Consts.gameBounds.Width / 2, 200, p, a);
			pType = p;
			tag = a;

			fireCount = 0;
			coolingDownCount = 0;
			coolingDown = false;
			noActiveTurrets = true;
		}
		public void AttachTurret(Turret t)
		{
			t.AttachReticleBody(cursor.Body);
			lTurrets.Add(t);
			noActiveTurrets = false;
		}
		public void Draw(Graphics g)
		{
			cursor.Draw(g);
		}
		private void Shoot()
		{
			if (!lTurrets[fireCount].Alive)
			{
				int cycleCount = 0;
				int curIndex = fireCount;
				while (!lTurrets[curIndex].Alive && !noActiveTurrets)
				{
					if (++curIndex >= lTurrets.Count)
						curIndex = 0;

					if (++cycleCount >= lTurrets.Count)
					{
						return;
					}
				}
				fireCount = curIndex;
			}
			if (coolingDown == false)
			{
				// TODO add logic to shoot from a tower based on the position of the cursor, if its closer
				// it should fire first, if ammo is 0 then the next closest should fire.
				
				lTurrets[fireCount++].ShootTurret();
				//lTurrets[1].ShootTurret(cursor.Body.Center);

				// TODO move into turret?
				coolingDown = true;

				if (fireCount >= lTurrets.Count)
					fireCount = 0;

			}
			else
			{
				coolingDownCount++;
			}

			if (coolingDownCount == 10)
			{
				coolingDown = false;
				coolingDownCount = 0;

			}
		}
		public void Update(long gameTime)
		{
			// Determine the keys pressed.
			KPress keysPressed = KeypressHandler.Instance.PlayerKeyState(tag);
			if ((keysPressed & KPress.SHOOT) == KPress.SHOOT)
				Shoot();

			cursor.Update(gameTime);
		}
		public void PostUpdate(long gameTime)
		{
			cursor.PostUpdate(gameTime);
		}
	}
}
