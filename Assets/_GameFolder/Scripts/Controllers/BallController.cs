using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using FlipperDunkClone.Canvases;
using FlipperDunkClone.Controllers;
using DG.Tweening;

namespace FlipperDunkClone.Controllers
{
	public class BallController : MonoBehaviour
	{
		GameSettingsManager _gameSettingsManager;
		ResetCanvas _resetCanvas;

		private Rigidbody2D _rigidbody2D;
		public SpriteRenderer ballSpriteRenderer;

		public void Initialize()
		{

		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
			GameManager.OnBallSelected += OnBallSelected;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameEnd -= OnGameEnd;
			GameManager.OnBallSelected -= OnBallSelected;
		}

		private void Start()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			OnGameStart();
		}

		private void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Menu:
					break;

				case GameState.Start:
					break;

				case GameState.Playing:
					if (Input.GetMouseButtonDown(0))
					{
						_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
					}
					GravityScale();
					break;

				case GameState.Reset:
					break;

				case GameState.End:
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnGameStart()
		{
			_rigidbody2D.bodyType = RigidbodyType2D.Static;
		}

		private void OnGameReset()
		{
			transform.position = GameSettingsManager.Instance.gameSettings.ballTransformPosition;
		}

		private void OnGameEnd(bool isSuccessful)
		{
			FreezeRigidbody();
			GameManager.OnDiamondScored?.Invoke(PlayerPrefsManager.DiamondScore);
		}

		private void OnBallSelected(Sprite ballSprite)
		{
			ChangeBallImage(ballSprite);
		}

		public void ChangeBallImage(Sprite newSprite)
		{
			ballSpriteRenderer.sprite = newSprite;
		}

		private void GravityScale()
		{
			if (_rigidbody2D.velocity.y < 0)
			{
				_rigidbody2D.gravityScale = 5f;
			}
			else
			{
				_rigidbody2D.gravityScale = 3f;
			}
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

		private void FreezeRigidbody()
		{

			_rigidbody2D.bodyType = RigidbodyType2D.Static;
			_rigidbody2D.velocity = Vector2.zero;
			_rigidbody2D.angularVelocity = 0f;
		}
	}
}