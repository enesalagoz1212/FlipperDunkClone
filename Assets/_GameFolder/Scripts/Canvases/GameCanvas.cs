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
		public TextMeshProUGUI tabToStartText;
		public TextMeshProUGUI tabToShootText;

		public Image gameImageBackground;
		public Image storePanel;

		private int levelIndex = 0;

		private bool _isShootText = false;

		public Button storeButton;

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

					if (!_isShootText && Input.GetMouseButtonDown(0))
					{
						DOVirtual.DelayedCall(0.5f, () =>
						{
							gameImageBackground.gameObject.SetActive(false);
						});
						tabToStartText.transform.DOPause();
						tabToStartText.DOKill();
						tabToShootText.gameObject.SetActive(true);
						ShootTextTween();
						_isShootText = true;
					}
					else if (_isShootText && Input.GetMouseButtonDown(0))
					{
						tabToShootText.transform.DOPause();
						tabToShootText.DOKill();
						tabToShootText.gameObject.SetActive(false);
					}
					break;
				case GameState.Reset:
					break;
				case GameState.End:
					_isShootText = false;

					break;
				default:
					break;
			}
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
			gameImageBackground.gameObject.SetActive(true);
			storeButton.onClick.AddListener(OnStoreButtonClick);
			StartTextTween();
			UpdateLevelsText();
			UpdateLevelDataMaxScore();
		}

		private void OnGameReset()
		{
			UpdateLevelDataMaxScore();
		}

		private void OnGameEnd(bool IsSuccessful)
		{
			if (IsSuccessful)
			{
				levelIndex++;
			}
			tabToStartText.transform.localScale = Vector3.one;
			tabToShootText.transform.localScale = Vector3.one;
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

		private void StartTextTween()
		{
			tabToStartText.transform.DOScale(1.4f, 0.7f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
		}

		private void ShootTextTween()
		{
			tabToShootText.transform.DOScale(1.4f, 0.7f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
		}

		public void OnStoreButtonClick()
		{
			storePanel.gameObject.SetActive(true);
			Debug.Log("Store Panel acildi");
		}
	}
}

