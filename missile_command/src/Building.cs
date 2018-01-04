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
	class Building : GameObject
	{
		private static List<Image> lSprite = new List<Image>();
		private static int buildingCount;

		static Building()
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
		public Building(Point pos, Size d, PType p, ETag a) : base(pos, d, p, a)
		{

		}
		public override void Collided()
		{

		}
		public override void Draw(Graphics g)
		{
			throw new NotImplementedException();
		}
		public override PType GetPlayerType()
		{
			throw new NotImplementedException();
		}
	}
}
