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
		public Button vibration;
		public Button sound;

		public Image onImageVibration;
		public Image offImageVibration;
		public Image backgroundImageVibration;
		public Image onImageSound;
		public Image offImageSound;
		public Image backgroundImageSound;

		//private bool _isVibration = true;
		//private bool _isSound = true;
		public void Initialize(GameCanvas gameCanvas)
		{
			_gameCanvas = gameCanvas;
		}
		private void Awake()
		{

		}

		void Start()
		{
			UpdateVisualsVibration();
			UpdateVisualsSound();

			vibration.onClick.AddListener(VibrationButton);
			sound.onClick.AddListener(SoundButton);
			closeButton.onClick.AddListener(OnCloseButton);
			vibration.onClick.AddListener(() => Debug.Log("Vibration calisti"));
			sound.onClick.AddListener(() => Debug.Log("Sound calisti "));
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
			PlayerPrefsManager.IsVibrationOn = !PlayerPrefsManager.IsVibrationOn;
			UpdateVisualsVibration();

		}

		public void SoundButton()
		{

			PlayerPrefsManager.IsSoundOn = !PlayerPrefsManager.IsSoundOn;
			UpdateVisualsSound();
			
		}

		public void UpdateVisualsVibration()
		{

			if (PlayerPrefsManager.IsVibrationOn)
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

		public void UpdateVisualsSound()
		{
			if (PlayerPrefsManager.IsSoundOn)
			{
				onImageSound.gameObject.SetActive(true);
				offImageSound.gameObject.SetActive(false);
				backgroundImageSound.color = Color.green;
		
				SoundManager.Instance.SetMuteState(false);
			}
			else
			{
				onImageSound.gameObject.SetActive(false);
				offImageSound.gameObject.SetActive(true);
				backgroundImageSound.color = Color.red;
			
				SoundManager.Instance.SetMuteState(true);
			}
		}

	}
}

