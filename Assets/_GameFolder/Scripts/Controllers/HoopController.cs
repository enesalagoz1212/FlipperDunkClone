using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using FlipperDunkClone.ScriptableObjects;

namespace FlipperDunkClone.Controllers
{
	public class HoopController : MonoBehaviour
	{
		public GameObject hoop;
		private LevelManager _levelManager;
		private LevelData _currentLevelData;

		private void Awake()
		{
			hoop.SetActive(false);
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

		public void Initialize(LevelManager levelManager)
		{
			_levelManager = levelManager;
		}

		private void OnGameStarted()
		{
			hoop.SetActive(true);
			SpawnRandomHoop();
		}

		private void OnGameEnd()
		{
			hoop.SetActive(false);
		}

		public void SpawnRandomHoop()
		{
			var randomTransform = _levelManager.ReturnRandomHoopSpawnPosition();
			transform.position = randomTransform.position;
		}
	}
}

