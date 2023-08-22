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

		public GameObject hoopPrefab;
		public GameObject hoops;

		private LevelDataManager _levelDataManager;
		private LevelData _currentLevelData;

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

		public void Initialize(LevelDataManager levelDataManager)
		{
			_levelDataManager = levelDataManager;
		
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
			LoadCurrentLevel();
			HoopSpawn();
		}
		private void OnGameEnd()
		{
			
		}

		public void LoadCurrentLevel()
		{
			if (_currentLevelIndex>=0 && _currentLevelIndex<_levelDataManager.levelDataArray.Length)
			{
				_currentLevelData = _levelDataManager.levelDataArray[_currentLevelIndex];
				LevelCompleted();
			}
		}

		private void NextLevel()
		{
			_currentLevelIndex++;
			if (_currentLevelIndex<_levelDataManager.levelDataArray.Length)
			{
				LoadCurrentLevel();
			}
		}

		public void LevelCompleted()
		{
			Debug.Log("10");
			Debug.Log("Current Score: " + GameManager.Instance.CurrentScore);
			Debug.Log("Max Score for Current Level: " + _currentLevelData.maxScore);
			if (GameManager.Instance.CurrentScore == _currentLevelData.maxScore)
			{
				Debug.Log("12");
				NextLevel();
				Debug.Log("15");
			}
		}

	

		private void HoopSpawn()
		{
			var hoop = Instantiate(hoopPrefab,_currentLevelData.hoopPosition, Quaternion.identity, hoops.transform);

		}

	}
}

