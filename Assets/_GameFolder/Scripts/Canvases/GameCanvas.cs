using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using TMPro;
using FlipperDunkClone.ScriptableObjects;
using UnityEngine.UI;
using DG.Tweening;

namespace FlipperDunkClone.Canvases
{
	public class GameCanvas : MonoBehaviour
	{
		private LevelData _levelData;
		private SettingsCanvas _settingCanvas;
		private LevelManager _levelManager;

		public TextMeshProUGUI scoreText;
		public TextMeshProUGUI levelsText;
		public TextMeshProUGUI tabToShootText;

		public Button gameButton;
		public Image gamePanel;


		private int levelIndex = 0;
		private bool _isShootText = false;

		private void OnEnable()
		{
			GameManager.OnGameScoreChanged += OnGameScoreChanged;
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
		}

		private void OnDisable()
		{
			GameManager.OnGameScoreChanged -= OnGameScoreChanged;
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameEnd -= OnGameEnd;
		}

		public void Initialize(LevelManager levelManager, SettingsCanvas settingsCanvas)
		{
			_settingCanvas = settingsCanvas;
			_levelManager = levelManager;
			gameButton.onClick.AddListener(OnGameButton);
		}

		private void Start()
		{

		}
		private void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					break;
				case GameState.Playing:
					scoreText.gameObject.SetActive(true);
					levelsText.gameObject.SetActive(true);
					break;
				case GameState.Reset:
					break;
				case GameState.End:
					break;
				case GameState.Menu:
					scoreText.gameObject.SetActive(false);
					levelsText.gameObject.SetActive(false);
					break;
				default:
					break;
			}

		}

		private void OnGameButton()
		{
			tabToShootText.transform.DOKill();
			tabToShootText.gameObject.SetActive(false);
		}

		public void OnSettingButtonClick()
		{
			if (_settingCanvas != null)
			{
				_settingCanvas.ChangeSettingButtonInteractable();
			}
		}

		private void OnGameStart()
		{
			tabToShootText.gameObject.SetActive(true);
			gamePanel.gameObject.SetActive(true);
			ShootTextTween();
			UpdateLevelsText();
			UpdateLevelDataMaxScore();
		}

		private void OnGameReset()
		{
			UpdateLevelDataMaxScore();
			tabToShootText.transform.localScale = Vector3.one;
		}

		private void OnGameEnd(bool IsSuccessful)
		{
			if (IsSuccessful)
			{
				levelIndex++;
			}
			gamePanel.gameObject.SetActive(false);
		}


		private void OnGameScoreChanged(int score)
		{
			UpdateScoreText(score);
		}

		public void UpdateLevelsText()
		{
			levelsText.text = "LEVEL " + PlayerPrefsManager.CurrentLevel;
		}

		public void UpdateScoreText(int score)
		{
			//int maxScore = LevelManager.Instance.levelDataArray[levelIndex].maxScore;
			scoreText.text = score.ToString();
		}

		public void UpdateLevelDataMaxScore()
		{
			if (levelIndex >= 0 && levelIndex <= LevelManager.Instance.levelDataArray.Length)
			{
				int totalLevels = LevelManager.Instance.levelDataArray.Length;
				int maxScore = LevelManager.Instance.levelDataArray[levelIndex % totalLevels].maxScore;
				scoreText.text = maxScore.ToString();
			}
		}

		private void ShootTextTween()
		{
			tabToShootText.transform.DOScale(1.4f, 0.7f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
		}
	}
}

