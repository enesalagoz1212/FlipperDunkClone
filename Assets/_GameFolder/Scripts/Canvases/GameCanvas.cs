using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using TMPro;
using FlipperDunkClone.ScriptableObjects;
using UnityEngine.UI;
using DG.Tweening;

namespace FlipperDunkClone.Canvases
{
	public class GameCanvas : MonoBehaviour
	{
		private LevelData _levelData;
		private SettingsCanvas _settingCanvas;
		private LevelManager _levelManager;

		public TextMeshProUGUI scoreText;
		public TextMeshProUGUI levelsText;

		public Button gameButton;
		public Image gamePanel;
		public Image[] basketImages;
		public Image[] basketImageTrue;

		private int levelIndex = 0;
		private bool _isShootText = false;

		private void OnEnable()
		{
			GameManager.OnMenuOpen += OnMenuOpen;
			GameManager.OnGameScoreChanged += OnGameScoreChanged;
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
		}

		private void OnDisable()
		{
			GameManager.OnMenuOpen -= OnMenuOpen;
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

	
		private void OnMenuOpen()
		{
			gamePanel.gameObject.SetActive(true);
			UpdateLevelsText();
			UpdateLevelDataMaxScore();
		}

		private void OnGameStart()
		{
			
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
			gamePanel.gameObject.SetActive(false);
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
			scoreText.text = score.ToString();
		}

		public void UpdateLevelDataMaxScore()
		{
			if (levelIndex >= 0 && levelIndex <= LevelManager.Instance.levelDataArray.Length)
			{
				int totalLevels = LevelManager.Instance.levelDataArray.Length;
				int maxScore = LevelManager.Instance.levelDataArray[levelIndex % totalLevels].maxScore;

				for (int i = 0; i < basketImages.Length; i++)
				{
					if (i < maxScore)
					{						
						basketImages[i].gameObject.SetActive(true);
					}
					else
					{
						basketImages[i].gameObject.SetActive(false);
					}
				}		
			}
		}


		private void CenterImage()
		{
		
		}
	}
}

