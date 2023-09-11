using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using FlipperDunkClone.Controllers;

namespace FlipperDunkClone.Managers
{
	public class ShopManager : MonoBehaviour
	{
		private LevelManager _levelManager;
		private BallController _ballController;
		private PlayerController _playerController;

		public Sprite[] ballSprites;
		public Sprite[] flipperSprites;
		public Sprite[] backgroundSprites;


		public void Initialize( LevelManager levelManager,BallController ballController,PlayerController playerController)
		{
			_playerController = playerController;
			_ballController = ballController;
			_levelManager = levelManager;

			int selectedBallIndex = PlayerPrefsManager.SelectedBall;
			_ballController.ChangeBallImage(ballSprites[selectedBallIndex]);

			int selectedFlipperIndex = PlayerPrefsManager.SelectedFlipper;
			_playerController.ChangeFlipperImage(flipperSprites[selectedFlipperIndex]);

			int selectedBackgroundIndex = PlayerPrefsManager.SelectedBackground;
			_levelManager.ChangeBackgroundImage(backgroundSprites[selectedBackgroundIndex]);
		}


		public void OnBallButtonClick(int ballIndex)
		{
			PlayerPrefsManager.SelectedBall = ballIndex;
			Debug.Log("Selected ball: " + (ballIndex));

			GameManager.OnBallSelected?.Invoke(ballSprites[ballIndex]);
		}
	

		public void OnFlipperButtonClick(int flipperIndex)
		{
			PlayerPrefsManager.SelectedFlipper = flipperIndex;
			Debug.Log("Selected Flipper: " + (flipperIndex));

			GameManager.OnFlipperSelected?.Invoke(flipperSprites[flipperIndex]);
		}

		public void OnBackgroundButtonClick(int backgroundIndex)
		{
			PlayerPrefsManager.SelectedBackground = backgroundIndex;
			Debug.Log("Selected Flipper: " + (backgroundIndex));

			GameManager.OnBackgroundSelected?.Invoke(backgroundSprites[backgroundIndex]);
		}
	}
}

