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
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{
			GameManager.OnGameScoredecreased -= OnGameScoredecreased;
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameEnd -= OnGameEnd;
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
			UpdateLevelDataMaxScore();
		}

		private void OnGameReset()
		{
			
		}

		private void OnGameEnd()
		{
			levelIndex++;
		}

		private void OnGameScoredecreased()
		{
			UpdateScoreText();
		}

		public void UpdateScoreText()
		{
			scoreText.text = GameManager.Instance.currentScore.ToString();

		}

		public void UpdateLevelDataMaxScore()
		{
			if (levelIndex >= 0 && levelIndex < LevelManager.Instance.levelDataArray.Length)
			{
				int maxScore = LevelManager.Instance.levelDataArray[levelIndex].maxScore;			
				scoreText.text = maxScore.ToString();
			}
		}
	}
}

