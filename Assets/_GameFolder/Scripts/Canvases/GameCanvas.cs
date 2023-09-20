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

		public GameObject basketPrefab;
		public Transform basketSpawnPoint;

		public List<UiBasketController> uiBasketControllers = new List<UiBasketController>();

		private int levelIndex = 0;

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
			if (true)
			{

			}
			int totalLevels = LevelManager.Instance.levelDataArray.Length;
			int maxScore = LevelManager.Instance.levelDataArray[levelIndex % totalLevels].maxScore;

			CreateBasket(maxScore);

			gamePanel.gameObject.SetActive(true);
			UpdateLevelsText();

		}
		private void OnGameStart()
		{


		}

		private void OnGameReset()
		{
			ClearBaskets();
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
		}

		public void CreateBasket(int maxScore)
		{
			float spacing = 0.8f;
			float centerOffset = (maxScore - 1) * spacing / 2;

			for (int i = 0; i < maxScore; i++)
			{
				float xPosition = i * spacing - centerOffset;
				Vector3 spawnPosition = basketSpawnPoint.position + new Vector3(xPosition, 0f, 0f);
				GameObject newBasket = Instantiate(basketPrefab, spawnPosition, Quaternion.identity, basketSpawnPoint);

				UiBasketController basketController = newBasket.GetComponent<UiBasketController>();
				uiBasketControllers.Add(basketController);
			}
		}

		public void ClearBaskets()
		{
			foreach (var basketController in uiBasketControllers)
			{
				Destroy(basketController.gameObject);
			}
			uiBasketControllers.Clear();
		}
	}
}

