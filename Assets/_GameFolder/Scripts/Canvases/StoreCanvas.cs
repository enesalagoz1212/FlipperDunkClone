using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Canvases
{
	public class StoreCanvas : MonoBehaviour
	{
		private LevelManager _levelManager;
		private ShopManager _shopManager;
		private MenuCanvas _menuCanvas;

		public Button backButton;
		public Button[] ballButtons;
		public Button[] flipperButtons;
		public Button[] backgroundButtons;

		public Button ballButton;
		public Button flipperButton;
		public Button backgroundButton;

		public Image ballPanel;
		public Image flipperPanel;
		public Image backgroundPamel;

		public Image storePanel;

		public void Initialize(ShopManager shopManager, MenuCanvas menuCanvas)
		{
			_shopManager = shopManager;
			_menuCanvas = menuCanvas;
	
			backButton.onClick.AddListener(BackButtonClick);
			ballButton.onClick.AddListener(OnBallButton);
			flipperButton.onClick.AddListener(OnFlipperButton);
			backgroundButton.onClick.AddListener(OnBackgroundButton);
		}


		private void Start()
		{

			for (int i = 0; i < ballButtons.Length; i++)
			{
				int ballIndex = i;
				ballButtons[i].onClick.AddListener(() =>
				{
					_shopManager.OnBallButtonClick(ballIndex);
				});
			}


			for (int i = 0; i < flipperButtons.Length; i++)
			{
				int flipperIndex = i;
				flipperButtons[i].onClick.AddListener(() =>
				{
					_shopManager.OnFlipperButtonClick(flipperIndex);
				});
			}


			for (int i = 0; i < backgroundButtons.Length; i++)
			{
				int backgroundIndex = i;
				backgroundButtons[i].onClick.AddListener(() =>
				{
					_shopManager.OnBackgroundButtonClick(backgroundIndex);
				});
			}
		}


		public void OnStorePanel()
		{
			storePanel.gameObject.SetActive(true);
		}

		public void BackButtonClick()
		{
			storePanel.gameObject.SetActive(false);
			GameManager.Instance.ChangeState(GameState.Menu);
		}

		public void OnBallButton()
		{
			ballPanel.gameObject.SetActive(true);
			flipperPanel.gameObject.SetActive(false);
			backgroundPamel.gameObject.SetActive(false);
		}

		public void OnFlipperButton()
		{
			flipperPanel.gameObject.SetActive(true);
			ballPanel.gameObject.SetActive(false);
			backgroundPamel.gameObject.SetActive(false);
		}

		public void OnBackgroundButton()
		{
			backgroundPamel.gameObject.SetActive(true);
			ballPanel.gameObject.SetActive(false);
			flipperPanel.gameObject.SetActive(false);
		}
	}

}
