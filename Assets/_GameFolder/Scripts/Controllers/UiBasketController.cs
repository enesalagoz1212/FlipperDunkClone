using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Controllers
{
    public class UiBasketController : MonoBehaviour
    {
        public GameObject tickImage;

		private void OnEnable()
		{
            GameManager.OnGameScoreChanged += OnGameScoreChanged;
		}

		private void OnDisable()
		{
            GameManager.OnGameScoreChanged -= OnGameScoreChanged;
			
		}
		private void Start()
        {
            tickImage.SetActive(false);

        }

        public void OnBasket()
        {
            tickImage.SetActive(true);
        }

      private void OnGameScoreChanged(int currentScore)
		{
            OnBasket();
		}
    }
}