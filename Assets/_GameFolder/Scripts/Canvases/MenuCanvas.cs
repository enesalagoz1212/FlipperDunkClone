using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Managers;
using DG.Tweening;
using TMPro;
using FlipperDunkClone.Controllers;
using UnityEngine.EventSystems;

namespace FlipperDunkClone.Canvases
{
	public class MenuCanvas : MonoBehaviour
	{
		private GameManager _gameManager;
		private BallController _ballController;

		public Button playButton;
		public Button storeButton;
		public Button backButton;
		public Button[] ballButtons;

		public TextMeshProUGUI tabToStartText;
		public TextMeshProUGUI tabToShootText;

		public Image gameImageBackground;
		public Image storePanel;

		public Sprite[] ballSprites;
		
		private void OnEnable()
		{
			GameManager.OnMenuOpen += OnMenuOpen;
		}

		private void OnDisable()
		{
			GameManager.OnMenuOpen -= OnMenuOpen;
		}

		private void Start()
		{
			for (int i = 0; i < ballButtons.Length; i++)
			{
				int ballIndex = i;
				ballButtons[i].onClick.AddListener(() =>
				{
					OnBallButtonClick(ballIndex);
				});
			}
			int selectedBallIndex = PlayerPrefsManager.SelectedBall;
			_ballController.ChangeBallImage(ballSprites[selectedBallIndex]);
		}
		
		public void Initialize(GameManager gameManager, BallController ballController)
		{
			_gameManager = gameManager;
			_ballController = ballController;
			
			storeButton.onClick.AddListener(OnStoreButtonClick);
			playButton.onClick.AddListener(OnPlayButtonClicked);
			backButton.onClick.AddListener(BackButtonClick);
		}

		private void OnMenuOpen()
		{
			gameImageBackground.gameObject.SetActive(true);
			tabToStartText.gameObject.SetActive(true);
			StartTextTween();
		}
		
		private void OnPlayButtonClicked()
		{
			_gameManager.OnGameStart();
			
			OnGameStart();
		}

		private void OnGameStart()
		{
			tabToStartText.transform.DOKill();
			tabToStartText.transform.localScale = Vector3.one;
			tabToStartText.gameObject.SetActive(false);
			tabToShootText.gameObject.SetActive(true);
			ShootTextTween();
			DOVirtual.DelayedCall(0.25f, () =>
			{
				gameImageBackground.gameObject.SetActive(false);
			});
		}

		public void OnStoreButtonClick()
		{
			tabToStartText.transform.DOKill();
			tabToStartText.transform.localScale = Vector3.one;
			
			tabToStartText.gameObject.SetActive(false);
			tabToShootText.gameObject.SetActive(false);
			storePanel.gameObject.SetActive(true);
		}

		public void BackButtonClick()
		{
			storePanel.gameObject.SetActive(false);
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

		#region SHOP CANVAS

		private void OnBallButtonClick(int ballIndex)
		{
			PlayerPrefsManager.SelectedBall = ballIndex;
			Debug.Log("Selected ball: " + (ballIndex));

			_ballController.ChangeBallImage(ballSprites[ballIndex]);
		}

		#endregion
	}
}