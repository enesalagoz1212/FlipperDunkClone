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
			transform.position =new Vector2(3.5f, 6.5f);
		}
		private void OnTriggerEnter2D(Collider2D other)
		{

			if (other.gameObject.CompareTag("Hoop"))
			{
				if (transform.position.y > other.transform.position.y)
				{
					GameManager.Instance.IncreaseScore();
				}
			}
			else if (other.gameObject.CompareTag("End"))
			{
				GameManager.Instance.EndGame();
			}

		}
	
	}

}

