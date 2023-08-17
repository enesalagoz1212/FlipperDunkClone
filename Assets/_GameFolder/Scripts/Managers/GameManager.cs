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
		}

		public void ChangeState(GameState gameState)
		{
			GameState = gameState;
		}
	}
}

