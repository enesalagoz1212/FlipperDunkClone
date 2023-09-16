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
		private ParticlePool _particlePool;

		public GameObject endPanel;
		public Button nextButton;

		public TextMeshProUGUI endLevelText;
		public TextMeshProUGUI diamondText;
		
		private void OnEnable()
		{
			GameManager.OnDiamondScored += OnDiamondScore;
		}
		private void OnDisable()
		{
			GameManager.OnDiamondScored -= OnDiamondScore;
		}

		public void Initialize(SoundManager soundManager,ParticlePool particlePool)
		{
			_soundManager = soundManager;
			_particlePool = particlePool;
			nextButton.onClick.AddListener(NextButtonClicked);
		}
		
		private void Start()
		{
			endPanel.SetActive(false);
		}

		private void NextButtonClicked()
		{
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

		private void OnDiamondScore(int score)
		{
			diamondText.text = " " + PlayerPrefsManager.DiamondScore.ToString();
		}
	}
}

