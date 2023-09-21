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

		private void Start()
		{
			tickImage.SetActive(false);

		}

		public void OnBasket()
		{
			tickImage.SetActive(true);
		}

	
	}
}