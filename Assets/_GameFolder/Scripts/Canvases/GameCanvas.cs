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

		public TextMeshProUGUI levelsText;

		public Button gameButton;
		public Image gamePanel;

		public GameObject basketPrefab;
		public Transform basketSpawnPoint;

		public List<UiBasketController> uiBasketControllers = new List<UiBasketController>();

		private int levelIndex = 0;
		private int currentBasketIndex = 0;

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
			_levelData = _levelManager.LoadCurrentLevel();
			int maxScore = _levelData.maxScore;
			CreateBasket(maxScore);

			SortBaskets();
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
			ThrowBasket();
		}

		public void UpdateLevelsText()
		{
			levelsText.text = "LEVEL " + PlayerPrefsManager.CurrentLevel;
		}

		public void UpdateLevelDataMaxScore()
		{
			int totalLevels = LevelManager.Instance.levelDataArray.Length;
			int maxScore = LevelManager.Instance.levelDataArray[levelIndex % totalLevels].maxScore;
		}

		public void CreateBasket(int maxScore)
		{
			for (int i = 0; i < maxScore; i++)
			{
				Vector3 spawnPosition = basketSpawnPoint.position;
				GameObject newBasket = Instantiate(basketPrefab, spawnPosition, Quaternion.identity, basketSpawnPoint);

				UiBasketController basketController = newBasket.GetComponent<UiBasketController>();
				uiBasketControllers.Add(basketController);
			}
		}

		public void SortBaskets()
		{
			int basketCount = uiBasketControllers.Count;

			float spacing = 100.0f;
			int centerIndex = basketCount / 2;
			float xOffset = 0f;

			if (basketCount % 2 != 1)
			{
				xOffset = spacing / 2f;
			}

			for (int i = 0; i < basketCount; i++)
			{
				float xPosition = (i - centerIndex) * spacing + xOffset;
				uiBasketControllers[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(xPosition, 0f, 0f);
			}
		}

		public void ClearBaskets()
		{
			foreach (var basketController in uiBasketControllers)
			{
				Destroy(basketController.gameObject);
			}
			uiBasketControllers.Clear();
			currentBasketIndex = 0;
		}

		public void ThrowBasket()
		{
			if (currentBasketIndex < uiBasketControllers.Count)
			{
				UiBasketController currentBasket = uiBasketControllers[currentBasketIndex];
				currentBasket.OnBasket();
				currentBasketIndex++;
			}
		}
	}
}

