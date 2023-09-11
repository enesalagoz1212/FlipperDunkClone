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
		public Image image;
		public Image backgroundPamel;

		public Image storePanel;

		public void Initialize(ShopManager shopManager, MenuCanvas menuCanvas )
		{
			_shopManager = shopManager;
			_menuCanvas = menuCanvas;
			
			backButton.onClick.AddListener(BackButtonClick);
			ballButton.onClick.AddListener(OnBallButton);
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
				ballButtons[i].onClick.AddListener(() =>
				{
					_shopManager.OnFlipperButtonClick(flipperIndex);
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
		}
	}

}
