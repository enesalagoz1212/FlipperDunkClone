using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using FlipperDunkClone.Controllers;

namespace FlipperDunkClone.Managers
{
	public class ShopManager : MonoBehaviour
	{

		private BallController _ballController;
		private PlayerController _playerController;

		public Sprite[] ballSprites;
		public Sprite[] flipperSprites;
		public Sprite[] backgroundSprites;


		public void Initialize(BallController ballController,PlayerController playerController)
		{
			_playerController = playerController;
			_ballController = ballController;
			int selectedBallIndex = PlayerPrefsManager.SelectedBall;
			_ballController.ChangeBallImage(ballSprites[selectedBallIndex]);
		}

		private void OnEnable()
		{
			GameManager.OnGameEnd += OnGameEnd;

		}

		private void OnDisable()
		{
			GameManager.OnGameEnd -= OnGameEnd;

		}
		private void Start()
		{

		}

		private void OnGameEnd(bool isSuccesful)
		{


		}

		public void OnBallButtonClick(int ballIndex)
		{
			PlayerPrefsManager.SelectedBall = ballIndex;
			Debug.Log("Selected ball: " + (ballIndex));

			_ballController.ChangeBallImage(ballSprites[ballIndex]);
		}

		public void OnFlipperButtonClick(int flipperIndex)
		{
			PlayerPrefsManager.SelectedFlipper = flipperIndex;
			Debug.Log("Selected Flipper: " + (flipperIndex));

			_playerController.ChangeFlipperImage(flipperSprites[flipperIndex]);

		}
	}
}

