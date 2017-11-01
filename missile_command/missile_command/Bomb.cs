using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	public class Bomb
	{
		private Player player;
		Point origin;
		Point destination;
		Color color;

		public Bomb(Player p, Point o, Point d)
		{
			player = p;
			origin = o;
			destination = d;

			// Set color
			switch (player)
			{
				case (Player.enemy):
					color = Color.White;
					break;
				case (Player.player1):
					color = Color.Green;
					break;
				case (Player.player2)
					c
			}


		}

		public void draw(Graphics g)
		{

		}
		public void move()
		{

		}
		public void collide()
		{

		}
		public void calcVelocity()
		{

		}
		public void explosionCalc()
		{

		}
	}
}
