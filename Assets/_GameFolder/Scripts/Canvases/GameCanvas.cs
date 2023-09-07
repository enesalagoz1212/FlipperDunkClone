using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using TMPro;
using FlipperDunkClone.ScriptableObjects;
using UnityEngine.UI;

namespace FlipperDunkClone.Canvases
{
	public class GameCanvas : MonoBehaviour
	{
		public TextMeshProUGUI scoreText;
		public TextMeshProUGUI levelsText;
		public TextMeshProUGUI startText;

		public Image gameImageBackground;

		LevelData levelData;

		private SettingsCanvas _settingCanvas;
		private LevelManager _levelManager;

		private int levelIndex = 0;

		private void OnEnable()
		{
			GameManager.OnGameScoreChanged += OnGameScoreChanged;
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
		}

		private void OnDisable()
		{
			GameManager.OnGameScoreChanged -= OnGameScoreChanged;
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

		private void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					break;
				case GameState.Playing:
					if (Input.GetMouseButtonDown(0))
					{
						gameImageBackground.gameObject.SetActive(false);
					}
					break;
				case GameState.Reset:
					break;
				case GameState.End:
					break;
				default:
					break;
			}
		}

		public void OnSettingButtonClick()
		{
			if (_settingCanvas != null)
			{
				_settingCanvas.ChangeSettingButtonInteractable();
			}
		}

		private void OnGameStart()
		{
			gameImageBackground.gameObject.SetActive(true);
			UpdateLevelsText();
			UpdateLevelDataMaxScore();
		}

		private void OnGameReset()
		{
			UpdateLevelDataMaxScore();
		}

		private void OnGameEnd(bool IsSuccessful)
		{
			if (IsSuccessful)
			{
				levelIndex++;
			}
		}


		private void OnGameScoreChanged(int score)
		{
			UpdateScoreText(score);
		}

		public void UpdateLevelsText()
		{
			levelsText.text = "LEVEL " + PlayerPrefsManager.CurrentLevel;
		}

		public void UpdateScoreText(int score)
		{
			//int maxScore = LevelManager.Instance.levelDataArray[levelIndex].maxScore;
			scoreText.text = score.ToString();
		}

		public void UpdateLevelDataMaxScore()
		{
			if (levelIndex >= 0 && levelIndex <= LevelManager.Instance.levelDataArray.Length)
			{
				int totalLevels = LevelManager.Instance.levelDataArray.Length;
				int maxScore = LevelManager.Instance.levelDataArray[levelIndex % totalLevels].maxScore;
				scoreText.text = maxScore.ToString();
			}
		}
	}
}

