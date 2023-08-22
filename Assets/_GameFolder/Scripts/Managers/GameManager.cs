using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FlipperDunkClone.ScriptableObjects;

namespace FlipperDunkClone.Managers
{
	public enum GameState
	{
		Start = 0,
		Playing = 1,
		Pause = 2,
		End = 3,
	}
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }
		public GameState GameState { get; set; }

		public static Action OnGameStarted;
		public static Action OnGamePlaying;
		public static Action OnGamePaused;
		public static Action OnGameEnd;
		public static Action OnGameScoreIncreased;


		[SerializeField] private LevelManager levelManager;
		[SerializeField] private LevelDataManager levelDataManager;


		private bool _isGamePaused = false;


		public int CurrentScore => currentScore;
		public int currentScore = 0;

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
			levelManager.Initialize(levelDataManager);

			OnGameStart();
		}

		private void OnGameStart()
		{
			currentScore = 0;
			GameState = GameState.Start;

			

			OnGameStarted?.Invoke();
			GameState = GameState.Playing;
			ResumeGame();
		}

		public void EndGame()
		{
			GameState = GameState.End;
			OnGameEnd?.Invoke();

			PauseGame();
		}
		public void ChangeState(GameState gameState)
		{
			GameState = gameState;
		}

		public void IncreaseScore()
		{
			currentScore = currentScore + 1;

			//Debug.Log("Current Score: " + currentScore);

			OnGameScoreIncreased?.Invoke();

		}

		public void RestartGame()
		{

			GameState = GameState.Start;			
			LevelManager.Instance.LoadCurrentLevel();
			OnGameStarted?.Invoke();

			ResumeGame();
		}

		private void PauseGame()
		{
			_isGamePaused = true;
			Time.timeScale = 0f;
		}

		private void ResumeGame()
		{
			_isGamePaused = false;
			Time.timeScale = 1f;
		}

	}
}

