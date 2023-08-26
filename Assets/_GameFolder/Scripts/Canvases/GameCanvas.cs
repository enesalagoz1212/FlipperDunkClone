using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using TMPro;
using FlipperDunkClone.ScriptableObjects;

namespace FlipperDunkClone.Canvases
{
	public class GameCanvas : MonoBehaviour
	{
		public TextMeshProUGUI scoreText;

		LevelData levelData;

		private SettingsCanvas _settingCanvas;
		private LevelManager _levelManager;

		private int levelIndex = 0;
		private void OnEnable()
		{
			GameManager.OnGameScoredecreased += OnGameScoredecreased;
			GameManager.OnGameStarted += OnGameStart;
		}
		private void OnDisable()
		{
			GameManager.OnGameScoredecreased -= OnGameScoredecreased;
			GameManager.OnGameStarted -= OnGameStart;
		}

		public void Initialize(LevelManager levelManager, SettingsCanvas settingsCanvas)
		{
			_settingCanvas = settingsCanvas;
			_levelManager = levelManager;

		}
		void Start()
		{

		}

		private void OnGameStart()
		{
			UpdateScoreText();
		}

		private void OnGameScoredecreased()
		{
			UpdateScoreText();
		}

		public void UpdateScoreText()
		{
			scoreText.text = GameManager.Instance.currentScore.ToString();

		}

	}
}

