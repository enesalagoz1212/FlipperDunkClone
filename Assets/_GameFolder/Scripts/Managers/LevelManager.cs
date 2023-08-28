using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FlipperDunkClone.ScriptableObjects;

namespace FlipperDunkClone.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		private UIManager _uiManager;

		public GameObject hoopPrefab;
		public GameObject hoops;
		private GameObject currentHoop;

		public LevelData[] levelDataArray;
		private LevelData _currentLevelData;

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
		}

		public void Initialize(UIManager uiManager)
		{
			_uiManager = uiManager;

		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStarted;
			GameManager.OnGameEnd += OnGameEnd;
			GameManager.OnGameReset += OnGameResetAction;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameEnd -= OnGameEnd;
			GameManager.OnGameReset -= OnGameResetAction;
		}

		private void OnGameStarted()
		{
			LoadCurrentLevel();
			HoopSpawn();
		}
		private void OnGameResetAction()
		{
			GameManager.Instance.ChangeState(GameState.Start);
			GameManager.OnGameStarted?.Invoke();
		}
		private void OnGameEnd()
		{

		}

		public void LoadCurrentLevel()
		{
			if (_currentLevelIndex >= 0 && _currentLevelIndex < levelDataArray.Length)
			{
				_currentLevelData = levelDataArray[_currentLevelIndex];
				GameManager.Instance.currentScore = _currentLevelData.maxScore;
			}
		}

		public void NextLevel()
		{
			_currentLevelIndex++;
			if (_currentLevelIndex < levelDataArray.Length)
			{
				LoadCurrentLevel();
				HoopSpawn();
			}
			GameManager.Instance.ResumeGame();
		}



		private void HoopSpawn()
		{
			RemoveHoop();

			currentHoop = Instantiate(hoopPrefab, _currentLevelData.hoopPosition, Quaternion.identity, hoops.transform);

		}

		private void RemoveHoop()
		{
			if (currentHoop != null)
			{
				Destroy(currentHoop);
			}
		}
	}
}

