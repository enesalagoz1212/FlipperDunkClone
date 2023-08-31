using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Managers;
using FlipperDunkClone.Controllers;

namespace FlipperDunkClone.Canvases
{
	public class EndCanvas : MonoBehaviour
	{
		BallController _ballController;
		public GameObject Ball;

		public GameObject endPanel;
		public Button nextButton;

		private void OnEnable()
		{
			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{
			GameManager.OnGameEnd -= OnGameEnd;

		}

		public void Initialize(BallController ballController)
		{
			_ballController = ballController;

		}
		void Start()
		{
			endPanel.SetActive(false);
			nextButton.onClick.AddListener(NextButtonClicked);
		}

		private void NextButtonClicked()
		{
			GameManager.Instance.ResetGame();

			GameManager.Instance.ChangeState(GameState.Start);
			GameManager.OnGameStarted?.Invoke();

			LevelManager.Instance.NextLevel();

			Ball.gameObject.SetActive(true);
			_ballController.BallTransformPosition();

			endPanel.SetActive(false);
		}


		private void OnGameEnd(bool IsSuccessful)
		{
			if (IsSuccessful)
			{
				endPanel.SetActive(true);
			}
		}
	}
}

