using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class City : Entity
	{
		private enum SpriteType
		{
			ALIVE = 0,
			DEAD
		};

		private static List<Image> lSprite = new List<Image>();
		private static int aliveCities = 0;

		private Image sprite;
		private Shield shield;

		private Collider prevCollider;
		private Collider cHolder;

		static City()
		{
			if (lSprite.Count == 0)
				for (int i = 0; i < 2; i++)
					lSprite.Add((Bitmap)Properties.Resources.ResourceManager.GetObject("City_" + i));
		}
		public City(int x, int y, int w, int h, ETag t = ETag.SYSTEM) : base(x, y, w, h, t)
		{
			sprite = lSprite[(int)SpriteType.ALIVE];
			aliveCities++;

			Size shieldSize = Body.Dimension;
			shieldSize.Width += Consts.CITY_TRUE_SIZE + 6;
			shieldSize.Height += Consts.CITY_TRUE_SIZE * 2;

			// I need to pass in the city true size /2, since that would be the TRUE center (windows drawing issues)
			shield = new Shield(Body.Center.X, Body.Bottom, Body.Center.X, Body.Top, shieldSize.Width, shieldSize.Height, Tag);

			cHolder = Collider;
			Collider = shield.Collider;
		}
		protected override void Collided(Collider collider)
		{
			if (shield.PreviousCollider != collider && collider != prevCollider)
			{
				if (!shield.Alive)
				{
					sprite = lSprite[(int)SpriteType.DEAD];
					Alive = false;
					prevCollider = collider;
				}
			}
		}
		public override void Draw(Graphics g)
		{
			g.DrawImage(sprite, Body.TopLeft);
			if (Alive)
			{
				shield.Draw(g);
			}
		}
		public static int NumCitiesAlive()
		{
			return aliveCities;
		}
		private void ShieldLowered()
		{
			Collider = cHolder;
		}
		private void ShieldReplished()
		{
			Collider = shield.Collider;
		}
		public override void Update(long gameTime)
		{
			shield.Update(gameTime);
			if (shield.Alive)
				Collider = shield.Collider;
			else
				Collider = cHolder;

		}
		public override void PostUpdate(long gameTime)
		{
			shield.PostUpdate(gameTime);
		}
	}
}
