using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

