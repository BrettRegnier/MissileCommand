using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class Building : GameObject
	{
		public Building(Point pos, PType p, Account a) : base(pos, p, a)
		{

		}
		public override void Collided()
		{
			throw new NotImplementedException();
		}
		public override void Draw(Graphics g)
		{
			throw new NotImplementedException();
		}
		public override Dimension GetDimension()
		{
			throw new NotImplementedException();
		}
		public override PType GetPlayerType()
		{
			throw new NotImplementedException();
		}
		public override Point GetPosition()
		{
			throw new NotImplementedException();
		}
	}
}
