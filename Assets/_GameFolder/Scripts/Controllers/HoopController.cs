using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using FlipperDunkClone.ScriptableObjects;
using DG.Tweening;

namespace FlipperDunkClone.Controllers
{
	public class HoopController : MonoBehaviour
	{
		public GameObject hoop;
		private LevelManager _levelManager;
		private LevelData _currentLevelData;

		private bool _allowHoopSpawn = true;
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
			_allowHoopSpawn = true;
		}

		private void OnGameEnd(bool IsSuccessful)
		{
			hoop.SetActive(false);
			_allowHoopSpawn = false;
		}

		public void SpawnRandomHoop()
		{
			if (!_allowHoopSpawn)
			{
				return;
			}
			var randomTransform = _levelManager.ReturnRandomHoopSpawnPosition();
			transform.position = randomTransform.position;
			transform.localScale = randomTransform.localScale;

			Vector3 targetPosition = transform.position;

			if (randomTransform.localScale.x > 0)
			{
				targetPosition += new Vector3(2.25f, 0f, 0f);
			}
			else
			{
				targetPosition -= new Vector3(2.2f, 0f, 0f);
			}

			transform.DOMove(targetPosition, 1.0f).SetEase(Ease.Linear);

		}
	}
}

