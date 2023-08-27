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
		private UIManager _uiManager;

		private Rigidbody2D _rigidbody2D;

		
		private bool _canClick;


		public void Initialize(UIManager uiManager)
		{
			_uiManager = uiManager;

		}
		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameReset -= OnGameReset;
		}

		void Start()
		{
			_canClick = true;
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}


		private void Update()
		{
			if (GameManager.Instance.GameState==GameState.Playing)
			{
				Debug.Log("ewrfewtwtwe egfagfawgf");
				GravitScale();
				Debug.Log("yjdjfdj");
			}
		}

		private void OnGameStart()
		{
			BallTransformPosition();
			_rigidbody2D.velocity = Vector2.zero;
			_rigidbody2D.angularVelocity = 0f;			
		}

		private void GravitScale()
		{
			if (_rigidbody2D.velocity.y < 0)
			{
				Debug.Log("rb");
				_rigidbody2D.gravityScale = 5f;
			}
			else
			{
				Debug.Log("-rb");
				_rigidbody2D.gravityScale = 3f;
			}
		}
		public void BallTransformPosition()
		{
			Debug.Log("Ball");
			transform.position = GameSettingsManager.Instance.gameSettings.ballTransformPosition;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{

			if (other.gameObject.CompareTag("Hoop"))
			{
				if (transform.position.y > other.transform.position.y)
				{
					GameManager.Instance.DecreaseScore();
				}
			}
			else if (other.gameObject.CompareTag("Reset"))
			{
				GameManager.Instance.ResetGame();
			}
		}

		private void OnGameReset()
		{
			_canClick = false;
			_rigidbody2D.velocity = Vector2.zero;


		}


	}

}

