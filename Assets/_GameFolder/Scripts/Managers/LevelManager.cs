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
		public LevelData[] levels;
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
		}
		private void OnGameEnd()
		{
			RestartLevel();
		}

		public void LoadLevel(int levelIndex)
		{
			LevelData selectedLEvel = levels[levelIndex];
		}

		private void RestartLevel()
		{
			GameManager.Instance.ChangeState(GameState.Start);
			GameManager.OnGameStarted?.Invoke();
		}

		private void HoopSpawn()
		{
			var hoop = Instantiate(hoopPrefab, new Vector3(-3.5f, 2f, 0f), Quaternion.identity, hoops.transform);

		}

	}
}

