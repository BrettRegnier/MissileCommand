using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Missile_Command___Final
{
    public partial class GameForm
    {
        private IncomeMenu incomeMenu;
        private EngineeringMenu engineerMenu;
        private ResearchMenu researchMenu;

        /// <summary>
        /// Makes the darn menus.
        /// </summary>
        private void MakeMenus()
        {
            myStartMenu = new InGameMenu(this, this.ClientSize.Width, this.ClientSize.Height, Link);
            myStartMenu.StateHandler += ChangeGameState;
            myStartMenu.DisplayIncomeMenu += MyStartMenu_DisplayIncomeMenu;
            myStartMenu.DisplayResearchMenu += MyStartMenu_DisplayResearchMenu;
            myStartMenu.DisplayEngineerMenu += MyStartMenu_DisplayEngineerMenu;

            incomeMenu = new IncomeMenu(this.ClientSize.Width, this.ClientSize.Height, this, Link);
            incomeMenu.checkPosition += CheckPosition;
            engineerMenu = new EngineeringMenu(this.ClientSize.Width, this.ClientSize.Height, this, Link);
            engineerMenu.checkPosition += CheckPosition;
            researchMenu = new ResearchMenu(this.ClientSize.Width, this.ClientSize.Height, this, Link);
            researchMenu.checkPosition += CheckPosition;
        }

        /// <summary>
        /// Displays the income menu ajd pushes all of the other menus when it is displayed to make sure it is always
        /// side by side with the main menu form.
        /// </summary>
        private void MyStartMenu_DisplayIncomeMenu(int buildingType)
        {
            if (!BuildingList[buildingType].Destroyed)
            {
                incomeMenu.DisplayMenu();
                //CheckPosition();

                if (researchMenu.Visible && engineerMenu.Visible)
                {
                    researchMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
                    engineerMenu.PosX = researchMenu.PosX + (researchMenu.Width + 10);
                }
                else if (researchMenu.Visible)
                {
                    researchMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
                }
                else if (engineerMenu.Visible)
                {
                    engineerMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
                }
            }
        }

        /// <summary>
        /// Displays the research menu and makes sure it is between the income menu and engineering menu, unless nothing is there then
        /// it will be side by side with the gamemenu.
        /// </summary>
        private void MyStartMenu_DisplayResearchMenu(int buildingType)
        {
            if (!BuildingList[buildingType].Destroyed)
            {
                researchMenu.DisplayMenu();
                //CheckPosition();

                if (incomeMenu.Visible)
                {
                    researchMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
                }
                else
                {
                    researchMenu.PosX = incomeMenu.PosX;
                }

                if (engineerMenu.Visible)
                {
                    engineerMenu.PosX = researchMenu.PosX + (researchMenu.Width + 10);
                }
            }
        }

        /// <summary>
        /// Displays the engineering upgrade window and moves it to the furthest of the other menus, unless nothing is there then
        /// it will be side by side with the gamemenu.
        /// </summary>
        private void MyStartMenu_DisplayEngineerMenu(int buildingType)
        {
            if (!BuildingList[buildingType].Destroyed)
            {
                engineerMenu.DisplayMenu();
                //CheckPosition();

                if (researchMenu.Visible)
                {
                    engineerMenu.PosX = researchMenu.PosX + (researchMenu.Width + 10);
                }
                else if (incomeMenu.Visible)
                {
                    engineerMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
                }
                else
                {
                    engineerMenu.PosX = incomeMenu.PosX;
                }
            }
        }

        /// <summary>
        /// Check the position of the menus. Moved the code into seperate methods to split up the amount of code it has to go through.
        /// </summary>
        private void CheckPosition()
        {
            if (!incomeMenu.Visible && !researchMenu.Visible)
            {
                engineerMenu.PosX = incomeMenu.PosX;
            }
            else if (!incomeMenu.Visible)
            {
                researchMenu.PosX = incomeMenu.PosX;
                engineerMenu.PosX = researchMenu.PosX + (researchMenu.Width + 10);
            }
            else if (!researchMenu.Visible)
            {
                engineerMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
            }

            #region Old Code
            // Check for when the income menu is opened
            //if (researchMenu.Visible && engineerMenu.Visible)
            //{
            //    researchMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
            //    engineerMenu.PosX = researchMenu.PosX + (researchMenu.Width + 10);
            //}
            //else if (researchMenu.Visible)
            //{
            //    researchMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
            //}
            //else if (engineerMenu.Visible)
            //{
            //    engineerMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
            //}

            // Check for when the research menu is opened
            //if (incomeMenu.Visible)
            //{
            //    researchMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
            //}
            //else
            //{
            //    researchMenu.PosX = incomeMenu.PosX;
            //}

            //if (engineerMenu.Visible)
            //{
            //    engineerMenu.PosX = researchMenu.PosX + (researchMenu.Width + 10);
            //}


            // Check for when the engineer menu is opened
            //if (researchMenu.Visible)
            //{
            //    engineerMenu.PosX = researchMenu.PosX + (researchMenu.Width + 10);
            //}
            //else if (incomeMenu.Visible)
            //{
            //    engineerMenu.PosX = incomeMenu.PosX + (incomeMenu.Width + 10);
            //}
            //else
            //{
            //    engineerMenu.PosX = incomeMenu.PosX;
            //}
            #endregion
        }
    }
}
