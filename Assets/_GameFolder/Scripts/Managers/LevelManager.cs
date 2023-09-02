using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FlipperDunkClone.ScriptableObjects;
using FlipperDunkClone.Controllers;
using FlipperDunkClone.Canvases;

namespace FlipperDunkClone.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		private UIManager _uiManager;
		private HoopController _hoopController;


		public LevelData[] levelDataArray;
		private LevelData _currentLevelData;

		public Transform[] hoopSpawnPoints;
		private int _lastHoopSpawnIndex;

		private int _currentLevelIndex = 0;
		private int maxScore;

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}

			_lastHoopSpawnIndex = -1;
		}

		public void Initialize(UIManager uiManager, HoopController hoopController)
		{
			_uiManager = uiManager;
			_hoopController = hoopController;
		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStarted;
			GameManager.OnGameEnd += OnGameEnd;
			GameManager.OnGameReset += OnGameReset;
		}
		
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameEnd -= OnGameEnd;
			GameManager.OnGameReset -= OnGameReset;
		}

		private void OnGameStarted()
		{
			LoadCurrentLevel();
		}

		private void OnGameReset()
		{
			LoadCurrentLevel();
		}

		private void OnGameEnd(bool isSuccessful)
		{
			_lastHoopSpawnIndex = -1;
		}

		private void LoadCurrentLevel()
		{
			var currentLevelIndex = PlayerPrefsManager.CurrentLevel % levelDataArray.Length;
			if (currentLevelIndex == 0)
			{
				currentLevelIndex = levelDataArray.Length;
			}
			_currentLevelData = levelDataArray[currentLevelIndex - 1];

			GameManager.Instance.currentScore = _currentLevelData.maxScore;
			UIManager.Instance.GameCanvas.UpdateScoreText(GameManager.Instance.currentScore);
		}

		public void NextLevel()
		{
			LoadCurrentLevel();
		}

		public Transform ReturnRandomHoopSpawnPosition()
		{
			var randomIndex = Random.Range(0, hoopSpawnPoints.Length);
			while (randomIndex == _lastHoopSpawnIndex)
			{
				randomIndex = Random.Range(0, hoopSpawnPoints.Length);
			}

			_lastHoopSpawnIndex = randomIndex;
			return hoopSpawnPoints[_lastHoopSpawnIndex];
		}


	}
}