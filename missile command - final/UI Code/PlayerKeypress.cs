using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Missile_Command___Final
{
    public partial class GameForm
    {
        // Maybe make into array? using the Player enum to find the position.
        private bool isKeyDownP1 = false;
        private bool isKeyDownP2 = false;
        private bool isKeyDownP3 = false;
        private bool isKeyDownP4 = false;

        enum KPress
        {
            none = 0,
            up = 1,
            right = 2,
            down = 4,
            left = 8,
            shoot = 16
            //showMenu = 32
        };

        enum Player
        {
            P1 = 0,
            P2,
            P3,
            P4
        }

        KPress Player1 = KPress.none;
        KPress Player2 = KPress.none;
        KPress Player3 = KPress.none;
        KPress Player4 = KPress.none;

        /// <summary>
        /// moving of all the players and allowing them to press 3 System.Windows.Forms.Keys at the same time. 
        /// Also allows the use of controller.
        /// For every player that is passed in it will allow for checking the keypresses and then will move the connected cursor with the presses same for shooting
        /// If player 1 is using a controller then player 3 gets their keybindings.
        /// Same fpr player 2 and 4
        /// </summary>
        private void MoveCursors()
        {
            if (!gameOver)
            {
                if (numPlayers > 0)
                {
                    if (PSettings.Player1Controller)
                    {
                        GamePadState Player1Controller = GamePad.GetState(PlayerIndex.One);
                        if (Player1Controller.IsConnected)
                        {
                            if ((Convert.ToInt16(Player1Controller.ThumbStick­s.Left.X)) == 1)
                            {
                                CursorList[(int)Player.P1].move((int)KPress.right);
                                TowerList[(int)Player.P1].TurretCalc(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                            }

                            if ((Convert.ToInt16(Player1Controller.ThumbStick­s.Left.X)) == -1)
                            {
                                CursorList[(int)Player.P1].move((int)KPress.left);
                                TowerList[(int)Player.P1].TurretCalc(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                            }

                            if ((Convert.ToInt16(Player1Controller.ThumbStick­s.Left.Y)) == 1)
                            {
                                CursorList[(int)Player.P1].move((int)KPress.up);
                                TowerList[(int)Player.P1].TurretCalc(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                            }

                            if ((Convert.ToInt16(Player1Controller.ThumbStick­s.Left.Y)) == -1)
                            {
                                CursorList[(int)Player.P1].move((int)KPress.down);
                                TowerList[(int)Player.P1].TurretCalc(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                            }

                            if (Player1Controller.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Released)
                            {
                                isKeyDownP1 = false;
                            }

                            if (Player1Controller.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                            {
                                if (isPlaying)
                                {
                                    ChangeGameState();
                                    myStartMenu.ShowMenu();
                                }
                            }

                            if ((Player1Controller.Buttons.A == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed) && !isKeyDownP1 && !TowerList[(int)Player.P1].Destroyed && TowerList[(int)Player.P1].CurrentAmmo > 0)
                            {
                                TowerList[(int)Player.P1].Shoot();

                                isKeyDownP1 = true;
                                System.Drawing.Point originPoint = TowerList[(int)Player.P1].GunPosition;
                                System.Drawing.Point destinationPoint = new System.Drawing.Point(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                                PlayerBomb db = new PlayerBomb(originPoint, destinationPoint, this, (int)Player.P1, Link, PSettings);
                                db.DestroyBomb += RemoveBomb;
                                BombList.Add(db);
                            }
                        }
                        else
                        {
                            PSettings.Player1Controller = false;
                            if (isPlaying)
                                ChangeGameState();

                            MessageBox.Show("No controller found for Player 1, switching to keyboard");

                            if (!isPlaying)
                                ChangeGameState();
                        }
                    }
                    else
                    {
                        // If player one chooses keyboard.
                        if ((Player1 & KPress.left) == KPress.left)
                        {
                            CursorList[(int)Player.P1].move((int)KPress.left);
                            TowerList[(int)Player.P1].TurretCalc(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                        }

                        if ((Player1 & KPress.right) == KPress.right)
                        {
                            CursorList[(int)Player.P1].move((int)KPress.right);
                            TowerList[(int)Player.P1].TurretCalc(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                        }

                        if ((Player1 & KPress.down) == KPress.down)
                        {
                            CursorList[(int)Player.P1].move((int)KPress.down);
                            TowerList[(int)Player.P1].TurretCalc(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                        }

                        if ((Player1 & KPress.up) == KPress.up)
                        {
                            CursorList[(int)Player.P1].move((int)KPress.up);
                            TowerList[(int)Player.P1].TurretCalc(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                        }

                        if ((Player1 & KPress.shoot) == KPress.shoot && !isKeyDownP1 && !TowerList[(int)Player.P1].Destroyed && TowerList[(int)Player.P1].CurrentAmmo > 0)
                        {
                            TowerList[(int)Player.P1].Shoot();

                            isKeyDownP1 = true;
                            System.Drawing.Point originPoint = TowerList[(int)Player.P1].GunPosition;
                            System.Drawing.Point destinationPoint = new System.Drawing.Point(CursorList[(int)Player.P1].PosX, CursorList[(int)Player.P1].PosY);
                            PlayerBomb db = new PlayerBomb(originPoint, destinationPoint, this, (int)Player.P1, Link, PSettings);
                            db.DestroyBomb += RemoveBomb;
                            BombList.Add(db);

                            // This is used for testing.
                            //originPoint = new System.Drawing.Point(TowerList[0].PosX, 0);
                            //destinationPoint = new System.Drawing.Point(TowerList[0].PosX, this.ClientSize.Height);
                            //EnemyBomb EBB = new EnemyBomb(originPoint, destinationPoint, this, speed);
                            //EBB.DestroyBomb += RemoveBomb;
                            //BombList.Add(EBB);

                            //originPoint = new System.Drawing.Point(BuildingList[0].PosX, 0);
                            //destinationPoint = new System.Drawing.Point(BuildingList[0].PosX, this.ClientSize.Height);
                            //EnemyBomb EBB2 = new EnemyBomb(originPoint, destinationPoint, this, speed);
                            //EBB2.DestroyBomb += RemoveBomb;
                            //BombList.Add(EBB2);

                            //originPoint = new System.Drawing.Point(BuildingList[1].PosX, 0);
                            //destinationPoint = new System.Drawing.Point(BuildingList[1].PosX, this.ClientSize.Height);
                            //EnemyBomb EBB3 = new EnemyBomb(originPoint, destinationPoint, this, speed);
                            //EBB3.DestroyBomb += RemoveBomb;
                            //BombList.Add(EBB3);

                            //originPoint = new System.Drawing.Point(BuildingList[2].PosX, 0);
                            //destinationPoint = new System.Drawing.Point(BuildingList[2].PosX, this.ClientSize.Height);
                            //EnemyBomb EBB4 = new EnemyBomb(originPoint, destinationPoint, this, speed);
                            //EBB4.DestroyBomb += RemoveBomb;
                            //BombList.Add(EBB4);

                            //originPoint = new System.Drawing.Point(BuildingList[3].PosX, 0);
                            //destinationPoint = new System.Drawing.Point(BuildingList[3].PosX, this.ClientSize.Height);
                            //EnemyBomb EBB5 = new EnemyBomb(originPoint, destinationPoint, this, speed);
                            //EBB5.DestroyBomb += RemoveBomb;
                            //BombList.Add(EBB5);
                        }
                    }
                }

                if (numPlayers > 1)
                {
                    if (PSettings.Player2Controller)
                    {
                        GamePadState Player2Controller;
                        if (PSettings.Player1Controller)
                            Player2Controller = GamePad.GetState(PlayerIndex.Two);
                        else
                            Player2Controller = GamePad.GetState(PlayerIndex.One);

                        if (Player2Controller.IsConnected)
                        {
                            if ((Convert.ToInt16(Player2Controller.ThumbStick­s.Left.X)) == 1)
                            {
                                CursorList[(int)Player.P2].move((int)KPress.right);
                                TowerList[(int)Player.P2].TurretCalc(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                            }

                            if ((Convert.ToInt16(Player2Controller.ThumbStick­s.Left.X)) == -1)
                            {
                                CursorList[(int)Player.P2].move((int)KPress.left);
                                TowerList[(int)Player.P2].TurretCalc(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                            }

                            if ((Convert.ToInt16(Player2Controller.ThumbStick­s.Left.Y)) == 1)
                            {
                                CursorList[(int)Player.P2].move((int)KPress.up);
                                TowerList[(int)Player.P2].TurretCalc(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                            }

                            if ((Convert.ToInt16(Player2Controller.ThumbStick­s.Left.Y)) == -1)
                            {
                                CursorList[(int)Player.P2].move((int)KPress.down);
                                TowerList[(int)Player.P2].TurretCalc(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                            }

                            if (Player2Controller.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Released)
                            {
                                isKeyDownP2 = false;
                            }

                            if (Player2Controller.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                            {
                                if (isPlaying)
                                {
                                    ChangeGameState();
                                    myStartMenu.ShowMenu();
                                }
                            }

                            if ((Player2Controller.Buttons.A == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed) && !isKeyDownP2 && !TowerList[(int)Player.P2].Destroyed && TowerList[(int)Player.P2].CurrentAmmo > 0)
                            {
                                TowerList[(int)Player.P2].Shoot();

                                isKeyDownP2 = true;
                                System.Drawing.Point originPoint = TowerList[(int)Player.P2].GunPosition;
                                System.Drawing.Point destinationPoint = new System.Drawing.Point(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                                PlayerBomb db = new PlayerBomb(originPoint, destinationPoint, this, (int)Player.P2, Link, PSettings);
                                db.DestroyBomb += RemoveBomb;
                                BombList.Add(db);
                            }
                        }
                        else
                        {
                            PSettings.Player2Controller = false;
                            if (isPlaying)
                                ChangeGameState();

                            MessageBox.Show("No controller found for Player 2, switching to keyboard");

                            if (!isPlaying)
                                ChangeGameState();
                        }
                    }
                    else
                    {
                        // Player 2
                        if ((Player2 & KPress.left) == KPress.left)
                        {
                            CursorList[(int)Player.P2].move((int)KPress.left);
                            TowerList[(int)Player.P2].TurretCalc(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                        }

                        if ((Player2 & KPress.right) == KPress.right)
                        {
                            CursorList[(int)Player.P2].move((int)KPress.right);
                            TowerList[(int)Player.P2].TurretCalc(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                        }

                        if ((Player2 & KPress.down) == KPress.down)
                        {
                            CursorList[(int)Player.P2].move((int)KPress.down);
                            TowerList[(int)Player.P2].TurretCalc(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                        }

                        if ((Player2 & KPress.up) == KPress.up)
                        {
                            CursorList[(int)Player.P2].move((int)KPress.up);
                            TowerList[(int)Player.P2].TurretCalc(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                        }

                        if ((Player2 & KPress.shoot) == KPress.shoot && !isKeyDownP2 && !TowerList[(int)Player.P2].Destroyed && TowerList[(int)Player.P2].CurrentAmmo > 0)
                        {
                            TowerList[(int)Player.P2].Shoot();

                            isKeyDownP2 = true;
                            System.Drawing.Point originPoint = TowerList[(int)Player.P2].GunPosition;
                            System.Drawing.Point destinationPoint = new System.Drawing.Point(CursorList[(int)Player.P2].PosX, CursorList[(int)Player.P2].PosY);
                            PlayerBomb db = new PlayerBomb(originPoint, destinationPoint, this, (int)Player.P2, Link, PSettings);
                            db.DestroyBomb += RemoveBomb;
                            BombList.Add(db);
                        }
                    }
                }

                if (numPlayers > 2)
                {
                    if (PSettings.Player3Controller)
                    {
                        GamePadState Player3Controller;
                        if (PSettings.Player1Controller)
                            Player3Controller = GamePad.GetState(PlayerIndex.Two);
                        else if (PSettings.Player2Controller)
                            Player3Controller = GamePad.GetState(PlayerIndex.Three);
                        else
                            Player3Controller = GamePad.GetState(PlayerIndex.One);

                        if (Player3Controller.IsConnected)
                        {
                            if ((Convert.ToInt16(Player3Controller.ThumbStick­s.Left.X)) == 1)
                            {
                                CursorList[(int)Player.P3].move((int)KPress.right);
                                TowerList[(int)Player.P3].TurretCalc(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                            }

                            if ((Convert.ToInt16(Player3Controller.ThumbStick­s.Left.X)) == -1)
                            {
                                CursorList[(int)Player.P3].move((int)KPress.left);
                                TowerList[(int)Player.P3].TurretCalc(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                            }

                            if ((Convert.ToInt16(Player3Controller.ThumbStick­s.Left.Y)) == 1)
                            {
                                CursorList[(int)Player.P3].move((int)KPress.up);
                                TowerList[(int)Player.P3].TurretCalc(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                            }

                            if ((Convert.ToInt16(Player3Controller.ThumbStick­s.Left.Y)) == -1)
                            {
                                CursorList[(int)Player.P3].move((int)KPress.down);
                                TowerList[(int)Player.P3].TurretCalc(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                            }

                            if (Player3Controller.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Released)
                            {
                                isKeyDownP3 = false;
                            }

                            if (Player3Controller.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                            {
                                if (isPlaying)
                                {
                                    ChangeGameState();
                                    myStartMenu.ShowMenu();
                                }
                            }

                            if ((Player3Controller.Buttons.A == Microsoft.Xna.Framework.Input.ButtonStat­e.Pressed) && !isKeyDownP3 && !TowerList[(int)Player.P3].Destroyed && TowerList[(int)Player.P3].CurrentAmmo > 0)
                            {
                                TowerList[(int)Player.P3].Shoot();

                                isKeyDownP3 = true;
                                System.Drawing.Point originPoint = TowerList[(int)Player.P3].GunPosition;
                                System.Drawing.Point destinationPoint = new System.Drawing.Point(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                                PlayerBomb db = new PlayerBomb(originPoint, destinationPoint, this, (int)Player.P3, Link, PSettings);
                                db.DestroyBomb += RemoveBomb;
                                BombList.Add(db);
                            }
                        }
                        else
                        {
                            PSettings.Player3Controller = false;
                            if (isPlaying)
                                ChangeGameState();

                            MessageBox.Show("No controller found for Player 3, switching to keyboard");

                            if (!isPlaying)
                                ChangeGameState();
                        }
                    }
                    else
                    {
                        // Player 3
                        if ((Player3 & KPress.left) == KPress.left)
                        {
                            CursorList[(int)Player.P3].move((int)KPress.left);
                            TowerList[(int)Player.P3].TurretCalc(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                        }

                        if ((Player3 & KPress.right) == KPress.right)
                        {
                            CursorList[(int)Player.P3].move((int)KPress.right);
                            TowerList[(int)Player.P3].TurretCalc(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                        }

                        if ((Player3 & KPress.down) == KPress.down)
                        {
                            CursorList[(int)Player.P3].move((int)KPress.down);
                            TowerList[(int)Player.P3].TurretCalc(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                        }

                        if ((Player3 & KPress.up) == KPress.up)
                        {
                            CursorList[(int)Player.P3].move((int)KPress.up);
                            TowerList[(int)Player.P3].TurretCalc(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                        }

                        if ((Player3 & KPress.shoot) == KPress.shoot && !isKeyDownP3 && !TowerList[(int)Player.P3].Destroyed && TowerList[(int)Player.P3].CurrentAmmo > 0)
                        {
                            TowerList[(int)Player.P3].Shoot();

                            isKeyDownP3 = true;
                            System.Drawing.Point originPoint = TowerList[(int)Player.P3].GunPosition;
                            System.Drawing.Point destinationPoint = new System.Drawing.Point(CursorList[(int)Player.P3].PosX, CursorList[(int)Player.P3].PosY);
                            PlayerBomb db = new PlayerBomb(originPoint, destinationPoint, this, (int)Player.P3, Link, PSettings);
                            db.DestroyBomb += RemoveBomb;
                            BombList.Add(db);
                        }
                    }
                }

                if (numPlayers > 3)
                {
                    if (PSettings.Player4Controller)
                    {
                        GamePadState Player4Controller;
                        if (PSettings.Player1Controller)
                            Player4Controller = GamePad.GetState(PlayerIndex.Two);
                        else if (PSettings.Player2Controller)
                            Player4Controller = GamePad.GetState(PlayerIndex.Three);
                        else if (PSettings.Player3Controller)
                            Player4Controller = GamePad.GetState(PlayerIndex.Four);
                        else
                            Player4Controller = GamePad.GetState(PlayerIndex.One);

                        if (Player4Controller.IsConnected)
                        {

                            if ((Convert.ToInt16(Player4Controller.ThumbStick­s.Left.X)) == 1)
                            {
                                CursorList[(int)Player.P4].move((int)KPress.right);
                                TowerList[(int)Player.P4].TurretCalc(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                            }

                            if ((Convert.ToInt16(Player4Controller.ThumbStick­s.Left.X)) == -1)
                            {
                                CursorList[(int)Player.P4].move((int)KPress.left);
                                TowerList[(int)Player.P4].TurretCalc(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                            }

                            if ((Convert.ToInt16(Player4Controller.ThumbStick­s.Left.Y)) == 1)
                            {
                                CursorList[(int)Player.P4].move((int)KPress.up);
                                TowerList[(int)Player.P4].TurretCalc(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                            }

                            if ((Convert.ToInt16(Player4Controller.ThumbStick­s.Left.Y)) == -1)
                            {
                                CursorList[(int)Player.P4].move((int)KPress.down);
                                TowerList[(int)Player.P4].TurretCalc(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                            }

                            if (Player4Controller.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Released)
                            {
                                isKeyDownP4 = false;
                            }

                            if (Player4Controller.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                            {
                                if (isPlaying)
                                {
                                    ChangeGameState();
                                    myStartMenu.ShowMenu();
                                }
                            }

                            if ((Player4Controller.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed) && !isKeyDownP4 && !TowerList[(int)Player.P4].Destroyed && TowerList[(int)Player.P4].CurrentAmmo > 0)
                            {
                                TowerList[(int)Player.P4].Shoot();

                                isKeyDownP4 = true;
                                System.Drawing.Point originPoint = TowerList[(int)Player.P4].GunPosition;
                                System.Drawing.Point destinationPoint = new System.Drawing.Point(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                                PlayerBomb db = new PlayerBomb(originPoint, destinationPoint, this, (int)Player.P4, Link, PSettings);
                                db.DestroyBomb += RemoveBomb;
                                BombList.Add(db);
                            }
                        }
                        else
                        {
                            PSettings.Player4Controller = false;
                            if (isPlaying)
                                ChangeGameState();

                            MessageBox.Show("No controller found for Player 4, switching to keyboard");

                            if (!isPlaying)
                                ChangeGameState();
                        }
                    }
                    else
                    {
                        // if player 2 is using a controller then use keyboard.
                        if ((Player4 & KPress.left) == KPress.left)
                        {
                            CursorList[(int)Player.P4].move((int)KPress.left);
                            TowerList[(int)Player.P4].TurretCalc(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                        }

                        if ((Player4 & KPress.right) == KPress.right)
                        {
                            CursorList[(int)Player.P4].move((int)KPress.right);
                            TowerList[(int)Player.P4].TurretCalc(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                        }

                        if ((Player4 & KPress.down) == KPress.down)
                        {
                            CursorList[(int)Player.P4].move((int)KPress.down);
                            TowerList[(int)Player.P4].TurretCalc(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                        }

                        if ((Player4 & KPress.up) == KPress.up)
                        {
                            CursorList[(int)Player.P4].move((int)KPress.up);
                            TowerList[(int)Player.P4].TurretCalc(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                        }

                        if ((Player4 & KPress.shoot) == KPress.shoot && !isKeyDownP4 && !TowerList[(int)Player.P4].Destroyed && TowerList[(int)Player.P4].CurrentAmmo > 0)
                        {
                            TowerList[(int)Player.P4].Shoot();

                            isKeyDownP4 = true;
                            System.Drawing.Point originPoint = TowerList[(int)Player.P4].GunPosition;
                            System.Drawing.Point destinationPoint = new System.Drawing.Point(CursorList[(int)Player.P4].PosX, CursorList[(int)Player.P4].PosY);
                            PlayerBomb db = new PlayerBomb(originPoint, destinationPoint, this, (int)Player.P4, Link, PSettings);
                            db.DestroyBomb += RemoveBomb;
                            BombList.Add(db);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks for key down on keypresses, doesnt need to have a check if the player exists because it wont do anything if they don't.
        /// </summary>
        private void CommandForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Player 1
            if (e.KeyData == System.Windows.Forms.Keys.A)
                Player1 |= KPress.left;
            if (e.KeyData == System.Windows.Forms.Keys.W)
                Player1 |= KPress.up;
            if (e.KeyData == System.Windows.Forms.Keys.D)
                Player1 |= KPress.right;
            if (e.KeyData == System.Windows.Forms.Keys.S)
                Player1 |= KPress.down;
            if (e.KeyData == System.Windows.Forms.Keys.Space)
                Player1 |= KPress.shoot;

            if (numPlayers > 1)
            {
                // Player 2
                if (e.KeyData == System.Windows.Forms.Keys.Left)
                    Player2 |= KPress.left;
                if (e.KeyData == System.Windows.Forms.Keys.Up)
                    Player2 |= KPress.up;
                if (e.KeyData == System.Windows.Forms.Keys.Right)
                    Player2 |= KPress.right;
                if (e.KeyData == System.Windows.Forms.Keys.Down)
                    Player2 |= KPress.down;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad0)
                    Player2 |= KPress.shoot;
            }

            if (numPlayers > 2)
            {
                // Player 3
                if (e.KeyData == System.Windows.Forms.Keys.J)
                    Player3 |= KPress.left;
                if (e.KeyData == System.Windows.Forms.Keys.I)
                    Player3 |= KPress.up;
                if (e.KeyData == System.Windows.Forms.Keys.L)
                    Player3 |= KPress.right;
                if (e.KeyData == System.Windows.Forms.Keys.K)
                    Player3 |= KPress.down;
                if (e.KeyData == System.Windows.Forms.Keys.Oem1)
                    Player3 |= KPress.shoot;
            }

            if (numPlayers > 3)
            {
                // Player 2
                if (e.KeyData == System.Windows.Forms.Keys.NumPad4)
                    Player4 |= KPress.left;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad8)
                    Player4 |= KPress.up;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad6)
                    Player4 |= KPress.right;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad2)
                    Player4 |= KPress.down;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad5)
                    Player4 |= KPress.shoot;
            }
        }

        /// <summary>
        /// Checks for key up on keypresses, doesnt need to have a check if the player exists because it wont do anything if they don't.
        /// </summary>
        private void CommandForm_KeyUp(object sender, KeyEventArgs e)
        {
            // Player 1
            if (e.KeyData == System.Windows.Forms.Keys.A)
                Player1 &= ~KPress.left;
            if (e.KeyData == System.Windows.Forms.Keys.W)
                Player1 &= ~KPress.up;
            if (e.KeyData == System.Windows.Forms.Keys.D)
                Player1 &= ~KPress.right;
            if (e.KeyData == System.Windows.Forms.Keys.S)
                Player1 &= ~KPress.down;
            if (e.KeyData == System.Windows.Forms.Keys.Space)
            {
                Player1 &= ~KPress.shoot;
                isKeyDownP1 = false;
            }

            if (numPlayers > 1)
            {
                // Player 2
                if (e.KeyData == System.Windows.Forms.Keys.Left)
                    Player2 &= ~KPress.left;
                if (e.KeyData == System.Windows.Forms.Keys.Up)
                    Player2 &= ~KPress.up;
                if (e.KeyData == System.Windows.Forms.Keys.Right)
                    Player2 &= ~KPress.right;
                if (e.KeyData == System.Windows.Forms.Keys.Down)
                    Player2 &= ~KPress.down;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad0)
                {
                    Player2 &= ~KPress.shoot;
                    isKeyDownP2 = false;
                }
            }

            if (numPlayers > 2)
            {
                // Player 3
                if (e.KeyData == System.Windows.Forms.Keys.J)
                    Player3 &= ~KPress.left;
                if (e.KeyData == System.Windows.Forms.Keys.I)
                    Player3 &= ~KPress.up;
                if (e.KeyData == System.Windows.Forms.Keys.L)
                    Player3 &= ~KPress.right;
                if (e.KeyData == System.Windows.Forms.Keys.K)
                    Player3 &= ~KPress.down;
                if (e.KeyData == System.Windows.Forms.Keys.Oem1)
                {
                    Player3 &= ~KPress.shoot;
                    isKeyDownP3 = false;
                }
            }

            if (numPlayers > 3)
            {
                // Player 4
                if (e.KeyData == System.Windows.Forms.Keys.NumPad4)
                    Player4 &= ~KPress.left;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad8)
                    Player4 &= ~KPress.up;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad6)
                    Player4 &= ~KPress.right;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad2)
                    Player4 &= ~KPress.down;
                if (e.KeyData == System.Windows.Forms.Keys.NumPad5)
                {
                    Player4 &= ~KPress.shoot;
                    isKeyDownP4 = false;
                }
            }
        }
        
        /// <summary>
        /// Overrides the keypresses for enter and esc if those keys are not pressed then it runs keypresses normally.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == System.Windows.Forms.Keys.Enter && !gameOver)
            {
                if (isPlaying)
                {
                    ChangeGameState();
                    myStartMenu.ShowMenu();
                    return true;
                }
            }

            if (keyData == System.Windows.Forms.Keys.Escape)
            {
                if (isPlaying)
                {
                    ChangeGameState();
                    DialogResult dialogResult = MessageBox.Show("Are sure you want to quit?", "Exit Game", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                        this.Close();
                    else
                        ChangeGameState();
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
