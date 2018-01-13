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
		private Pen outlineColor;
		private Color textColor;
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
			outlineColor = Pens.Green;
			textColor = Color.Green;
			IsVisible = true;
			IsEnabled = true;

			Body.AdjustY(Body.Dimension.Height + OUTLINE_OFFSET);
			Body.AdjustX(-Body.Dimension.Width / 2);

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
				{
					textColor = Color.LimeGreen;
					fontSize = 12;
				}
				else
				{
					textColor = Color.Green;
					fontSize = 10;
				}
				if (!IsEnabled)
				{
					innerColor = new SolidBrush(Color.FromArgb(20, 20, 20));
					textColor = Color.Green;
					fontSize = 10;
				}

				int nX = Body.Left - OUTLINE_OFFSET;
				int nY = Body.Top - OUTLINE_OFFSET;
				int nW = Body.Width + OUTLINE_OFFSET;
				int nH = Body.Height + OUTLINE_OFFSET;
				g.DrawRectangle(outlineColor, nX, nY, nW, nH);
				g.FillRectangle(innerColor, Body.Left, Body.Top, Body.Width, Body.Height);

				if (!string.IsNullOrEmpty(Text))
				{
					g.DrawString(Text, new Font("Times New Roman", fontSize), new SolidBrush(textColor), Body.Center.X, Body.Center.Y, sf);
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
