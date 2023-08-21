using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Canvases
{
	public class EndCanvas : MonoBehaviour
	{
		public GameObject endPanel;
		public Button continueButton;

		private void OnEnable()
		{
			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{
			GameManager.OnGameEnd -= OnGameEnd;

		}
		void Start()
		{
			endPanel.SetActive(false);
			continueButton.onClick.AddListener(OnContinueButtonClicked);
		}

		private void OnContinueButtonClicked()
		{
			GameManager.Instance.RestartGame();
			endPanel.SetActive(false);
		}

		private void OnGameEnd()
		{
			endPanel.SetActive(true);
		}
	}
}

