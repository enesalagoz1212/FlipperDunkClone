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
		public Button restartButton;

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
			restartButton.onClick.AddListener(OnRestartButtonClicked);
		}

		private void OnRestartButtonClicked()
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

