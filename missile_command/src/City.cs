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
		private static List<Image> lSprite = new List<Image>();
		private static int cityCount = 0;

		private Image sprite;

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
			sprite = lSprite[0];
			UpdateDimension(Utils.CITY_SIZE, Utils.CITY_SIZE);
			UpdatePositionX(Utils.CITY_POSITIONS_X[cityCount++]);
			UpdatePositionY(Utils.gameBounds.Height - (Utils.LAND_MASS_HEIGHT + dimension.Height));
		}
		public override void Collided()
		{

		}
		public override void Draw(Graphics g)
		{
			g.DrawImage(sprite, position);
		}
		public override PType GetPlayerType()
		{
			throw new NotImplementedException();
		}
	}
}
