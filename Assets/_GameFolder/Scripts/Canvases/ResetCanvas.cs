using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Controllers;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Canvases
{
	public class ResetCanvas : MonoBehaviour
	{
		BallController _ballController;

		public GameObject resetPanel;
		public Button restartButton;

		public void Initialize(BallController ballController)
		{
			_ballController = ballController;

		}

		private void OnEnable()
		{
			GameManager.OnGameReset += OnGameReset;

		}
		private void OnDisable()
		{
			GameManager.OnGameReset -= OnGameReset;


		}
		void Start()
		{
			resetPanel.SetActive(false);
			restartButton.onClick.AddListener(OnRestartButtonClicked);
		}

		private void OnRestartButtonClicked()
		{
			GameManager.Instance.ResetGame();

			_ballController.BallTransformPosition();
			_ballController.BallSetActice();

			resetPanel.SetActive(false);
		}

		public void ResetPanelGame()
		{
			Debug.Log("oyun fail oldu");  
			resetPanel.SetActive(true);
		}


		private void OnGameReset()
		{

		}
		void Update()
		{

		}
	}

}

