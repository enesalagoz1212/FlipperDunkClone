using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Managers;
using DG.Tweening;
using TMPro;
using FlipperDunkClone.Controllers;

namespace FlipperDunkClone.Canvases
{
	public class MenuCanvas : MonoBehaviour
	{
		private BallController _ballController;

		public Button storeButton;
		public Button backButton;
		public Button[] ballButtons;

		public TextMeshProUGUI tabToStartText;
		public TextMeshProUGUI tabToShootText;

		public Image gameImageBackground;
		public Image storePanel;

		public Sprite[] ballSprites;

		private bool _isShootText = false;
		public void Initialize(BallController ballController)
		{
			_ballController = ballController;
		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
		}

		private void OnDisable()
		{

			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameEnd -= OnGameEnd;
		}
		void Start()
		{
			for (int i = 0; i < ballButtons.Length; i++)
			{
				int ballIndex = i;
				ballButtons[i].onClick.AddListener(() =>
				{
					OnBallButtonClick(ballIndex);	
				});
			}
		}


		void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					

					break;
				case GameState.Playing:
					int selectedBallIndex = PlayerPrefsManager.SelectedBall;
					_ballController.ChangeBallImage(ballSprites[selectedBallIndex]);

					if (!_isShootText && Input.GetMouseButtonDown(0))
					{
						tabToStartText.DOKill();
						tabToStartText.gameObject.SetActive(false);

						tabToShootText.gameObject.SetActive(true);
						ShootTextTween();
						DOVirtual.DelayedCall(0.25f, () =>
						 {
							 gameImageBackground.gameObject.SetActive(false);
						 });
						_isShootText = true;
					}
					else if (_isShootText && Input.GetMouseButtonDown(0))
					{
						tabToShootText.DOKill();
						tabToShootText.gameObject.SetActive(false);
					}

					break;
				case GameState.Reset:
					break;
				case GameState.End:
					_isShootText = false;
					break;
				case GameState.Menu:
					Debug.Log("GameState menu oldu");
					_isShootText = false;
					tabToStartText.gameObject.SetActive(false);
					tabToShootText.gameObject.SetActive(false);
					storePanel.gameObject.SetActive(true);

					backButton.onClick.AddListener(BackButtonClick);
					break;
				default:
					break;
			}
		}

		private void OnGameStart()
		{
			gameImageBackground.gameObject.SetActive(true);
			storeButton.onClick.AddListener(OnStoreButtonClick);
			tabToStartText.gameObject.SetActive(true);
			StartTextTween();
		}

		private void OnGameReset()
		{

		}

		private void OnGameEnd(bool isSuccuessful)
		{
			tabToStartText.transform.localScale = Vector3.one;
			tabToShootText.transform.localScale = Vector3.one;
		}

		private void OnBallButtonClick(int ballIndex)
		{
			PlayerPrefsManager.SelectedBall = ballIndex ;
			Debug.Log("Selected ball: " + (ballIndex));

			_ballController.ChangeBallImage(ballSprites[ballIndex]);
		}

		public void OnStoreButtonClick()
		{
			GameManager.Instance.ChangeState(GameState.Menu, false);

			Debug.Log("Store Panel acildi");
		}

		public void BackButtonClick()
		{
			tabToStartText.transform.localScale = Vector3.one;
			tabToShootText.transform.localScale = Vector3.one;
			storePanel.gameObject.SetActive(false);
			GameManager.Instance.ChangeState(GameState.Playing, false);
			gameImageBackground.gameObject.SetActive(true);
			tabToStartText.gameObject.SetActive(true);
			StartTextTween();
		}

	
		private void StartTextTween()
		{
			tabToStartText.transform.DOScale(1.4f, 0.7f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
		}

		private void ShootTextTween()
		{
			tabToShootText.transform.DOScale(1.4f, 0.7f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
		}

	}
}

