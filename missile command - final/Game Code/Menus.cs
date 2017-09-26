using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Missile_Command___Final
{
    abstract class PlayerMenu
    {
        #region Fields

        public delegate void RecheckPosition();
        public event RecheckPosition checkPosition;

        // Controls
        protected Panel pnlMenu = new Panel();
        protected Button btnFinish = new Button();
        protected Label lblBuildingName = new Label();
        protected Label lblUpgrade1 = new Label();
        protected Label lblUpgrade2 = new Label();
        protected Label lblUpgrade3 = new Label();
        protected Label lblStatus1 = new Label();
        protected Label lblStatus2 = new Label();
        protected Label lblStatus3 = new Label();
        protected Button btnUpgrade1 = new Button();
        protected Button btnUpgrade2 = new Button();
        protected Button btnUpgrade3 = new Button();
        protected Label lblPurchaseStatus = new Label();

        protected Form fp;

        protected LinkerClass Link;

        protected double Upgrade1Cost = 0;
        protected double Upgrade2Cost = 0;
        protected double Upgrade3Cost = 0;

        protected const double COST_MODIFIER = 1.1;

        #endregion

        #region Properties

        public bool Visible { get { return pnlMenu.Visible; } set { pnlMenu.Visible = value; } }
        public int PosX { get { return pnlMenu.Left; } set { pnlMenu.Left = value; } }
        public int PosY { get { return pnlMenu.Top; } set { pnlMenu.Top = value; } }
        public int Width { get { return pnlMenu.Width; } }
        public int Height { get { return pnlMenu.Height; } }

        #endregion

        #region  Constructor

        /// <summary>
        /// Sets up the menus
        /// </summary>
        public PlayerMenu(int formWidth, int formHeight, Form fp, LinkerClass Link)
        {
            this.fp = fp;
            this.Link = Link;

            pnlMenu.Controls.Add(btnFinish);
            pnlMenu.Controls.Add(lblBuildingName);
            pnlMenu.Controls.Add(lblUpgrade1);
            pnlMenu.Controls.Add(lblUpgrade2);
            pnlMenu.Controls.Add(lblUpgrade3);
            pnlMenu.Controls.Add(lblStatus1);
            pnlMenu.Controls.Add(lblStatus2);
            pnlMenu.Controls.Add(lblStatus3);
            pnlMenu.Controls.Add(btnUpgrade1);
            pnlMenu.Controls.Add(btnUpgrade2);
            pnlMenu.Controls.Add(btnUpgrade3);
            pnlMenu.Controls.Add(lblPurchaseStatus);

            pnlMenu.Width = formWidth / 8;
            pnlMenu.Height = 274;
            //pnlMenu.Width = 240;
            //pnlMenu.Height = formWidth / 7;
            pnlMenu.Left = (formWidth / 2) + 120; // Revert this back to the one below.
            //pnlMenu.Left = (formWidth / 2) + pnlMenu.Width / 2;
            pnlMenu.Top = (formHeight / 2) - pnlMenu.Height / 2;
            pnlMenu.BackColor = SystemColors.Control;
            pnlMenu.Visible = false;

            lblBuildingName.Width = pnlMenu.Width;
            lblBuildingName.Height = 40;
            lblBuildingName.Left = 10;
            lblBuildingName.Top = 10;
            lblBuildingName.Font = new Font("Arial", 12, FontStyle.Bold);

            btnFinish.Width = 50;
            btnFinish.Height = 30;
            btnFinish.Left = pnlMenu.Width - (btnFinish.Width + 10);
            btnFinish.Top = pnlMenu.Height - (btnFinish.Height + 10);
            btnFinish.Text = "Finish";
            btnFinish.Click += btnFinish_Click;

            lblPurchaseStatus.Width = pnlMenu.Width - (btnFinish.Width + 10);
            lblPurchaseStatus.Height = 20;
            lblPurchaseStatus.Left = 10;
            lblPurchaseStatus.Top = pnlMenu.Height - (btnFinish.Height + 10);
            lblPurchaseStatus.Text = "";

            int lblUpgCount = 0;
            int lblStatCount = 0;
            int btnCount = 0;

            foreach (Control cntrl in pnlMenu.Controls)
            {
                if (cntrl is Label)
                {
                    if (cntrl != lblBuildingName && cntrl != lblPurchaseStatus)
                    {
                        cntrl.Height = 20;

                        if (cntrl == lblUpgrade1 || cntrl == lblUpgrade2 || cntrl == lblUpgrade3)
                        {
                            cntrl.Width = pnlMenu.Width - cntrl.Left;
                            cntrl.Left = 10;
                            cntrl.Top = 50 + (60 * lblUpgCount++);
                            cntrl.Font = new Font("Arial", 9, FontStyle.Bold);
                        }
                        else
                        {
                            cntrl.Width = pnlMenu.Width;
                            cntrl.Left = 32;
                            cntrl.Top = 73 + (60 * lblStatCount++);
                        }
                    }
                }

                if (cntrl is Button)
                {
                    if (cntrl != btnFinish)
                    {
                        cntrl.Width = 20;
                        cntrl.Height = 20;
                        cntrl.Text = "+";
                        cntrl.Top += 70 + (60 * btnCount++);
                        cntrl.Left = 12;
                    }
                }
            }

            btnUpgrade1.Click += Upgrade1_Click;
            btnUpgrade2.Click += Upgrade2_Click;
            btnUpgrade3.Click += Upgrade3_Click;

            fp.Controls.Add(pnlMenu);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Displays the menu of whatever panel is to be revealed.
        /// </summary>
        public void DisplayMenu()
        {
            pnlMenu.Visible = true;
        }

        /// <summary>
        /// Closes the menu and makes sure it is in the closes and next position of the menus.
        /// </summary>
        public void btnFinish_Click(object sender, EventArgs e)
        {
            lblPurchaseStatus.Text = "";
            pnlMenu.Visible = false;
            checkPosition();
        }

        /// <summary>
        /// Sets the generic upgrade 1 up
        /// </summary>
        protected virtual void Upgrade1_Click(object sender, EventArgs e)
        {
            Link.currentMoney -= Upgrade1Cost;
            Upgrade1Cost *= COST_MODIFIER;
            lblPurchaseStatus.Text = "Upgrade Purchased";
        }

        /// <summary>
        /// Sets the generic upgrade 2 up
        /// </summary>
        protected virtual void Upgrade2_Click(object sender, EventArgs e)
        {
            Link.currentMoney -= Upgrade2Cost;
            Upgrade2Cost *= COST_MODIFIER;
            lblPurchaseStatus.Text = "Upgrade Purchased";
        }

        /// <summary>
        /// Sets the generic upgrade 3 up
        /// </summary>
        protected virtual void Upgrade3_Click(object sender, EventArgs e)
        {
            Link.currentMoney -= Upgrade3Cost;
            Upgrade3Cost *= COST_MODIFIER;
            lblPurchaseStatus.Text = "Upgrade Purchased";
        }

        #endregion
    }

    class IncomeMenu : PlayerMenu
    {
        #region Fields

        private double incomeAmt = 1.50;
        private int incomeInterval = 1000;
        private double bountyReward;
        private Timer incomeTimer = new Timer();

        #endregion

        #region Constructor

        /// <summary>
        /// Sets up the look of the income menu.
        /// </summary>
        public IncomeMenu(int formWidth, int formHeight, Form fp, LinkerClass Link)
            : base(formWidth, formHeight, fp, Link)
        {
            incomeTimer.Interval = incomeInterval;
            incomeTimer.Tick += IncomeTimer_Tick;
            incomeTimer.Enabled = true;

            bountyReward = Link.bountryReward;

            Upgrade1Cost = 10;
            Upgrade2Cost = 100;
            Upgrade3Cost = 50;

            lblBuildingName.Text = "Income";

            lblUpgrade1.Text = "Increase Income Per Tick";
            lblUpgrade2.Text = "Decrease Income Interval";
            lblUpgrade3.Text = "Increase Bomb Bounty";

            lblStatus1.Text = incomeAmt.ToString() + "/tick - " + Upgrade1Cost.ToString("C");
            lblStatus2.Text = incomeInterval.ToString() + "ms - " + Upgrade2Cost.ToString("C");
            lblStatus3.Text = bountyReward.ToString() + "/kill - " + Upgrade3Cost.ToString("C");

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gives money and makes sure it interval is the same as whatever upgrade has be set.
        /// </summary>
        private void IncomeTimer_Tick(object sender, EventArgs e)
        {
            if (!Link.incomeDestroyed)
            {
                if (Link.isPlaying)
                    Link.currentMoney += incomeAmt;
            }
            incomeTimer.Interval = incomeInterval;
        }

        /// <summary>
        /// Increases the cost and income per tick.
        /// </summary>
        protected override void Upgrade1_Click(object sender, EventArgs e)
        {
            if (Link.currentMoney >= Upgrade1Cost)
            {
                base.Upgrade1_Click(sender, e);
                incomeAmt += 1.50;
                lblStatus1.Text = incomeAmt.ToString() + "/tick - " + Upgrade1Cost.ToString("C");

                if (incomeAmt >= 200)
                {
                    btnUpgrade1.Enabled = false;
                    lblStatus1.Text = incomeAmt.ToString() + "/tick - Max Upgrade";
                }
            }
            else
            {
                lblPurchaseStatus.Text = "Insufficient Funds";
            }
        }

        /// <summary>
        /// Decreases he interval to recived money and increases the cost.
        /// </summary>
        protected override void Upgrade2_Click(object sender, EventArgs e)
        {
            if (Link.currentMoney >= Upgrade2Cost)
            {
                base.Upgrade2_Click(sender, e);
                incomeInterval -= 10;
                incomeTimer.Interval = incomeInterval;
                lblStatus2.Text = incomeInterval.ToString() + "ms - " + Upgrade2Cost.ToString("C");

                if (incomeInterval <= 100)
                {
                    btnUpgrade2.Enabled = false;
                    lblStatus2.Text = incomeInterval.ToString() + "ms - Max Upgrade";
                }
            }
            else
            {
                lblPurchaseStatus.Text = "Insufficient Funds";
            }
        }

        /// <summary>
        /// Increases the bountry reward and cost of the upgrade.
        /// </summary>
        protected override void Upgrade3_Click(object sender, EventArgs e)
        {
            if (Link.currentMoney >= Upgrade3Cost)
            {
                base.Upgrade3_Click(sender, e);
                bountyReward += 2;
                Link.bountryReward = bountyReward;
                lblStatus3.Text = bountyReward.ToString() + "/kill - " + Upgrade3Cost.ToString("C");

                if (bountyReward >= 150)
                {
                    btnUpgrade3.Enabled = false;
                    lblStatus3.Text = bountyReward.ToString() + "/kill - Max Upgrade";
                }
            }
            else
            {
                lblPurchaseStatus.Text = "Insufficient Funds";
            }
        }

        #endregion
    }

    class ResearchMenu : PlayerMenu
    {
        #region Fields

        private int ReloadInvterval;
        private int explosionSize;
        private double bombSpeed;

        #endregion

        #region Constructor

        /// <summary>
        /// Sets up the research menu
        /// </summary>
        public ResearchMenu(int formWidth, int formHeight, Form fp, LinkerClass Link)
            : base(formWidth, formHeight, fp, Link)
        {
            ReloadInvterval = Link.reloadAmmoInterval;
            explosionSize = Link.explosionVar;
            bombSpeed = Link.bombSpeedVar;

            Upgrade1Cost = 10;
            Upgrade2Cost = 100;
            Upgrade3Cost = 50;

            lblBuildingName.Text = "Research";

            lblUpgrade1.Text = "Increase Reload Speed";
            lblUpgrade2.Text = "Increase Explosion Size";
            lblUpgrade3.Text = "Increase Bomb Speed";

            lblStatus1.Text = ReloadInvterval.ToString() + "ms/tick - " + Upgrade1Cost.ToString("C");
            lblStatus2.Text = explosionSize.ToString() + " - " + Upgrade2Cost.ToString("C");
            lblStatus3.Text = bombSpeed.ToString() + " - " + Upgrade3Cost.ToString("C");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Descreases the reload interval and increases cost
        /// </summary>
        protected override void Upgrade1_Click(object sender, EventArgs e)
        {
            if (Link.currentMoney >= Upgrade1Cost)
            {
                base.Upgrade1_Click(sender, e);
                
                    ReloadInvterval -= 250;
                    Link.reloadAmmoInterval = ReloadInvterval;
                    lblStatus1.Text = ReloadInvterval.ToString() + "ms/tick - " + Upgrade1Cost.ToString("C");
                
                if (ReloadInvterval <= 500)
                {
                    btnUpgrade1.Enabled = false;
                    lblStatus1.Text = ReloadInvterval.ToString() + "ms/tick - Max Upgrade";
                }
            }
            else
            {
                lblPurchaseStatus.Text = "Insufficient Funds";
            }
        }

        /// <summary>
        /// Increases the explosion size and increases cost.
        /// </summary>
        protected override void Upgrade2_Click(object sender, EventArgs e)
        {
            if (Link.currentMoney >= Upgrade2Cost)
            {
                base.Upgrade2_Click(sender, e);
                explosionSize += 10;
                Link.explosionVar = explosionSize;
                lblStatus2.Text = explosionSize.ToString() + " - " + Upgrade2Cost.ToString("C");

                if (explosionSize == 400)
                {
                    btnUpgrade2.Enabled = false;
                    lblStatus2.Text = explosionSize.ToString() + " - Max Upgrade";
                }
            }
            else
            {
                lblPurchaseStatus.Text = "Insufficient Funds";
            }
        }

        /// <summary>
        /// Increases the bombspeed and increases cost.
        /// </summary>
        protected override void Upgrade3_Click(object sender, EventArgs e)
        {
            if (Link.currentMoney >= Upgrade3Cost)
            {
                base.Upgrade3_Click(sender, e);

                if (bombSpeed < 30)
                {
                    bombSpeed += 1;
                    Link.bombSpeedVar = bombSpeed;
                    lblStatus3.Text = bombSpeed.ToString() + " - " + Upgrade3Cost.ToString("C");
                }

                if (bombSpeed == 30)
                {
                    btnUpgrade3.Enabled = false;
                    lblStatus3.Text = bombSpeed.ToString() + " Max Upgrade";
                }
            }
            else
            {
                lblPurchaseStatus.Text = "Insufficient Funds";
            }
        }

        #endregion

    }

    class EngineeringMenu : PlayerMenu
    {
        #region Fields

        private int maxHealth;
        private int repairAmt;
        private int repairInterval;
        private Timer RepairTimer = new Timer();

        #endregion


        #region Constructor

        /// <summary>
        /// Sets up the engineering menu
        /// </summary>
        public EngineeringMenu(int formWidth, int formHeight, Form fp, LinkerClass Link)
            : base(formWidth, formHeight, fp, Link)
        {
            maxHealth = Link.maxHealth;
            repairAmt = Link.repairAmt;
            repairInterval = Link.repairSpeed;
            //RepairTimer.Tick += RepairTimer_Tick;
            //RepairTimer.Enabled = true;

            Upgrade1Cost = 50;
            Upgrade2Cost = 100;
            Upgrade3Cost = 10;

            lblBuildingName.Text = "Engineering";

            lblUpgrade1.Text = "Increase Building Health";
            lblUpgrade2.Text = "Increase Repair Amount";
            lblUpgrade3.Text = "Decrease Repair Interval";

            lblStatus1.Text = maxHealth.ToString() + " max health - " + Upgrade1Cost.ToString("C");
            lblStatus2.Text = repairAmt.ToString() + " health/tick - " + Upgrade2Cost.ToString("C");
            lblStatus3.Text = repairInterval.ToString() + "ms - " + Upgrade3Cost.ToString("C");
        }

        #endregion

        #region Methods

        //private void RepairTimer_Tick(object sender, EventArgs e)
        //{
        //    if (Link.isPlaying)
        //    Link.currentHealth += repairAmt;
        //}

        /// <summary>
        /// Increases the maxhealth of all buildings and increases the cost.
        /// </summary>
        protected override void Upgrade1_Click(object sender, EventArgs e)
        {
            if (Link.currentMoney >= Upgrade1Cost)
            {
                base.Upgrade1_Click(sender, e);
                maxHealth += 5;
                Link.maxHealth = maxHealth;
                lblStatus1.Text = maxHealth.ToString() + " max health - " + Upgrade1Cost.ToString("C");

                if (maxHealth >= 500)
                {
                    btnUpgrade1.Enabled = false;
                    lblStatus1.Text = maxHealth.ToString() + " max health - Max Upgrade";
                }
            }
            else
            {
                lblPurchaseStatus.Text = "Insufficient Funds";
            }
        }

        /// <summary>
        /// Increases the repairamount pertick and increases cost.
        /// </summary>
        protected override void Upgrade2_Click(object sender, EventArgs e)
        {
            if (Link.currentMoney >= Upgrade2Cost)
            {
                base.Upgrade2_Click(sender, e);

                if (repairAmt < 30)
                {
                    repairAmt += 1;
                    Link.repairAmt = repairAmt;
                    lblStatus2.Text = repairAmt.ToString() + " health/tick - " + Upgrade2Cost.ToString("C");
                }

                if (repairAmt ==30)
                {
                    btnUpgrade2.Enabled = false;
                    lblStatus2.Text = repairAmt.ToString() + " health/tick - Max Upgrade";
                }
            }
            else
            {
                lblPurchaseStatus.Text = "Insufficient Funds";
            }
        }

        /// <summary>
        /// Decreases the repair interval and increases cost.
        /// </summary>
        protected override void Upgrade3_Click(object sender, EventArgs e)
        {
            if (Link.currentMoney >= Upgrade3Cost)
            {
                base.Upgrade3_Click(sender, e);
                repairInterval -= 100;
                RepairTimer.Interval = repairInterval;
                lblStatus3.Text = repairInterval.ToString() + "ms - " + Upgrade3Cost.ToString("C");

                if (repairInterval <= 500)
                {
                    btnUpgrade3.Enabled = false;
                    lblStatus3.Text = repairInterval.ToString() + "ms - Max Upgrade";
                }
            }
            else
            {
                lblPurchaseStatus.Text = "Insufficient Funds";
            }
        }
        #endregion
    }

    class InGameMenu
    {
        #region Fields

        public delegate void ChangeGameState();
        public event ChangeGameState StateHandler;

        public delegate void OpenEngineerMenu(int buildingType);
        public event OpenEngineerMenu DisplayEngineerMenu;

        public delegate void OpenIncomeMenu(int buildingType);
        public event OpenIncomeMenu DisplayIncomeMenu;

        public delegate void OpenResearchMenu(int buildingType);
        public event OpenResearchMenu DisplayResearchMenu;

        private static List<Control> ControlList = new List<Control>();

        private static Panel pnlMenu = new Panel();
        private static Label lblTitle = new Label();
        private static Button btnReturnToGame = new Button();
        private static Button btnReturnToMainMenu = new Button();
        private static Button btnResearch = new Button();
        private static Button btnEngineers = new Button();
        private static Button btnIncome = new Button();
        private static Button btnHowToPlay = new Button();
        private static Label BuildingDeny = new Label();

        private Form fp;
        private int formWidth = 0;
        private int formHeight = 0;

        int btnPress = 0; // Ask steve about this.

        private LinkerClass Link;

        #endregion

        #region Constructor

        static InGameMenu()
        {
            //ControlList.Add(pnlMenu);
            ControlList.Add(btnReturnToGame);
            ControlList.Add(btnReturnToMainMenu);
            ControlList.Add(btnIncome);
            ControlList.Add(btnResearch);
            ControlList.Add(btnEngineers);
            ControlList.Add(btnHowToPlay);
        }

        /// <summary>
        /// sets up the menu.
        /// </summary>
        public InGameMenu(Form fp, int FormWidth, int FormHeight, LinkerClass Link)
        {
            this.Link = Link;
            this.fp = fp;
            this.formWidth = FormWidth;
            this.formHeight = FormHeight;
            SetupMenu();
        }

        #endregion

        #region Properties

        public int FormWidth { set { formWidth = value; } }
        public int FormHeight { set { formHeight = value; } }
        public bool incomeBtnEnable { set { btnIncome.Enabled = value; } }
        public bool researchbtnEnable { set { btnResearch.Enabled = value; } }
        public bool engineerbtnEnable { set { btnEngineers.Enabled = value; } }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets up the menu.
        /// </summary>
        private void SetupMenu()
        {
            pnlMenu.Width = 200;
            pnlMenu.Height = 320;
            pnlMenu.Left = (formWidth / 2) - (pnlMenu.Width / 2);
            pnlMenu.Top = (formHeight / 2) - (pnlMenu.Height / 2);
            pnlMenu.Visible = false;
            pnlMenu.BackColor = Color.White;

            lblTitle.Left = 50;
            lblTitle.Top = 15;
            lblTitle.Width = 150;
            lblTitle.Height = 30;
            lblTitle.Text = "Paused";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);

            pnlMenu.Controls.Add(lblTitle);

            for (int i = 0; i < ControlList.Count; i++)
            {
                ControlList[i].Height = 30;
                ControlList[i].Width = 150;

                ControlList[i].Left = 25;
                ControlList[i].Top = 60 + (40 * i);
                ControlList[i].TabStop = false;

                pnlMenu.Controls.Add(ControlList[i]);
            }

            btnReturnToGame.Click += ReturnToGame;
            btnReturnToGame.Text = "Return to game";

            btnReturnToMainMenu.Click += BackToMainMenu;
            btnReturnToMainMenu.Text = "Main Menu";

            btnEngineers.Click += EngineerMenu;
            btnEngineers.Text = "Engineer Building";

            btnIncome.Click += IncomeMenu;
            btnIncome.Text = "Income Building";

            btnResearch.Click += ResearchMenu;
            btnResearch.Text = "Research Building";

            btnHowToPlay.Click += BtnHowToPlay_Click;
            btnHowToPlay.Text = "How To Play";

            fp.Controls.Add(pnlMenu);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows the show when called and sets the focus to the form when it gets closed as well.
        /// </summary>
        public void ShowMenu()
        {
            btnIncome.Enabled = !Link.incomeDestroyed;
            btnResearch.Enabled = !Link.researchDestroyed;
            btnEngineers.Enabled = !Link.engineerDestroyed;
            pnlMenu.Visible = true;
            fp.Focus();
        }

        /// <summary>
        /// Closes the menu and sets the game back to playing.
        /// </summary>
        public void ReturnToGame(object sender, EventArgs e)
        {
            pnlMenu.Visible = false;
            StateHandler();
        }

        /// <summary>
        /// Sends you back to the main menu by just closes the dialogbox 
        /// </summary>
        public void BackToMainMenu(object sender, EventArgs e)
        {
            // Game doesnt close on first click???? Sends two events??
            if (btnPress++ == 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are sure you want to quit?", "Exit Game", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    pnlMenu.Visible = false;
                    fp.Close();
                }
                else
                {
                    btnPress = 0;
                }
            }
        }

        /// <summary>
        /// Opens the income menu
        /// </summary>
        public void IncomeMenu(object sender, EventArgs e)
        {
            //pnlMenu.Visible = false;
            DisplayIncomeMenu(0);
        }

        /// <summary>
        /// Opens the reseach menu
        /// </summary>
        public void ResearchMenu(object sender, EventArgs e)
        {
            //pnlMenu.Visible = false;
            DisplayResearchMenu(1);
        }

        /// <summary>
        /// Opens the Engineering menu
        /// </summary>
        public void EngineerMenu(object sender, EventArgs e)
        {
            //pnlMenu.Visible = false;
            DisplayEngineerMenu(2);
        }

        /// <summary>
        /// Opens the how to play form.
        /// </summary>
        private void BtnHowToPlay_Click(object sender, EventArgs e)
        {
            HowToPlayForm HTPF = new HowToPlayForm();
            HTPF.ShowDialog();
        }

        #endregion
    }
}
