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

			LevelManager.Instance.NextLevel();
			GameManager.Instance.ResetGame();

			endPanel.SetActive(false);
		}


		private void OnGameEnd()
		{
			endPanel.SetActive(true);
		}
	}
}

