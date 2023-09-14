using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Canvases
{
	public class SettingsCanvas : MonoBehaviour
	{
		GameCanvas _gameCanvas;

		public GameObject settingPanel;
		public Button settingButton;
		public Button closeButton;

		public Image onImageVibration;
		public Image offImageVibration;
		public Image backgroundImageVibration;
		public Image onImageSound;
		public Image offImageSound;
		public Image backgroundImageSound;

		private bool _isVibration;
		private bool _isSound;
		public void Initialize(GameCanvas gameCanvas)
		{
			_gameCanvas = gameCanvas;
		}
		private void Awake()
		{

		}

		void Start()
		{
			
			Button onButtonVibration = onImageVibration.GetComponent<Button>();
			if (onButtonVibration != null)
			{
				onButtonVibration.onClick.AddListener(VibrationButton);
			}

			Button offButtonVibration = offImageVibration.GetComponent<Button>();
			if (offButtonVibration != null)
			{
				offButtonVibration.onClick.AddListener(VibrationButton);
			}

			Button onButtonSound = onImageSound.GetComponent<Button>();
			if (onButtonSound != null)
			{
				onButtonSound.onClick.AddListener(SoundButton);
			}

			Button offButtonSound = offImageSound.GetComponent<Button>();
			if (offButtonSound != null)
			{
				offButtonSound.onClick.AddListener(SoundButton);
			}

			closeButton.onClick.AddListener(OnCloseButton);
		}


		public void SettingPanel()
		{
			settingPanel.SetActive(true);
		}

		private void OnCloseButton()
		{
			settingPanel.SetActive(false);
			GameManager.OnMenuOpen?.Invoke();
		}

		public void VibrationButton()
		{
			_isVibration = !_isVibration;

			if (_isVibration)
			{
				onImageVibration.gameObject.SetActive(true);
				offImageVibration.gameObject.SetActive(false);
				backgroundImageVibration.color = Color.green;
			}
			else
			{
				onImageVibration.gameObject.SetActive(false);
				offImageVibration.gameObject.SetActive(true);
				backgroundImageVibration.color = Color.red;
			}
		}

		public void SoundButton()
		{
			_isSound = !_isSound;

			if (_isSound)
			{
				onImageSound.gameObject.SetActive(true);
				offImageSound.gameObject.SetActive(false);
				backgroundImageSound.color = Color.green;
			}
			else
			{
				onImageSound.gameObject.SetActive(false);
				offImageSound.gameObject.SetActive(true);
				backgroundImageSound.color = Color.red;
			}
		}
	}
}

