using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Managers;
using FlipperDunkClone.Controllers;
using TMPro;

namespace FlipperDunkClone.Canvases
{
	public class EndCanvas : MonoBehaviour
	{
		public GameObject endPanel;
		public Button nextButton;

		public TextMeshProUGUI endLevelText;
		public TextMeshProUGUI diamondText;
		
		private void OnEnable()
		{
			GameManager.OnGameEnd += OnGameEnd;
			GameManager.OnDiamondScored += OnDiamondScore;
		}
		private void OnDisable()
		{
			GameManager.OnGameEnd -= OnGameEnd;
			GameManager.OnDiamondScored -= OnDiamondScore;
		}

		public void Initialize()
		{
			
		}
		
		void Start()
		{
			endPanel.SetActive(false);
			nextButton.onClick.AddListener(NextButtonClicked);
		}

		private void NextButtonClicked()
		{
			GameManager.Instance.ResetGame();
			LevelManager.Instance.NextLevel();

			endPanel.SetActive(false);
		}


		private void OnGameEnd(bool IsSuccessful)
		{
			if (IsSuccessful)
			{
				endPanel.SetActive(true);
			}
		}

		public void UpdateEndLevelText(int endLevel)
		{
			endLevelText.text = "LEVEL " + endLevel.ToString();
		}

		private void OnDiamondScore(int score)
		{
			diamondText.text = " " + PlayerPrefsManager.DiamondScore.ToString();
		}
	}
}

