using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FlipperDunkClone.ScriptableObjects;
using DG.Tweening;

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
		public static Action OnGameScoreIncreased;


		[SerializeField] private LevelManager levelManager;
		[SerializeField] private UIManager uiManager;




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
			levelManager.Initialize();

			OnGameStart();
		}

		private void OnGameStart()
		{
			
			GameState = GameState.Start;		
			OnGameStarted?.Invoke();
			GameState = GameState.Playing;
	
			currentScore = 0;
		}

		public void ResetGame()
		{
			ChangeState(GameState.Reset);
			OnGameReset?.Invoke();
			DOVirtual.DelayedCall(0.5f, () =>
			{
				OnGameStart();
			});

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

		//public void IncreaseScore()
		//{
		//	currentScore++;

		//	//Debug.Log("Current Score: " + currentScore);

		//	OnGameScoreIncreased?.Invoke();

		//}
		
		public void DecreaseScore()
		{
			currentScore--;

			OnGameScoreIncreased?.Invoke();
			if (currentScore==0)
			{
				LevelManager.Instance.LoadCurrentLevel();

			}
		}

		public void RestartGame()
		{

			
			GameState = GameState.Start;
			LevelManager.Instance.LoadCurrentLevel();
			OnGameStarted?.Invoke();

		
		}


	}
}

