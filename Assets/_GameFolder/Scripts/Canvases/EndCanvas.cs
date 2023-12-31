using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Managers;
using FlipperDunkClone.Controllers;
using TMPro;
using UnityEngine.PlayerLoop;
using FlipperDunkClone.Pooling;
using DG.Tweening;

namespace FlipperDunkClone.Canvases
{
	public class EndCanvas : MonoBehaviour
	{
		private SoundManager _soundManager;

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

		public void Initialize(SoundManager soundManager,ParticlePool particlePool)
		{
			_soundManager = soundManager;
			nextButton.onClick.AddListener(NextButtonClicked);
		}
		
		private void Start()
		{
			endPanel.SetActive(false);
		}

		private void NextButtonClicked()
		{
			nextButton.interactable = false;
			GameManager.Instance.ResetGame();
			LevelManager.Instance.NextLevel();
			endPanel.SetActive(false);
			_soundManager.PlayLevelCompletedSound();
		}
		
		public void OnGameSuccess()
		{
			endPanel.SetActive(true);
			UpdateEndLevelText();

		}

		private void UpdateEndLevelText()
		{
			var finishedLevel = PlayerPrefsManager.CurrentLevel - 1;
			endLevelText.text = "LEVEL " + finishedLevel.ToString();
		}

		private void OnGameEnd(bool isSuccesful)
		{
			nextButton.interactable = true;
		}

		private void OnDiamondScore(int score)
		{
			diamondText.text = " " + PlayerPrefsManager.DiamondScore.ToString();
		}
	}
}

