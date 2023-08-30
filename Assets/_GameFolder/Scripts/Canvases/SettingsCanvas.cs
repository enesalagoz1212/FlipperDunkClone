using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlipperDunkClone.Canvases
{
	public class SettingsCanvas : MonoBehaviour
	{

		public GameObject settingPanel;
		public Button settingButton;
		void Start()
		{

		}

		void Update()
		{

		}

		public void ChangeSettingButtonInteractable()
		{

			settingPanel.SetActive(true);
		}
	}
}

