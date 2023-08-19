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
			transform.position =new Vector2(3f, -4f);
		}
		private void OnTriggerEnter2D(Collider2D collision)
		{

			if (collision.gameObject.CompareTag("Hoop"))
			{
				Debug.Log("1");
				if (transform.position.y > collision.transform.position.y)
				{
					Debug.Log("2");
					GameManager.Instance.IncreaseScore(1);
					Debug.Log("3");
				}
			}
		}
	
	}

}

