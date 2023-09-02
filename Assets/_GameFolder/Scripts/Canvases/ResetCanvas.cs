using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Controllers;
using FlipperDunkClone.Managers;
using TMPro;

namespace FlipperDunkClone.Canvases
{
	public class ResetCanvas : MonoBehaviour
	{
		BallController _ballController;

		public GameObject resetPanel;
		public Button restartButton;

		public TextMeshProUGUI resetLevelText;
		public TextMeshProUGUI resetDiamondScore;
		public void Initialize(BallController ballController)
		{
			_ballController = ballController;

		}

		private void OnEnable()
		{
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
			GameManager.OnDiamondScored += OnDiamondScore;

		}
		private void OnDisable()
		{
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameEnd -= OnGameEnd;
			GameManager.OnDiamondScored = OnDiamondScore;


		}
		void Start()
		{
			resetPanel.SetActive(false);
			restartButton.onClick.AddListener(OnRestartButtonClicked);
		}

		private void OnRestartButtonClicked()
		{
			GameManager.Instance.ResetGame();
			
			resetPanel.SetActive(false);
		}

		private void ResetPanelGame()
		{
			resetPanel.SetActive(true);
		}

		private void OnGameEnd(bool IsSuccessful)
		{
			if (!IsSuccessful)
			{
				ResetPanelGame();
			}
		}
		private void OnGameReset()
		{

		}
		
		public void UpdateResetLevelText(int LevelText)
		{
			resetLevelText.text = "LEVEL " + LevelText.ToString();
		}

		private void OnDiamondScore(int score)
		{
			resetDiamondScore.text = PlayerPrefsManager.DiamondScore.ToString();
		}

		

	}

}

