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
		public int diamondScore = 0;

		LevelData LevelData;
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
			GameInitialize();
		}

		private void Update()
		{

		}
		private void GameInitialize()
		{
			levelManager.Initialize(uiManager, hoopController);
			uiManager.Initialize(levelManager, ballController);
			ballController.Initialize(uiManager, hoopController, levelManager);
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
			ChangeState(GameState.End,IsSuccesful);
		}

		public void ChangeState(GameState gameState, bool isSuccessful = false)
		{
			GameState = gameState;

			switch (GameState)
			{
				case GameState.Start:
					OnGameStarted?.Invoke();
					ChangeState(GameState.Playing);
					break;

				case GameState.Playing:
					playerController.SetIsRotating(true);
					break;

				case GameState.Reset:
					OnGameReset?.Invoke();
					break;

				case GameState.End:
					// TODO => INVOKE OnGameEnd With Boolean

					if (isSuccessful)
					{
						IncreaseDiamondScore(2);
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