using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace missile_command
{
	class GameButton : Component
	{
		private const int OUTLINE_OFFSET = 1;

		public event EventHandler Click;

		private bool currentMouse;
		private bool previousMouse;
		private bool isHovering;

		private Point mousePosition;

		private int fontSize;
		private Brush innerColor;
		private StringFormat sf;

		public bool IsEnabled { get; set; }
		public int MovePosition { get; set; }
		public String Text { get; set; }
		public bool IsVisible { get; set; }

		public GameButton(int x, int y, int w, int h) : base(x, y, w, h)
		{
			Init();
		}
		public GameButton(string text, int x, int y, int w, int h) : base(x, y, w, h)
		{
			Init();
			Text = text;
		}
		private void Init()
		{
			IsVisible = true;
			IsEnabled = true;

			sf = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};
			fontSize = 10;
		}
		public override void Draw(Graphics g)
		{
			if (IsVisible)
			{
				innerColor = new SolidBrush(Color.FromArgb(20, 20, 20));
				if (isHovering)
					innerColor = new SolidBrush(Color.FromArgb(40, 40, 40));

				if (currentMouse == true && isHovering)
					fontSize = 12;
				else
					fontSize = 10;
				if (!IsEnabled)
				{
					innerColor = new SolidBrush(Color.FromArgb(20, 20, 20));
					fontSize = 10;
				}

				g.FillRectangle(innerColor, Body.Left, Body.Top, Body.Width, Body.Height);
				g.DrawRectangle(new Pen(Config.Instance.SystemColor), Body.Left, Body.Top, Body.Width, Body.Height);

				if (!string.IsNullOrEmpty(Text))
				{
					g.DrawString(Text, new Font("Times New Roman", fontSize), new SolidBrush(Config.Instance.SystemColor), Body.Center.X, Body.Center.Y, sf);
				}
			}
		}
		public override void Update(long gameTime)
		{
			// true is clicked, false is not clicked
			previousMouse = currentMouse;
			currentMouse = MouseHandler.Instance.MouseState(MOUSE_BUTTONS.VK_LBUTTON);
			mousePosition = System.Windows.Forms.Cursor.Position;

			Rectangle mouseRectangle = new Rectangle(mousePosition.X, mousePosition.Y, 1, 1);

			isHovering = false;
			if (mouseRectangle.IntersectsWith(new Rectangle(Body.TopLeft, Body.Dimension)))
			{
				isHovering = true;
				if (currentMouse == false && previousMouse == true && IsEnabled)
				{
					// Then the mouse is hovering and clicked.
					Click?.Invoke(this, new EventArgs());
				}
			}
		}
		public override void PostUpdate(long gameTime)
		{

		}
	}
}
