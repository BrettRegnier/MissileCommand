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
		int players = 0;

		List<Component> lComponents = new List<Component>();
		public MenuState(Window g) : base(g)
		{
			int numButton = 0;
			int startX = Utils.gameBounds.Width / 2;
			int startY = 200;
			int variableHeight = 40;
			int btnWidth = 100;
			int btnHeight = 30;

			Dropdown newGameButton = new Dropdown("New Game", startX, startY + variableHeight * numButton, btnWidth, btnHeight);
			newGameButton.Click += NewGameButton_Click;

			// Move the button slightly to the right.
			GameButton OnePlayer = new GameButton("1 Player", startX + 5, startY + variableHeight * numButton, btnWidth - 10, btnHeight);
			OnePlayer.Click += (sender, e) => { players = 1; };
			OnePlayer.IsVisible = false;
			OnePlayer.PrevButton = newGameButton;

			// Move the button slightly to the right.
			GameButton TwoPlayers = new GameButton("2 Players", startX + 5, startY + variableHeight * numButton, btnWidth - 10, btnHeight);
			TwoPlayers.Click += (sender, e) => { players = 2; };
			TwoPlayers.IsVisible = false;
			TwoPlayers.PrevButton = OnePlayer;
			OnePlayer.NextButton = TwoPlayers;

			// Move the button slightly to the right.
			GameButton ThreePlayers = new GameButton("3 Players",  startX + 5, startY + variableHeight * numButton, btnWidth - 10, btnHeight);
			ThreePlayers.Click += (sender, e) => { players = 3; };
			ThreePlayers.IsVisible = false;
			ThreePlayers.PrevButton = TwoPlayers;
			TwoPlayers.NextButton = ThreePlayers;
			numButton++;

			GameButton highScoresButton = new GameButton("Highscores", startX, startY + variableHeight * numButton, btnWidth, btnHeight);
			highScoresButton.Click += HighScoresButton_Click;
			highScoresButton.PrevButton = ThreePlayers;
			ThreePlayers.NextButton = highScoresButton;
			numButton++;

			GameButton exitButton = new GameButton("Exit", startX, startY + variableHeight * numButton, btnWidth, btnHeight);
			exitButton.Click += ExitButton_Click;
			exitButton.PrevButton = highScoresButton;
			highScoresButton.NextButton = exitButton;
			exitButton.NextButton = new GameButton(10,10,10,10);//dummy button till I have null hceking
			numButton++;

			//highScoresButton.IsVisible = false;
			//exitButton.IsVisible = false;
			
			lComponents.Add(highScoresButton);
			lComponents.Add(exitButton);
			lComponents.Add(newGameButton);
			newGameButton.AttachButton(OnePlayer);
			newGameButton.AttachButton(TwoPlayers);
			newGameButton.AttachButton(ThreePlayers);
		}
		public override void Draw(Graphics g)
		{
			foreach (Component component in lComponents)
				component.Draw(g);
		}
		public override void Update(long gameTime)
		{
			foreach (Component component in lComponents)
				component.Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			foreach (Component component in lComponents)
				component.PostUpdate(gameTime);
		}

		private void NewGameButton_Click(object sender, EventArgs e)
		{
			((Dropdown)sender).DropDown();
			game.NextState(new GameState(1, new GameMode(), game));
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
