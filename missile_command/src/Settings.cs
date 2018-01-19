using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace missile_command
{
	public partial class Settings : Form
	{
		private PlayerConfiguration pc;

		public Settings()
		{
			InitializeComponent();
			Init();
		}
		private void Init()
		{
			lblAesethetic.Left = this.Width / 2 - lblAesethetic.Width / 2;

			// Fill cursors
			cmbCursor.Left = this.Width / 2 - cmbCursor.Width / 2;
			for (int i = 0; i < 12; i++)
			{
				if (i < 10)
					cmbCursor.Items.Add("Cursor_0" + i.ToString());
				else
					cmbCursor.Items.Add("Cursor_" + i.ToString());
			}
			pbCursor.Left = cmbCursor.Left + cmbCursor.Width + 10;

			cmbColor.Left = cmbCursor.Left;
			cmbColor.Items.Add("01. Red");
			cmbColor.Items.Add("02. Pink");
			cmbColor.Items.Add("03. Orange");
			cmbColor.Items.Add("04. Yellow");
			cmbColor.Items.Add("05. Gold");
			cmbColor.Items.Add("06. Silver");
			cmbColor.Items.Add("07. Light Green");
			cmbColor.Items.Add("08. Dark Green");
			cmbColor.Items.Add("09. Light Blue");
			cmbColor.Items.Add("10. Dark Blue");
			cmbColor.Items.Add("11. Purple");
			cmbColor.Items.Add("12. Violet");
			lblColor.Left = cmbColor.Left + cmbColor.Width + 10;

			lblControls.Left = this.Width / 2 - lblControls.Width / 2;
			chkMouse.Left = this.Width / 2 - chkMouse.Width / 2;

			foreach (string itm in cmbUp.Items)
			{
				cmbDown.Items.Add(itm);
				cmbLeft.Items.Add(itm);
				cmbRight.Items.Add(itm);
				cmbShoot.Items.Add(itm);
			}

			cmbUp.Left = this.Width / 2 - cmbUp.Width / 2;
			lblUp.Left = (cmbUp.Left + cmbUp.Width / 2) - lblUp.Width / 2;

			cmbDown.Left = this.Width / 2 - cmbDown.Width / 2;
			lblDown.Left = (cmbDown.Left + cmbDown.Width / 2) - lblDown.Width / 2;

			cmbLeft.Left = cmbDown.Left - cmbLeft.Width - 5;
			lblLeft.Left = (cmbLeft.Left + cmbLeft.Width / 2) - lblLeft.Width / 2;

			cmbRight.Left = cmbDown.Right + 5;
			lblRight.Left = (cmbRight.Left + cmbRight.Width / 2) - lblRight.Width / 2;

			cmbShoot.Left = this.Width / 2 - cmbShoot.Width / 2;
			lblShoot.Left = (cmbShoot.Left + cmbShoot.Width / 2) - lblShoot.Width / 2;

			btnApply.Left = this.Width / 2 - btnApply.Width;
			btnExit.Left = this.Width / 2;

			cmbPlayer.SelectedIndex = 0;
		}
		private void btnApply_Click(object sender, EventArgs e)
		{
			SaveConfig();
		}
		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void cmbCursor_SelectedIndexChanged(object sender, EventArgs e)
		{
			pbCursor.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(cmbCursor.Text.ToLower());
		}
		private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			Color color = GetColor(cmbColor.SelectedIndex);
			lblColor.BackColor = color;
			lblColor.ForeColor = color;
		}
		private void cmbPlayer_SelectedIndexChanged(object sender, EventArgs e)
		{
			ETag t = ETag.PLAYER1;
			if (cmbPlayer.SelectedIndex == 1)
				t = ETag.PLAYER2;
			else if (cmbPlayer.SelectedIndex == 2)
				t = ETag.PLAYER3;

			LoadConfig(t);
		}
		private Color GetColor(int index)
		{
			Color currColor = Color.Green;
			switch (index)
			{
				case 0: // Red
					currColor = Color.FromArgb(246, 116, 116);
					break;
				case 1: // Pink
					currColor = Color.FromArgb(255, 192, 203);
					break;
				case 2: // Orange
					currColor = Color.FromArgb(255, 163, 67);
					break;
				case 3: // Yellow
					currColor = Color.FromArgb(255, 255, 128);
					break;
				case 4: // Gold
					currColor = Color.FromArgb(197, 179, 88);
					break;
				case 5: // Silver
					currColor = Color.FromArgb(191, 193, 194);
					break;
				case 6: // Light Green
					currColor = Color.FromArgb(128, 255, 128);
					break;
				case 7: // Dark Green
					currColor = Color.FromArgb(3, 192, 60);
					break;
				case 8: // Light Blue
					currColor = Color.FromArgb(30, 144, 255);
					break;
				case 9: // Dark Blue
					currColor = Color.FromArgb(0, 51, 153);
					break;
				case 10: // Purple
					currColor = Color.FromArgb(102, 0, 255);
					break;
				case 11: // Violet
					currColor = Color.FromArgb(49, 0, 98);
					break;
			}
			return currColor;
		}
		private void LoadConfig(ETag t)
		{
			pc = Config.Instance.LoadPlayer(t);

			chkMouse.Checked = pc.MouseEnabled;

			foreach (string itm in cmbCursor.Items)
				if (itm.ToLower() == pc.PCursor)
					cmbCursor.SelectedItem = itm;

			for (int i = 0; i < 12; i++)
			{
				if (GetColor(i).ToArgb().ToString() == pc.PColor.ToArgb().ToString())
				{
					cmbColor.SelectedIndex = i;
					break;
				}
			}

			foreach (string itm in cmbUp.Items)
				if (itm == pc.PKeys[KPress.UP].ToString())
					cmbUp.SelectedItem = itm;
			foreach (string itm in cmbDown.Items)
				if (itm == pc.PKeys[KPress.DOWN].ToString())
					cmbDown.SelectedItem = itm;
			foreach (string itm in cmbLeft.Items)
				if (itm == pc.PKeys[KPress.LEFT].ToString())
					cmbLeft.SelectedItem = itm;
			foreach (string itm in cmbRight.Items)
				if (itm == pc.PKeys[KPress.RIGHT].ToString())
					cmbRight.SelectedItem = itm;
			foreach (string itm in cmbShoot.Items)
				if (itm == pc.PKeys[KPress.SHOOT].ToString())
					cmbShoot.SelectedItem = itm;
		}
		private void SaveConfig()
		{
			pc.MouseEnabled = chkMouse.Enabled;
			pc.PColor = GetColor(cmbColor.SelectedIndex);
			pc.PCursor = cmbCursor.Text.ToLower();

			Enum.TryParse(cmbUp.Text, out Keys up);
			Enum.TryParse(cmbDown.Text, out Keys down);
			Enum.TryParse(cmbLeft.Text, out Keys left);
			Enum.TryParse(cmbRight.Text, out Keys right);
			Enum.TryParse(cmbShoot.Text, out Keys shoot);

			pc.PKeys[KPress.UP] = up;
			pc.PKeys[KPress.DOWN] = down;
			pc.PKeys[KPress.LEFT] = left;
			pc.PKeys[KPress.RIGHT] = right;
			pc.PKeys[KPress.SHOOT] = shoot;

			pc.Save();
		}
	}
}
