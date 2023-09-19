using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using TMPro;
using FlipperDunkClone.ScriptableObjects;
using UnityEngine.UI;
using DG.Tweening;
using FlipperDunkClone.Controllers;

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

		public List<UiBasketController> uiBasketControllers = new List<UiBasketController>();

		private int levelIndex = 0;
		private bool _isShootText = false;
		private int _currentBasketIndex = 0;
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
			UpdateLevelDataMaxScore();
		}

		private void OnGameReset()
		{
			_currentBasketIndex = 0;
			ResetBasketImages();
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

		public void ActivateBasketImage()
		{

			if (_currentBasketIndex < basketImageTrue.Length)
			{
				basketImageTrue[_currentBasketIndex].gameObject.SetActive(true);
				_currentBasketIndex++;
			}
		}

		public void ResetBasketImages()
		{
			foreach (Image image in basketImageTrue)
			{
				image.gameObject.SetActive(false);
			}
		}

		private void CenterImage()
		{

		}
	}
}

