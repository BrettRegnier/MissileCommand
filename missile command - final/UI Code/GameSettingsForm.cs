using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Missile_Command___Final
{
    public partial class GameSettingsForm : Form
    {
        PlayerSettings PSettings;

        /// <summary>
        /// Sets up the game settings form
        /// </summary>
        public GameSettingsForm(PlayerSettings playerSettings)
        {
            InitializeComponent();
            PSettings = playerSettings;

            chkPlayer1.Checked = PSettings.Player1Controller;
            chkPlayer2.Checked = PSettings.Player2Controller;
            chkPlayer3.Checked = PSettings.Player3Controller;
            chkPlayer4.Checked = PSettings.Player4Controller;

            cmbP1.SelectedIndex = PSettings.P1Index;
            cmbP2.SelectedIndex = PSettings.P2Index;
            cmbP3.SelectedIndex = PSettings.P3Index;
            cmbP4.SelectedIndex = PSettings.P4Index;

            lblP1.ForeColor = Color.FromArgb(PSettings.P1Color);
            lblP2.ForeColor = Color.FromArgb(PSettings.P2Color);
            lblP3.ForeColor = Color.FromArgb(PSettings.P3Color);
            lblP4.ForeColor = Color.FromArgb(PSettings.P4Color);

            chkSound.Checked = PSettings.soundEnabled;
        }

        /// <summary>
        /// Sets up the color from the Setting class below. Sets the index of the combo box to the setting.
        /// Changes the color to the accociated color.
        /// </summary>
        private void ColorSelector(object sender, EventArgs e)
        {
            if (sender == cmbP1)
            {
                PSettings.P1Color = SetColor(cmbP1.SelectedIndex).ToArgb();
                PSettings.P1Index = cmbP1.SelectedIndex;
                lblP1.ForeColor = Color.FromArgb(PSettings.P1Color);
            }
            else if (sender == cmbP2)
            {
                PSettings.P2Color = SetColor(cmbP2.SelectedIndex).ToArgb();
                PSettings.P2Index = cmbP2.SelectedIndex;
                lblP2.ForeColor = Color.FromArgb(PSettings.P2Color);
            }
            else if (sender == cmbP3)
            {
                PSettings.P3Color = SetColor(cmbP3.SelectedIndex).ToArgb();
                PSettings.P3Index = cmbP3.SelectedIndex;
                lblP3.ForeColor = Color.FromArgb(PSettings.P3Color);
            }
            else if (sender == cmbP4)
            {
                PSettings.P4Color = SetColor(cmbP4.SelectedIndex).ToArgb();
                PSettings.P4Index = cmbP4.SelectedIndex;
                lblP4.ForeColor = Color.FromArgb(PSettings.P4Color);
            }
        }

        /// <summary>
        /// Checks the index and sets it to a color I specified
        /// </summary>
        private Color SetColor(int index)
        {
            // 12 choices... 11 indexes
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

        /// <summary>
        /// Saves the settings to the harddrive then closes the form,
        /// </summary>
        private void btnSave_Exit_Click(object sender, EventArgs e)
        {
            PSettings.Player1Controller = chkPlayer1.Checked;
            PSettings.Player2Controller = chkPlayer2.Checked;
            PSettings.Player3Controller = chkPlayer3.Checked;
            PSettings.Player4Controller = chkPlayer4.Checked;

            PSettings.soundEnabled = chkSound.Checked;

            PSettings.SaveSettings();
            this.Close();
        }

        /// <summary>
        /// Just closes the form,
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    [Serializable()]
    public class PlayerSettings
    {
        public bool soundEnabled { get; set; }
        public int P1Color { get; set; }
        public int P2Color { get; set; }
        public int P3Color { get; set; }
        public int P4Color { get; set; }
        public int P1Index { get; set; }
        public int P2Index { get; set; }
        public int P3Index { get; set; }
        public int P4Index { get; set; }
        public bool Player1Controller { get; set; }
        public bool Player2Controller { get; set; }
        public bool Player3Controller { get; set; }
        public bool Player4Controller { get; set; }

        /// <summary>
        /// Loads the settings from the hard drive
        /// </summary>
        public static PlayerSettings LoadSettings()
        {
            PlayerSettings pSettings;
            StreamReader myReader = new StreamReader("PlayerSettings.XML");
            XmlSerializer myXmlSerializer = new XmlSerializer(typeof(PlayerSettings));
            pSettings = (PlayerSettings)myXmlSerializer.Deserialize(myReader);
            myReader.Close();
            return pSettings;
        }
        
        /// <summary>
        /// Saves the settings to the hard drive.
        /// </summary>
        public void SaveSettings()
        {
            XmlSerializer myXmlSerializer = new XmlSerializer(typeof(PlayerSettings));
            StreamWriter myWriter = new StreamWriter("PlayerSettings.XML");
            myXmlSerializer.Serialize(myWriter, this);
            myWriter.Close();
        }
    }
}
