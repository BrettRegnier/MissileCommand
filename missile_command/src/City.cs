using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
		private static int cityCount = 0;
		private static int aliveCities = 0;

		private Image sprite;
		private bool isDestroyed;
		private Shield shield;

		static City()
		{
			if (lSprite.Count == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					object obj = Properties.Resources.ResourceManager.GetObject("City_" + i);
					Bitmap bm = ((System.Drawing.Bitmap)(obj));
					lSprite.Add(bm);
				}
			}
		}
		public City(ETag t = ETag.SYSTEM) : base (t)
		{
			sprite = lSprite[(int)SpriteType.ALIVE];
			Body.UpdateDimension(Utils.CITY_SIZE, Utils.CITY_SIZE);
			Body.UpdatePositionX(Utils.CITY_POSITIONS_X[cityCount++]);
			Body.UpdatePositionY(Utils.gameBounds.Height - (Utils.LAND_MASS_HEIGHT + Body.Height));
			aliveCities++;
			isDestroyed = false;

			Size shieldSize = Body.Dimension;
			shieldSize.Width += Utils.CITY_TRUE_SIZE*2;
			shieldSize.Height += Utils.CITY_TRUE_SIZE*2;
			shield = new Shield(Body.Left, Body.Bottom, Body.CenterX, Body.Top, shieldSize.Width, shieldSize.Height, Tag);
		}
		protected override void Collided(Body collider)
		{
			if (!shield.Active())
			{
				sprite = lSprite[(int)SpriteType.DEAD];
				isDestroyed = true;
			}
		}
		public override void Draw(Graphics g)
		{
			g.DrawImage(sprite, Body.TopLeft);
			if (!isDestroyed)
			{
				shield.Draw(g);
			}
		}
		public static int NumCitiesAlive()
		{
			return aliveCities;
		}
	}
}
