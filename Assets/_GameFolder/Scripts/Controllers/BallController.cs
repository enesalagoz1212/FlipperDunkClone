using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Controllers
{
	public class BallController : MonoBehaviour
	{
		private Rigidbody2D _rigitbody2D;
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
			_rigitbody2D = GetComponent<Rigidbody2D>();
		}


		void Update()
		{
			if (_rigitbody2D.velocity.y<0)
			{
				//Debug.Log("rb");
				_rigitbody2D.gravityScale = 2f;
			}
			else
			{
				//Debug.Log("-rb");
				_rigitbody2D.gravityScale = 1.5f;
			}
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

