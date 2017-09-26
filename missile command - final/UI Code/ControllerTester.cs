using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Missile_Command___Final
{
    /// <summary>
    /// Ignore this, it is just a test form.
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateInput();         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateInput();
        }
        //private void button1_Click(object sender, EventArgs e) { GamePad.SetVibration(PlayerIndex.One, 1f, 1f); }
        //private void button2_Click(object sender, EventArgs e) { GamePad.SetVibration(PlayerIndex.One, 0f, 0f); }

        protected void UpdateInput()
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            if (currentState.IsConnected)
            {
                label1.Text = "Connected";
                if (currentState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_A.Checked = true;
                }
                else
                {
                    radioButton_A.Checked = false;
                }

                if (currentState.Buttons.B == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_B.Checked = true;
                }
                else
                {
                    radioButton_B.Checked = false;
                }

                if (currentState.Buttons.X == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_X.Checked = true;
                }
                else
                {
                    radioButton_X.Checked = false;
                }

                if (currentState.Buttons.Y == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_Y.Checked = true;
                }
                else
                {
                    radioButton_Y.Checked = false;
                }

                if (currentState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_Up.Checked = true;
                }
                else
                {
                    radioButton_Up.Checked = false;
                }

                if (currentState.DPad.Down == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_Down.Checked = true;
                }
                else
                {
                    radioButton_Down.Checked = false;
                }

                if (currentState.DPad.Left == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_Left.Checked = true;
                }
                else
                {
                    radioButton_Left.Checked = false;
                }

                if (currentState.DPad.Right == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_Right.Checked = true;
                }
                else
                {
                    radioButton_Right.Checked = false;
                }

                progressBar_RT.Value = Convert.ToInt16(currentState.Triggers.Ri­ght * 100);
                progressBar_LT.Value = Convert.ToInt16(currentState.Triggers.Le­ft * 100);

                if (currentState.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_RB.Checked = true;
                }
                else
                {
                    radioButton_RB.Checked = false;
                }

                if (currentState.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_LB.Checked = true;
                }
                else
                {
                    radioButton_LB.Checked = false;
                }

                //RS_Block.Location = new System.Drawing.Point((Convert.ToInt16(currentState.ThumbStick­s.Right.X * 90) + 90), (Convert.ToInt16(currentState.ThumbStick­s.Right.Y * -90) + 90));
                //LS_Block.Location = new System.Drawing.Point((Convert.ToInt16(currentState.ThumbStick­s.Left.X * 90) + 90), (Convert.ToInt16(currentState.ThumbStick­s.Left.Y * -90) + 90));

                if (currentState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_START.Checked = true;
                }
                else
                {
                    radioButton_START.Checked = false;
                }

                if (currentState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed)
                {
                    radioButton_BACK.Checked = true;
                }
                else
                {
                    radioButton_BACK.Checked = false;
                }
            }
        }
    }
}
