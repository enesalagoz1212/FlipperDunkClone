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
		}
		private void OnDisable()
		{
			GameManager.OnGameScoreIncreased -= OnGameScoreIncreased;
		}
		void Start()
        {

        }

		private void OnGameScoreIncreased(int score)
		{
			UpdateScoreText();
		}

		public void UpdateScoreText()
		{
			scoreText.text = "Skor: " + GameManager.Instance.score.ToString();
		}

	}
}

