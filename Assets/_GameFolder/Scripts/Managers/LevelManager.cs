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
		private GameObject currentHoop;

		public LevelData[] levelDataArray;
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

		public void Initialize()
		{


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
			
		}
		private void OnGameEnd()
		{

		}

		public void LoadCurrentLevel()
		{
			if (_currentLevelIndex >= 0 && _currentLevelIndex < levelDataArray.Length)
			{
				_currentLevelData = levelDataArray[_currentLevelIndex];
			}
		}

		private void NextLevel()
		{
			_currentLevelIndex++;
			if (_currentLevelIndex < levelDataArray.Length)
			{
				LoadCurrentLevel();
			}
		}

		public void LevelCompleted()
		{
			Debug.Log("10");
			Debug.Log("Current Score: " + GameManager.Instance.currentScore);
			Debug.Log("Max Score for Current Level: " + _currentLevelData.maxScore);
			if (GameManager.Instance.currentScore == _currentLevelData.maxScore)
			{
				Debug.Log("12");
				NextLevel();
				Debug.Log("15");
			}
		}



		private void HoopSpawn()
		{
			RemoveHoop();

			currentHoop = Instantiate(hoopPrefab, _currentLevelData.hoopPosition, Quaternion.identity, hoops.transform);

		}

		private void RemoveHoop()
		{
			if (currentHoop!=null)
			{
				Destroy(currentHoop);
			}
		}
	}
}

