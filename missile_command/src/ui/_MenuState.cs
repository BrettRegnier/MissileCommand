using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class MenuState : State
	{
		List<Component> lComponents = new List<Component>();
		public MenuState(Window g) : base(g)
		{
<<<<<<< Updated upstream
			Button newGameButton = new Button("New Game", Utils.gameBounds.Width / 2, 200, 100, 30);
=======
			int numButton = 0;
			int startX = Utils.gameBounds.Width / 2;
			int startY = 200;
			int btnWidth = 100;
			int btnHeight = 30;

			Dropdown newGameButton = new Dropdown("New Game", startX, startY + Utils.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
>>>>>>> Stashed changes
			newGameButton.Click += NewGameButton_Click;
			lComponents.Add(newGameButton);

<<<<<<< Updated upstream
			Button highScoresButton = new Button("Highscores", Utils.gameBounds.Width / 2, 240, 100, 30);
=======
			// Move the button slightly to the right.
			DropdownButton OnePlayer = new DropdownButton("1 Player", startX + 5, startY + Utils.SEPERATION_VALUE * numButton, btnWidth - 10, btnHeight);
			OnePlayer.Click += (sender, e) => { players = 1; };
			OnePlayer.IsVisible = false;
			OnePlayer.PrevButton = newGameButton;

			// Move the button slightly to the right.
			DropdownButton TwoPlayers = new DropdownButton("2 Players", startX + 5, startY + Utils.SEPERATION_VALUE * numButton, btnWidth - 10, btnHeight);
			TwoPlayers.Click += (sender, e) => { players = 2; };
			TwoPlayers.IsVisible = false;
			TwoPlayers.PrevButton = OnePlayer;
			OnePlayer.NextButton = TwoPlayers;

			// Move the button slightly to the right.
			DropdownButton ThreePlayers = new DropdownButton("3 Players",  startX + 5, startY + Utils.SEPERATION_VALUE * numButton, btnWidth - 10, btnHeight);
			ThreePlayers.Click += (sender, e) => { players = 3; };
			ThreePlayers.IsVisible = false;
			ThreePlayers.PrevButton = TwoPlayers;
			TwoPlayers.NextButton = ThreePlayers;
			numButton++;

			GameButton highScoresButton = new GameButton("Highscores", startX, startY + Utils.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
>>>>>>> Stashed changes
			highScoresButton.Click += HighScoresButton_Click;
			lComponents.Add(highScoresButton);

<<<<<<< Updated upstream
			Button exitButton = new Button("Exit", Utils.gameBounds.Width / 2, 280, 100, 30);
=======
			GameButton exitButton = new GameButton("Exit", startX, startY + Utils.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
>>>>>>> Stashed changes
			exitButton.Click += ExitButton_Click;
			lComponents.Add(exitButton);
		}
		public override void Update()
		{
			throw new NotImplementedException();
		}
		public override void PostUpdate()
		{
			throw new NotImplementedException();
		}
		public override void Draw(Graphics g)
		{
			foreach (Component component in lComponents)
				component.Draw(g);
		}

		private void NewGameButton_Click(object sender, EventArgs e)
		{
<<<<<<< Updated upstream
			throw new NotImplementedException();
=======
			((Dropdown)sender).DropDown();
			//game.NextState(new GameState(1, new GameMode(), game));
>>>>>>> Stashed changes
		}
		private void HighScoresButton_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
		private void ExitButton_Click(object sender, EventArgs e)
		{
			game.Close();
		}
	}
}
