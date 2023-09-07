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
		private bool firstClick = true;

		private Tween moveUpDownTween;
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

		private void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					break;
				case GameState.Playing:
					if (Input.GetMouseButtonDown(0))
					{
						if (firstClick)
						{
							firstClick = false;
							hoop.SetActive(true);
							SpawnRandomHoop();
						}
						else
						{
							hoop.SetActive(true);
						}
					}

					break;
				case GameState.Reset:
					break;
				case GameState.End:
					firstClick = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();

			}
		}
		private void OnGameStarted()
		{
			hoop.SetActive(false);
		
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
				Debug.Log("a");
				targetPosition += new Vector3(2.25f, 0f, 0f);
			}
			else
			{
				Debug.Log("b");
				targetPosition -= new Vector3(2.2f, 0f, 0f);
			}

			var moveRightLeftTween = transform.DOMove(targetPosition, 1.0f).SetEase(Ease.Linear);
			moveRightLeftTween.OnComplete(() =>
			{
				if (randomTransform.position.y == 6)
				{
					Debug.Log("1111");
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

