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

		public GameObject resetPanel;
		public GameObject endPanel;
		public Button restartButton;
		public Button nextButton;

		private void OnEnable()
		{
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameEnd -= OnGameEnd;

		}

		public void Initialize(BallController ballController)
		{
			_ballController = ballController;

		}
		void Start()
		{
			resetPanel.SetActive(false);
			endPanel.SetActive(false);
			restartButton.onClick.AddListener(OnRestartButtonClicked);
			nextButton.onClick.AddListener(NextButtonClicked);
		}

		private void NextButtonClicked()
		{
			LevelManager.Instance.NextLevel();
			endPanel.SetActive(false);
		}
		private void OnRestartButtonClicked()
		{
			GameManager.Instance.RestartGame();

			resetPanel.SetActive(false);
		}


		private void OnGameReset()
		{
			resetPanel.SetActive(true);
		}
		private void OnGameEnd()
		{
			endPanel.SetActive(true);
		}
	}
}

