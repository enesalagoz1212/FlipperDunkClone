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

		public void Initialize(GameCanvas gameCanvas)
		{
			_gameCanvas = gameCanvas;
		}
		private void Awake()
		{
			settingButton.onClick.AddListener(OnSettingButton);
		}

		void Start()
		{

		}

		void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					break;
				case GameState.Playing:
					settingButton.gameObject.SetActive(false);
					break;
				case GameState.Reset:
					break;
				case GameState.End:
					break;
				case GameState.Menu:
					break;
				default:
					break;
			}
		}

		public void OnSettingButton()
		{
			settingPanel.SetActive(true);
			_gameCanvas.OnSettingButtonClick();
		}
		public void ChangeSettingButtonInteractable()
		{
			settingButton.interactable = !settingButton.interactable;
			settingPanel.SetActive(!settingButton.interactable);

		}
	}
}

