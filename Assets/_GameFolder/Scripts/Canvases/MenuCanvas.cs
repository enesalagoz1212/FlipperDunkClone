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
		private SettingsCanvas _settingCanvas;
		private StoreCanvas _storeCanvas;
		private GameManager _gameManager;
		private BallController _ballController;

		public Button playButton;
		public Button storeButton;
		public Button settingButton;

		public Image menuBackgroundImage;

		public TextMeshProUGUI tabToStartText;



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

		}

		public void Initialize(GameManager gameManager, BallController ballController, StoreCanvas storeCanvas, SettingsCanvas settingsCanvas)
		{
			_gameManager = gameManager;
			_ballController = ballController;
			_storeCanvas = storeCanvas;
			_settingCanvas = settingsCanvas;

			playButton.onClick.AddListener(OnPlayButtonClicked);
			storeButton.onClick.AddListener(OnStoreButtonClick);
			settingButton.onClick.AddListener(OnSettingButtonClick);
		}

		private void OnMenuOpen()
		{
			menuBackgroundImage.gameObject.SetActive(true);
			tabToStartText.gameObject.SetActive(true);
			StartTextTween();
		}

		private void OnPlayButtonClicked()
		{
			_gameManager.OnGameStart();
			OnGameStart();
		}

		public void OnStoreButtonClick()
		{
			tabToStartText.transform.localScale = Vector3.one;
			_storeCanvas.OnStorePanel();
		}


		public void OnSettingButtonClick()
		{
			_settingCanvas.SettingPanel();
			menuBackgroundImage.gameObject.SetActive(false);
		}

		private void OnGameStart()
		{
			tabToStartText.transform.DOKill();
			tabToStartText.transform.localScale = Vector3.one;
			tabToStartText.gameObject.SetActive(false);

			DOVirtual.DelayedCall(0.25f, () =>
			{
				menuBackgroundImage.gameObject.SetActive(false);
			});
		}



		public void StartTextTween()
		{
			tabToStartText.transform.DOScale(1.4f, 0.7f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
		}



	}
}