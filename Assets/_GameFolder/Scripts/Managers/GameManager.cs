using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

		private bool _isGamePaused = false;

		public int score = 0;
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
			OnGameStart();
		}

		private void OnGameStart()
		{
			GameState = GameState.Start;
			OnGameStarted?.Invoke();
			GameState = GameState.Playing;
			score = 0;
		}

		public void EndGame()
		{
			score = 0;
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
			score++;
			OnGameScoreIncreased?.Invoke();

		}

		public void RestartGame()
		{
			GameState = GameState.Start;

			score = 0;
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

