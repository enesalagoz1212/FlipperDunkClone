using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using TMPro;

namespace FlipperDunkClone.Canvases
{
    public class GameCanvas : MonoBehaviour
    {
		public TextMeshProUGUI scoreText;
		private void OnEnable()
		{
			GameManager.OnGameScoreIncreased += OnGameScoreIncreased;
			GameManager.OnGameStarted += OnGameStart;
		}
		private void OnDisable()
		{
			GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
			GameManager.OnGameStarted -= OnGameStart;
		}
		void Start()
        {

        }

		private void OnGameStart()
		{
			UpdateScoreText();
		}

		private void OnGameScoreIncreased()
		{
			UpdateScoreText();
		}

		public void UpdateScoreText()
		{
			scoreText.text = "Skor: " + GameManager.Instance.score.ToString();
		}

	}
}

