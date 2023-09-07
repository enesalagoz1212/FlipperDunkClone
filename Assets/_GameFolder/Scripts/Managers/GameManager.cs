using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FlipperDunkClone.ScriptableObjects;
using DG.Tweening;
using FlipperDunkClone.Controllers;
using FlipperDunkClone.Canvases;

namespace FlipperDunkClone.Managers
{
	public enum GameState
	{
		Start = 0,
		Playing = 1,
		Reset = 2,
		End = 3,
	}
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }
		public GameState GameState { get; set; }

		public static Action OnGameStarted;
		public static Action OnGamePlaying;
		public static Action OnGameReset;
		public static Action<bool> OnGameEnd;
		public static Action<int> OnGameScoreChanged;
		public static Action<int> OnDiamondScored;

		[SerializeField] private LevelManager levelManager;
		[SerializeField] private UIManager uiManager;
		[SerializeField] private BallController ballController;
		[SerializeField] private HoopController hoopController;
		[SerializeField] private PlayerController playerController;

		public int currentScore;

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

		void Start()
		{
			Application.targetFrameRate = 60;
			GameInitialize();
		}

		private void Update()
		{
			if (GameState == GameState.Playing)
			{
				if (Input.GetKeyDown(KeyCode.A))
				{
					OnBasketThrown();
				}
				else if (Input.GetKeyDown(KeyCode.S))
				{
					EndGame(false);
				}
			}
		}

		private void GameInitialize()
		{
			levelManager.Initialize();
			uiManager.Initialize(levelManager);
			ballController.Initialize();
			hoopController.Initialize(levelManager);

			OnGameStart();
		}

		private void OnGameStart()
		{
			ChangeState(GameState.Start);
		}

		public void ResetGame()
		{
			ChangeState(GameState.Reset);
		}

		public void EndGame(bool IsSuccesful)
		{
			ChangeState(GameState.End, IsSuccesful);
		}

		public void ChangeState(GameState gameState, bool isSuccessful = false)
		{
			GameState = gameState;

			Debug.Log($"Game State: {gameState}");

			switch (GameState)
			{
				case GameState.Start:
					OnGameStarted?.Invoke();

					ChangeState(GameState.Playing);

					break;

				case GameState.Playing:
					//playerController.SetIsRotating(true);
					break;

				case GameState.Reset:
					OnGameReset?.Invoke();

					DOVirtual.DelayedCall(0.25f, () =>
					{
						ChangeState(GameState.Start);
					});
					break;

				case GameState.End:
					if (isSuccessful)
					{
						IncreaseDiamondScore(2);
						PlayerPrefsManager.CurrentLevel++;
					}

					OnGameEnd?.Invoke(isSuccessful);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void OnBasketThrown()
		{
			currentScore--;
			OnGameScoreChanged?.Invoke(currentScore);

			if (currentScore == 0)
			{
				DOVirtual.DelayedCall(0.1f, () =>
				{
					EndGame(true);

				});
			}

			hoopController.SpawnRandomHoop();
		}

		public void IncreaseDiamondScore(int score)
		{
			PlayerPrefsManager.DiamondScore += score;
			OnDiamondScored?.Invoke(PlayerPrefsManager.DiamondScore);
		}
	}
}