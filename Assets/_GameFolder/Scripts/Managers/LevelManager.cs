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

		public LevelData[] levelDataArray;
		private LevelData _currentLevelData;

		public Transform[] hoopSpawnPoints;
		private int _lastHoopSpawnIndex;

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

		public void Initialize()
		{

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

			Transform hoopSpawnTransform = hoopSpawnPoints[_lastHoopSpawnIndex];


			if (randomIndex == 2 || randomIndex == 3)
			{
			
				Debug.Log("2 yada 3");
				hoopSpawnTransform.localScale = new Vector3(-1, 1, 1);

			}
			else
			{
			
				Debug.Log("0 yada 1");
				hoopSpawnTransform.localScale = new Vector3(1, 1, 1);

			}

	
			return hoopSpawnTransform;
		}


	}
}