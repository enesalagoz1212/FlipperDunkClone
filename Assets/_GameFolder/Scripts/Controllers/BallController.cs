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

		private bool _firstClick = true;
		public void Initialize()
		{

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

		private void Start()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					break;

				case GameState.Playing:
					if (Input.GetMouseButtonDown(0))
					{
						if (_firstClick)
						{
							_firstClick = false;
							_rigidbody2D.bodyType = RigidbodyType2D.Static;
						}
						else
						{
							_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
						}
					}
					GravityScale();
					break;

				case GameState.Reset:
					break;

				case GameState.End:
					_firstClick = true;
					break;
				case GameState.Menu:
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnGameStart()
		{
			transform.position = GameSettingsManager.Instance.gameSettings.ballTransformPosition;
			_rigidbody2D.velocity = Vector2.zero;
			_rigidbody2D.angularVelocity = 0f;
			_rigidbody2D.bodyType = RigidbodyType2D.Static;
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

		private void OnGameReset()
		{
			FreezeRigidbody();
		}

		private void OnGameEnd(bool IsSuccessful)
		{
			FreezeRigidbody();
			GameManager.OnDiamondScored?.Invoke(PlayerPrefsManager.DiamondScore);
		}

		private void FreezeRigidbody()
		{
			_rigidbody2D.bodyType = RigidbodyType2D.Static;
			_rigidbody2D.velocity = Vector2.zero;
		}
	}
}