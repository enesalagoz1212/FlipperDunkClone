using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FlipperDunkClone.ScriptableObjects;
using FlipperDunkClone.Controllers;

namespace FlipperDunkClone.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		private UIManager _uiManager;
		private HoopController _hoopController;

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

		public void Initialize(UIManager uiManager,HoopController hoopController)
		{
			_uiManager = uiManager;
			_hoopController = hoopController;
		}

		private void Start()
		{
			HoopSpawn();
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

			}

		}



		private void HoopSpawn()
		{
			RemoveHoop();

			var currentHoop = Instantiate(hoopPrefab, _currentLevelData.hoopPosition, Quaternion.identity, hoops.transform);

		}

		public void RemoveHoop()
		{
			if (currentHoop != null)
			{
				Destroy(currentHoop);
			}
		}
	}
}

