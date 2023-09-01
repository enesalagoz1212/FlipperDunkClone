using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using FlipperDunkClone.Canvases;
using FlipperDunkClone.Controllers;

namespace FlipperDunkClone.Controllers
{
	public class BallController : MonoBehaviour
	{
		GameSettingsManager _gameSettingsManager;
		LevelManager _levelManager;
		UIManager _uiManager;
		ResetCanvas _resetCanvas;
		HoopController _hoopController;

		private Rigidbody2D _rigidbody2D;
		

		public void Initialize(UIManager uiManager, HoopController hoopController, LevelManager levelManager)
		{
			_uiManager = uiManager;
			_hoopController = hoopController;
			_levelManager = levelManager;
		}
		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameEnd -= OnGameEnd;
		}

		void Start()
		{

			_rigidbody2D = GetComponent<Rigidbody2D>();
		}


		private void Update()
		{
			GravityScale();
		}

		private void OnGameStart()
		{
			BallTransformPosition();
			_rigidbody2D.velocity = Vector2.zero;
			_rigidbody2D.angularVelocity = 0f;
		}

		private void GravityScale()
		{
			if (_rigidbody2D.velocity.y < 0)
			{
				// Debug.Log("rb");
				_rigidbody2D.gravityScale = 5f;
			}
			else
			{
				// Debug.Log("-rb");
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
					
					GameManager.Instance.OnBasketThrown();
				}
			}
			else if (other.gameObject.CompareTag("Fail"))
			{
				GameManager.Instance.EndGame(false);
			}
		}

		private void OnGameReset()
		{
			_rigidbody2D.velocity = Vector2.zero;
			BallTransformPosition();
			gameObject.SetActive(true);
			Debug.Log("e");
		}

		public void BallSetActive()
		{
			gameObject.SetActive(true);
		}
		private void OnGameEnd(bool IsSuccessful)
		{
			gameObject.SetActive(false);
			GameManager.OnDiamondScored?.Invoke(PlayerPrefsManager.DiamondScore);
		}
	}

}

