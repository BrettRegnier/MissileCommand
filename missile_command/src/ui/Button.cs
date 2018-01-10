using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace missile_command
{
	class Button : Component
	{
		private const int OUTLINE_OFFSET = 1;

		public event EventHandler Click;

		private bool currentMouse;
		private bool previousMouse;
		private bool isHovering;
		private Point mousePosition;

		private Pen outlineColor;
		private Brush innerColor;
		private Color textColor;
		private Rectangle outerRect;
		private Rectangle innerRect;
		public String Text { get; set; }

		private int fontSize;

		StringFormat sf;

		public Button(int x, int y, int w, int h) : base(x, y, w, h)
		{
			Init();
		}
		public Button(string text, int x, int y, int w, int h) : base(x, y, w, h)
		{
			Init();
			Text = text;
		}
		private void Init()
		{
			outlineColor = Pens.Green;
			textColor = Color.Green;

			Body.MovePositionY(Body.Dimension.Height + OUTLINE_OFFSET);
			Body.MovePositionX(-Body.Dimension.Width / 2);

			innerRect = new Rectangle(Body.TopLeft, Body.Dimension);

			// 2 pixels on all sides
			int nX = Body.Left - OUTLINE_OFFSET;
			int nY = Body.Top - OUTLINE_OFFSET;
			int nW = Body.Width + OUTLINE_OFFSET;
			int nH = Body.Height + OUTLINE_OFFSET;
			outerRect = new Rectangle(nX, nY, nW, nH);

			sf = new StringFormat();
			sf.LineAlignment = StringAlignment.Center;
			sf.Alignment = StringAlignment.Center;
			fontSize = 10;
		}
		public override void Draw(Graphics g)
		{
			Update(); // For now until I update the engine.

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

			g.DrawRectangle(outlineColor, outerRect);
			g.FillRectangle(innerColor,innerRect);

			if (!string.IsNullOrEmpty(Text))
			{
				g.DrawString(Text, new Font("Times New Roman", fontSize), new SolidBrush(textColor), innerRect, sf);
			}

			PostUpdate(); // for now until I update the engine.
		}
		public override void Update()
		{
			// true is clicked, false is not clicked
			previousMouse = currentMouse;
			currentMouse = MouseHandler.Instance().MouseState(MOUSE_BUTTONS.VK_LBUTTON);
			mousePosition = System.Windows.Forms.Cursor.Position;

			Rectangle mouseRectangle = new Rectangle(mousePosition.X, mousePosition.Y, 1, 1);

			isHovering = false;
			if (mouseRectangle.IntersectsWith(innerRect))
			{
				isHovering = true;
				if (currentMouse == false && previousMouse == true)
				{
					// Then the mouse is hovering and clicked.
					Click?.Invoke(this, new EventArgs());
				}
			}
		}
		public override void PostUpdate()
		{

		}
	}
}
