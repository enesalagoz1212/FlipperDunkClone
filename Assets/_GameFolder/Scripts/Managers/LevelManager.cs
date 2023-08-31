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

		private void OnGameEnd(bool IsSuccessful)
		{
			_lastHoopSpawnIndex = -1;
			if (IsSuccessful)
			{

				UIManager.Instance.EndCanvas.UpdateEndLevelText(_currentLevelIndex + 1);
			}
			else
			{
				UIManager.Instance.ResetCanvas.UpdateResetLevelText(_currentLevelIndex + 1);

			}
		}

		public void LoadCurrentLevel()
		{
			if (_currentLevelIndex >= 0 && _currentLevelIndex <= levelDataArray.Length)
			{
				_currentLevelData = levelDataArray[_currentLevelIndex];
				GameManager.Instance.currentScore = _currentLevelData.maxScore;
				UIManager.Instance.GameCanvas.UpdateLevelsText(_currentLevelIndex + 1);
			}
		}

		public void NextLevel()
		{
			_currentLevelIndex++;
			if (_currentLevelIndex <= levelDataArray.Length)
			{
				int totalLevels = levelDataArray.Length;
				_currentLevelIndex = _currentLevelIndex % totalLevels;
				LoadCurrentLevel();
			}
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