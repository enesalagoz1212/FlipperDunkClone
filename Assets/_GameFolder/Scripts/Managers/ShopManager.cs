using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using FlipperDunkClone.Controllers;
using System;

namespace FlipperDunkClone.Managers
{
	public class ShopManager : MonoBehaviour
	{
		public static ShopManager Instance { get; private set; }

		private LevelManager _levelManager;
		private BallController _ballController;
		private PlayerController _playerController;

		public static Action<Sprite> OnBallSelected;
		public static Action<Sprite> OnFlipperSelected;
		public static Action<Sprite> OnBackgroundSelected;

		public Sprite[] ballSprites;
		public Sprite[] flipperSprites;
		public Sprite[] backgroundSprites;

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}
		public void Initialize(LevelManager levelManager, BallController ballController, PlayerController playerController)
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

			OnBallSelected?.Invoke(ballSprites[ballIndex]);
		}


		public void OnFlipperButtonClick(int flipperIndex)
		{
			PlayerPrefsManager.SelectedFlipper = flipperIndex;
			Debug.Log("Selected Flipper: " + (flipperIndex));

			OnFlipperSelected?.Invoke(flipperSprites[flipperIndex]);
		}

		public void OnBackgroundButtonClick(int backgroundIndex)
		{
			PlayerPrefsManager.SelectedBackground = backgroundIndex;
			Debug.Log("Selected Flipper: " + (backgroundIndex));

			OnBackgroundSelected?.Invoke(backgroundSprites[backgroundIndex]);
		}
	}
}

