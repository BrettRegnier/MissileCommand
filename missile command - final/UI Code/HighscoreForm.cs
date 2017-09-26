using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Missile_Command___Final
{
    [Serializable()]
    public partial class HighscoreForm: Form
    {
        private double Score = 0;
        private string Player = "";
        private List<string> highscoreList = new List<string>();
        private bool hasScore = false;

        /// <summary>
        /// Overload :D
        /// </summary>
        public HighscoreForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the score bool to true if there is a score.
        /// Sets the score of the form to the score passed in.
        /// </summary>
        public HighscoreForm(long score)
        {
            InitializeComponent();
            Score = score;
            hasScore = true;
        }

        /// <summary>
        /// Loads the scores from the harddrive and displays them in the listbox.
        /// </summary>
        private void HighscoreForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!hasScore)
                {
                    btnSave.Enabled = false;
                    txtName.Enabled = false;
                }

                lblScore.Text = Score.ToString();
                using (Stream myStream = File.Open("Highscore.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    highscoreList = (List<string>)bin.Deserialize(myStream);
                }

                DisplayScores();
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
                Player = txtName.Text + ", " + Score.ToString();
                highscoreList.Add(Player);

                highscoreList.Sort(new HighscoreComparer());

                using (Stream myStream = File.Open("Highscore.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(myStream, highscoreList);
                }

                DisplayScores();

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
        /// Adds the scores to the listbox.
        /// </summary>
        private void DisplayScores()
        {
            lsScore.Items.Clear();
            foreach (string tmp in highscoreList)
            {
                lsScore.Items.Add(tmp);
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

        /// <summary>
        /// Close...
        /// </summary>
        private void btnBackToMainMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
