using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Controllers
{
    public class BallController : MonoBehaviour
    {

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
		}
		void Start()
        {

        }

     
        void Update()
        {

        }

		private void OnGameStart()
		{
			// top position 
		}

	}

}

