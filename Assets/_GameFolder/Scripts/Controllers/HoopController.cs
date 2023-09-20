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
		public GameObject hoopTagCollider;
		private LevelManager _levelManager;
		private LevelData _currentLevelData;

		private bool _allowHoopSpawn = true;

		private Tween moveUpDownTween;

		private void Awake()
		{
			hoop.SetActive(false);
		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStarted;
			GameManager.OnGameEnd += OnGameEnd;
			GameManager.OnGameScoreChanged += OnGameScoreChanged;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStarted;
			GameManager.OnGameEnd -= OnGameEnd;
			GameManager.OnGameScoreChanged -= OnGameScoreChanged;
		}

		public void Initialize(LevelManager levelManager)
		{
			_levelManager = levelManager;

			hoop.SetActive(false);
		}

		private void OnGameStarted()
		{
			hoop.SetActive(true);
			_allowHoopSpawn = true;
			SpawnRandomHoop();
		}

		private void OnGameEnd(bool isSuccessful)
		{
			hoop.SetActive(false);
			_allowHoopSpawn = false;
		}

		private void OnGameScoreChanged(int score)
		{
			hoopTagCollider.gameObject.tag = "Untagged";
		}


		public void SpawnRandomHoop()
		{
			hoopTagCollider.gameObject.tag = "Hoop";
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
			
			var moveRightLeftTween = transform.DOMove(targetPosition, 1.0f).SetEase(Ease.Linear);
			moveRightLeftTween.OnComplete(() =>
			{
				if (randomTransform.position.y == 6)
				{
					moveUpDownTween = transform.DOMoveY(transform.position.y - 6.0f, 2.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
				}
				else
				{
					if (moveUpDownTween != null)
					{
						moveUpDownTween.Kill();
					}
				}
			});
		}
	}
}