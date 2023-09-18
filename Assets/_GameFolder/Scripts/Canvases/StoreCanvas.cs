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

		private Color ballButtonColor = new Color(1f, 0.305f, 0f);
		private Color flipperButtonColor = new Color(0.713f, 0.298f, 0.843f);
		private Color backgroundButtonColor = new Color(0.172f, 0.639f, 0.921f);

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
		public Image colorButtonsImage;

		public void Initialize(ShopManager shopManager, MenuCanvas menuCanvas)
		{
			_shopManager = shopManager;
			_menuCanvas = menuCanvas;

			backButton.onClick.AddListener(BackButtonClick);
			ballButton.onClick.AddListener(OnBallButton);
			flipperButton.onClick.AddListener(OnFlipperButton);
			backgroundButton.onClick.AddListener(OnBackgroundButton);
		}

		private void OnEnable()
		{
			GameManager.OnMenuOpen += OnMenuOpen;

			SelectedObject(PlayerPrefsManager.SelectedBall, ballButtons);
			SelectedObject(PlayerPrefsManager.SelectedFlipper, flipperButtons);
			SelectedObject(PlayerPrefsManager.SelectedBackground, backgroundButtons);
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
					_shopManager.OnBallButtonClick(ballIndex);
					ChangeButtonColor(ballButtons[ballIndex], Color.green);
					ResetButtonColors(ballButtons, ballIndex);
				});
			}


			for (int i = 0; i < flipperButtons.Length; i++)
			{
				int flipperIndex = i;
				flipperButtons[i].onClick.AddListener(() =>
				{
					_shopManager.OnFlipperButtonClick(flipperIndex);
					ChangeButtonColor(flipperButtons[flipperIndex], Color.green);
					ResetButtonColors(flipperButtons, flipperIndex);
				});
			}


			for (int i = 0; i < backgroundButtons.Length; i++)
			{
				int backgroundIndex = i;
				backgroundButtons[i].onClick.AddListener(() =>
				{
					_shopManager.OnBackgroundButtonClick(backgroundIndex);
					ChangeButtonColor(backgroundButtons[backgroundIndex], Color.green);
					ResetButtonColors(backgroundButtons,backgroundIndex);
				});
			}
		}

		private void OnMenuOpen()
		{
			OnBallButton();
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
			
			ChangeColorImage(ballButtonColor);

			ballButton.transform.DOScale(new Vector3(1f, 1.12f, 1f), 0.1f);
			flipperButton.transform.DOScale(Vector3.one, 0.1f);
			backgroundButton.transform.DOScale(Vector3.one, 0.1f);
		}

		public void OnFlipperButton()
		{
			flipperPanel.gameObject.SetActive(true);
			ballPanel.gameObject.SetActive(false);
			backgroundPamel.gameObject.SetActive(false);

			ChangeColorImage(flipperButtonColor);

			flipperButton.transform.DOScale(new Vector3(1f, 1.12f, 1f), 0.1f);
			ballButton.transform.DOScale(Vector3.one, 0.1f);
			backgroundButton.transform.DOScale(Vector3.one, 0.1f);
		}

		public void OnBackgroundButton()
		{
			backgroundPamel.gameObject.SetActive(true);
			ballPanel.gameObject.SetActive(false);
			flipperPanel.gameObject.SetActive(false);

			ChangeColorImage(backgroundButtonColor);

			backgroundButton.transform.DOScale(new Vector3(1f, 1.12f, 1f), 0.1f);
			ballButton.transform.DOScale(Vector3.one, 0.1f);
			flipperButton.transform.DOScale(Vector3.one, 0.1f);
		}


		public void ChangeColorImage(Color newColor)
		{
			colorButtonsImage.color = newColor;
		}

		private void ChangeButtonColor(Button button,Color newColor)
		{
			Image buttonImage = button.GetComponent<Image>();
			if (buttonImage!=null)
			{
				buttonImage.color = newColor;
			}
		}

		private void ResetButtonColors(Button[] buttons,int selectedIndex)
		{
			for (int i = 0; i < buttons.Length; i++)
			{
				if (i != selectedIndex)
				{
					ChangeButtonColor(buttons[i], Color.white);
				}
			}
		}

		private void SelectedObject(int selectedIndex, Button[] button)
		{
			if (selectedIndex >= 0 && selectedIndex < button.Length)
			{
				ChangeButtonColor(button[selectedIndex], Color.green);
			}
		}
	}

}
