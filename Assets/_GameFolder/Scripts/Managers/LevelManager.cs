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

		public SpriteRenderer backgroundSpriteRenderer;

		private bool _levelDataLoad = false;
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

		public void LoadCurrentLevel()
		{
			var currentLevelIndex = PlayerPrefsManager.CurrentLevel % levelDataArray.Length;
			if (currentLevelIndex == 0)
			{
				currentLevelIndex = levelDataArray.Length;
			}
			_currentLevelData = levelDataArray[currentLevelIndex - 1];

			GameManager.Instance.currentScore = _currentLevelData.maxScore;

			Debug.Log("currentLevelData ka� defa �al��t�");
		}
		public void Initialize()
		{
			LoadCurrentLevel();
		}

		private void OnEnable()
		{
			GameManager.OnGameEnd += OnGameEnd;
			ShopManager.OnBackgroundSelected += OnBackgroundSelected;
		}

		private void OnDisable()
		{
			GameManager.OnGameEnd -= OnGameEnd;
			ShopManager.OnBackgroundSelected -= OnBackgroundSelected;
		}


		public LevelData GetLevelData()
		{
			return _currentLevelData;
		}

		private void OnGameEnd(bool isSuccessful)
		{
			_lastHoopSpawnIndex = -1;
		}

		
		public void NextLevel()
		{
			LoadCurrentLevel();
		}

		private void OnBackgroundSelected(Sprite backgroundSprite)
		{
			ChangeBackgroundImage(backgroundSprite);
		}

		public void ChangeBackgroundImage(Sprite newSprite)
		{
			backgroundSpriteRenderer.sprite = newSprite;
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

			if (randomIndex == 2 || randomIndex == 3 || randomIndex == 4)
			{
				hoopSpawnTransform.localScale = new Vector3(-1, 1, 1);
			}
			else
			{
				hoopSpawnTransform.localScale = new Vector3(1, 1, 1);
			}
			return hoopSpawnTransform;
		}
	}
}