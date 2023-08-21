using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FlipperDunkClone.ScriptableObjects;

namespace FlipperDunkClone.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public GameObject hoopPrefab;
		public GameObject hoops;
		public static LevelManager Instance { get; private set; }
		public LevelData LevelData => levelData;

		public LevelData[] levels;
		private LevelData levelData;

		private int _currentLevelIndex = 0;
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
		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStarted;
			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameEnd -= OnGameEnd;
		}

		private void OnGameStarted()
		{
			HoopSpawn();
			LoadCurrentLevel();
		}
		private void OnGameEnd()
		{
			RestartLevel();
		}

		private void LoadCurrentLevel()
		{
			if (_currentLevelIndex >= 0 && _currentLevelIndex < levels.Length)
			{
				levelData = levels[_currentLevelIndex];
			}
		}

		private void NextLevel()
		{
			_currentLevelIndex++;
			if (_currentLevelIndex < levels.Length)
			{
				LoadCurrentLevel();
			}
		}

		public void LevelCompleted()
		{
			if (GameManager.Instance.score == levelData.maxScore)
			{
				NextLevel();
			}
		}

		private void RestartLevel()
		{
			GameManager.Instance.ChangeState(GameState.Start);
			GameManager.OnGameStarted?.Invoke();
		}

		private void HoopSpawn()
		{
			var hoop = Instantiate(hoopPrefab, new Vector3(-3.5f, 1f, 0f), Quaternion.identity, hoops.transform);

		}

	}
}

