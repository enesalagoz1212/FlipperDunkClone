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
		public static Action OnGameReset;
		public static Action OnGameEnd;
		public static Action<int> OnGameScoreChanged;
		
		[SerializeField] private LevelManager levelManager;
		[SerializeField] private UIManager uiManager;
		[SerializeField] private BallController ballController;
		[SerializeField] private HoopController hoopController;
		
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
			GameInitialize();
		}


		private void GameInitialize()
		{
			levelManager.Initialize(uiManager,hoopController);
			uiManager.Initialize(levelManager, ballController);
			ballController.Initialize(uiManager,hoopController,levelManager);
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
		
		public void EndGame(bool isSuccess)
		{
			ChangeState(GameState.End);
			OnGameEnd?.Invoke();
		}
		
		public void ChangeState(GameState gameState)
		{
			GameState = gameState;

			switch (GameState)
			{
				case GameState.Start:
					OnGameStarted?.Invoke();
					ChangeState(GameState.Playing);
					break;
				
				case GameState.Playing:
					break;
				
				case GameState.Reset:
					OnGameReset?.Invoke();
					break;
				
				case GameState.End:
					// TODO => INVOKE OnGameEnd With Boolean
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
				DOVirtual.DelayedCall(0.2f, () =>
				{
					EndGame(true);
				});
			}
			
			hoopController.SpawnRandomHoop();
		}
	}
}