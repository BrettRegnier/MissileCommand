﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace missile_command
{
	[Serializable()]
	public partial class HighscoresForm : Form
	{
		private double score = 0;
		private string name = "";
		private List<string> highscoreList = new List<string>();
		private bool hasScore = false;

		/// <summary>
		/// Overload :D
		/// </summary>
		public HighscoresForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Sets the score bool to true if there is a score.
		/// Sets the score of the form to the score passed in.
		/// </summary>
		public HighscoresForm(long score)
		{
			InitializeComponent();
			this.score = score;
			hasScore = true;
		}

		/// <summary>
		/// Loads the scores from the harddrive and displays them in the listbox.
		/// </summary>
		private void HighscoresForm_Load(object sender, EventArgs e)
		{
			try
			{
				if (!hasScore)
				{
					btnSave.Enabled = false;
					txtName.Enabled = false;
				}

				lblScore.Text = "Score: " + score.ToString();
				lblScore.ForeColor = Config.Instance.SystemColor;
				using (Stream myStream = File.Open("Highscore.bin", FileMode.Open))
				{
					BinaryFormatter bin = new BinaryFormatter();
					highscoreList = (List<string>)bin.Deserialize(myStream);
				}

				this.Invalidate();
			}
			catch (IOException ex)
			{
				using (Stream myStream = File.Open("Highscore.bin", FileMode.Create))
				{
					BinaryFormatter bin = new BinaryFormatter();
					bin.Serialize(myStream, highscoreList);
				}
				lblStatus.Text = "Error while loading making a new file";
			}
		}

		/// <summary>
		/// Saves the new score to the list and to the harddrive.
		/// </summary>
		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtName.Text.Length != 3)
				{
					throw new Exception("You much enter a 3 letter name");
				}
				name = txtName.Text + ", " + score.ToString();
				highscoreList.Add(name);

				highscoreList.Sort(new HighscoreComparer());

				using (Stream myStream = File.Open("Highscore.bin", FileMode.Create))
				{
					BinaryFormatter bin = new BinaryFormatter();
					bin.Serialize(myStream, highscoreList);
				}

				this.Invalidate();

				btnSave.Enabled = false;
				txtName.Enabled = false;
			}
			catch (IOException ex)
			{
				lblStatus.Text = "Error while writing " + ex.Message;
			}
			catch (Exception ex)
			{
				lblStatus.Text = ex.Message;
			}
		}
		/// <summary>
		/// Compares the scores against each other to sort the list from hightest to lowest.
		/// </summary>
		public class HighscoreComparer : IComparer<string>
		{
			public int Compare(string x, string y)
			{
				long scoreX;
				long scoreY;

				scoreX = Convert.ToInt64(x.Substring(5, x.Length - 5));
				scoreY = Convert.ToInt64(y.Substring(5, y.Length - 5));

				if (scoreX > scoreY)
					return -1;
				else if (scoreY > scoreX)
					return 1;
				else
					return 0;
			}
		}
		private void btnReturn_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void HighscoresForm_Paint(object sender, PaintEventArgs e)
		{
			int startx = txtName.Left + 200;
			int starty = lblScore.Top;
			int seperation = 10;
			for (int i = 0; i < highscoreList.Count; i++)
			{
				e.Graphics.DrawString(highscoreList[i], new Font("Times New Roman", 12), new SolidBrush(Config.Instance.SystemColor), startx, starty + seperation * i);
			}
		}
	}
}