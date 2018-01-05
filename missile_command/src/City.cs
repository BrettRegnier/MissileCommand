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
	class City : GameObject
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
		public City(PType p, ETag a) : base(p, a)
		{
			sprite = lSprite[(int)SpriteType.ALIVE];
			UpdateDimension(Utils.CITY_SIZE, Utils.CITY_SIZE);
			UpdatePositionX(Utils.CITY_POSITIONS_X[cityCount++]);
			UpdatePositionY(Utils.gameBounds.Height - (Utils.LAND_MASS_HEIGHT + dimension.Height));
			aliveCities++;
			isDestroyed = false;

			Size shieldSize = Dimension();
			shieldSize.Width += Utils.CITY_TRUE_SIZE*2;
			shieldSize.Height += Utils.CITY_TRUE_SIZE*2;
			shield = new Shield(BottomLeft(), TopCenter(), shieldSize, ETag.SYSTEM);
		}
		public override void Collided()
		{
			sprite = lSprite[(int)SpriteType.DEAD];
			isDestroyed = true;
		}
		public override void Draw(Graphics g)
		{
			g.DrawImage(sprite, position);
			//g.DrawRectangle(Pens.Yellow, new Rectangle(position, dimension));
			shield.Draw(g);
		}
		public override int Top()
		{
			if (isDestroyed)
				return base.Top() + Utils.DESTROYED_CITY_SIZE_OFFSET;
			else
				return base.Top();
		}
		public static int NumCitiesAlive()
		{
			return aliveCities;
		}
	}
}
