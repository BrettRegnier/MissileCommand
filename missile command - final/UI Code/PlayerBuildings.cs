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
        /// <summary>
        /// Adds the player objects to the form based on the size of the form and how many players are in the game.
        /// </summary>
        public void AddPlayerObjects()
        {
            int startVal = 0;
            int clientSplit = this.ClientSize.Width / 2;

            if (numPlayers == 2 || numPlayers == 4)
            {
                startVal = this.ClientSize.Width / 8;
                clientSplit = this.ClientSize.Width / 4;
            }
            if (numPlayers == 3)
            {
                startVal = this.ClientSize.Width / 6;
                clientSplit = this.ClientSize.Width / 3;
            }

            // the weird values determine where the defense buildings will be placed.
            for (int i = 0; i < numPlayers; i++)
            {
                switch (i)
                {
                    case (int)Player.P1:
                        playerVar = 1;
                        break;

                    case (int)Player.P2:
                        playerVar = 2;
                        break;

                    case (int)Player.P3:
                        playerVar = 0;
                        break;

                    case (int)Player.P4:
                        playerVar = 3;
                        break;
                }

                Cursor Cursor = new Cursor(startVal + (clientSplit * playerVar), this.ClientSize.Height, i, PSettings);
                Cursor.FormWidth = this.ClientSize.Width;
                Cursor.FormHeight = this.ClientSize.Height;
                CursorList.Add(Cursor);

                DefenseTower Tower = new DefenseTower(new Point(startVal + (clientSplit * playerVar), this.ClientSize.Height), i, Link, this.ClientSize.Width, this.ClientSize.Height, PSettings);
                Tower.RebuiltHandler += BuildingRebuilt;
                TowerList.Add(Tower);
                //Point tmp = Tower.GunPosition;
            }



            //BuildingList.Add(new IncomeBuilding(new Point(this.ClientSize.Width / 8, this.ClientSize.Height)));
            //BuildingList.Add(new ResearchBuilding(new Point(this.ClientSize.Width / 3, this.ClientSize.Height)));
            //BuildingList.Add(new EngineerBuilding(new Point((clientSplit * playerVar), this.ClientSize.Height)));
            //BuildingList.Add(new GenericBuilding(new Point((clientSplit * playerVar) * 6, this.ClientSize.Height)));
        }

        /// <summary>
        /// Seperates the buildings to fit all of the buildings.. might try to find a way around this..
        /// </summary>
        private void AddBuildings()
        {
            // Make a formula to divide this evenly without this nasty nasty nasty nasty code.

            int startVal = this.ClientSize.Width / 39;
            int clientSplit = this.ClientSize.Width / 11;

            switch (numPlayers)
            {
                case 2:
                    startVal = -this.ClientSize.Width / 39;
                    clientSplit = this.ClientSize.Width / 12;
                    break;
                case 3:
                    startVal = -this.ClientSize.Width / 12;
                    clientSplit = this.ClientSize.Width / 8;
                    break;
                case 4:
                    startVal = -this.ClientSize.Width / 11;
                    clientSplit = this.ClientSize.Width / 9;
                    break;
            }

            IncomeBuilding income = new IncomeBuilding(new Point(startVal + clientSplit, this.ClientSize.Height), Link, this.ClientSize.Width, this.ClientSize.Height);
            income.RebuiltHandler += BuildingRebuilt;
            BuildingList.Add(income);

            ResearchBuilding research = new ResearchBuilding(new Point(startVal + clientSplit * 3, this.ClientSize.Height), Link, this.ClientSize.Width, this.ClientSize.Height);
            research.RebuiltHandler += BuildingRebuilt;
            BuildingList.Add(research);

            switch (numPlayers)
            {
                case 1:
                    startVal = TowerList[0].PosX + 50;
                    break;
                case 2:
                    startVal = TowerList[1].PosX + 50;
                    break;
                case 3:
                    startVal = TowerList[0].PosX + 50;
                    break;
                case 4:
                    startVal = TowerList[1].PosX - 50;
                    break;
            }

            EngineerBuilding engineer = new EngineerBuilding(new Point(startVal + clientSplit, this.ClientSize.Height), Link, this.ClientSize.Width, this.ClientSize.Height);
            engineer.RebuiltHandler += BuildingRebuilt;
            BuildingList.Add(engineer);

            GenericBuilding generic = new GenericBuilding(new Point(startVal + (clientSplit * 3), this.ClientSize.Height), Link, this.ClientSize.Width, this.ClientSize.Height);
            generic.RebuiltHandler += BuildingRebuilt;
            BuildingList.Add(generic);


            #region Old Code
            //int PosX = this.ClientSize.Width / 13;
            //int PosX2 = 0;
            //if (numPlayers == 2)
            //{
            //    //PosX = TowerList[0].GunPosition.X + ((TowerList[1].GunPosition.X - TowerList[0].GunPosition.X) / 2) - 50;
            //    //PosX = this.ClientSize.Width / 20;
            //    int PosX2 = TowerList[1].GunPosition.X + 50;
            //}

            //IncomeBuilding IncomeBuild = new IncomeBuilding(new Point(PosX, this.ClientSize.Height));
            //BuildingList.Add(IncomeBuild);

            //ResearchBuilding ResearchBuild = new ResearchBuilding(new Point(PosX * 4, this.ClientSize.Height));
            //BuildingList.Add(ResearchBuild);

            //EngineerBuilding EngineerBuild = new EngineerBuilding(new Point(PosX * 8, this.ClientSize.Height));
            //BuildingList.Add(EngineerBuild);

            //BuildingList.Add(new GenericBuilding(new Point(PosX * 11, this.ClientSize.Height)));
            #endregion
        }

        /// <summary>
        /// When a building is rebuilt it will reduct how many are declared destroyed so you dont get game overed.
        /// </summary>
        public void BuildingRebuilt()
        {
            destroyedCount--;
        }
    }
}
