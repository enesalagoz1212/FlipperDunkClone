using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using TMPro;
using FlipperDunkClone.ScriptableObjects;

namespace FlipperDunkClone.Canvases
{
    public class GameCanvas : MonoBehaviour
    {
		public TextMeshProUGUI scoreText;

		LevelData levelData;

		private SettingsCanvas _settingCanvas;
		private LevelManager _levelManager;
		private void OnEnable()
		{
			GameManager.OnGameScoreIncreased += OnGameScoreIncreased;
			GameManager.OnGameStarted += OnGameStart;
		}
		private void OnDisable()
		{
			GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
			GameManager.OnGameStarted -= OnGameStart;
		}

		public void Initialize(LevelManager levelManager,SettingsCanvas settingsCanvas)
		{
			_settingCanvas = settingsCanvas;
			_levelManager = levelManager;
		
		}
		void Start()
        {

        }

		private void OnGameStart()
		{
			UpdateScoreText();
		}

		private void OnGameScoreIncreased()
		{
			UpdateScoreText();
		}

		public void UpdateScoreText()
		{
			scoreText.text = " " + GameManager.Instance.currentScore.ToString();
		}

	}
}

