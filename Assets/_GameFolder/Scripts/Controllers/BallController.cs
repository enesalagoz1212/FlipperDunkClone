using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Controllers
{
	public class BallController : MonoBehaviour
	{
		GameSettingsManager gameSettingsManager;
		LevelManager levelManager;
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
			_rigitbody2D.velocity = Vector2.zero;
			_rigitbody2D.angularVelocity = 0f;
			transform.position = GameSettingsManager.Instance.gameSettings.ballTransformPosition;
		}
		private void OnTriggerEnter2D(Collider2D other)
		{

			if (other.gameObject.CompareTag("Hoop"))
			{
				if (transform.position.y > other.transform.position.y)
				{
					GameManager.Instance.IncreaseScore();
					LevelManager.Instance.LevelCompleted();
				}
			}
			else if (other.gameObject.CompareTag("End"))
			{
				GameManager.Instance.EndGame();
			}

		}
	
	}

}

