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
		public static Action OnGameEnd;
		public static Action OnGameScoredecreased;


		[SerializeField] private LevelManager levelManager;
		[SerializeField] private UIManager uiManager;
		[SerializeField] private BallController ballController;




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


		void Update()
		{

		}

		private void GameInitialize()
		{
			levelManager.Initialize(uiManager);
			uiManager.Initialize(levelManager, ballController);
			ballController.Initialize(uiManager);
			OnGameStart();
		}

		private void OnGameStart()
		{

			GameState = GameState.Start;
			OnGameStarted?.Invoke();
			GameState = GameState.Playing;	 
		}

		public void ResetGame()
		{
			ChangeState(GameState.Reset);
			OnGameReset?.Invoke();
		}
		public void EndGame()
		{
			GameState = GameState.End;
			OnGameEnd?.Invoke();

		}
		public void ChangeState(GameState gameState)
		{
			GameState = gameState;
		}


		public void DecreaseScore()
		{
			currentScore--;
			OnGameScoredecreased?.Invoke();

			if (currentScore == 0)
			{
				DOVirtual.DelayedCall(0.2f, () =>
				{
					GameState = GameState.End;
					OnGameEnd?.Invoke();
				
				});
			}
		}


	}
}

