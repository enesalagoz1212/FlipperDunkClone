using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using FlipperDunkClone.Canvases;
using FlipperDunkClone.Controllers;
using FlipperDunkClone.Pooling;
using DG.Tweening;

namespace FlipperDunkClone.Controllers
{
	public class BallController : MonoBehaviour
	{
		GameSettingsManager _gameSettingsManager;
		ResetCanvas _resetCanvas;
		SoundManager _soundManager;
		ParticlePool _particlePool;


		private Rigidbody2D _rigidbody2D;
		public SpriteRenderer ballSpriteRenderer;

		public void Initialize(SoundManager soundManager, ParticlePool particlePool)
		{
			_soundManager = soundManager;
			_particlePool = particlePool;
			
		}

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;
			ShopManager.OnBallSelected += OnBallSelected;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameEnd -= OnGameEnd;
			ShopManager.OnBallSelected -= OnBallSelected;
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
					other.gameObject.tag = "Untagged";				
					GameObject basketParticleEffect = _particlePool.GetParticleBasket(other.transform.position);

					_soundManager.PlayBasketScoreSound();
					_soundManager.PlayApplauseSound();

					UIManager.Instance.GameCanvas.ActivateBasketImage();

					DOVirtual.DelayedCall(0.4f, () =>
					{
						ReturnParticleBasket(basketParticleEffect);
						GameManager.Instance.OnBasketThrown();
					});

					DOVirtual.DelayedCall(1f, () =>
					{
						other.gameObject.tag = "Hoop";
					});
				}
			}
			else if (other.gameObject.CompareTag("Fail"))
			{
				GameManager.Instance.EndGame(false);
				_soundManager.PlayGameOverSound();
			}

		}

		private void ReturnParticleBasket(GameObject particle)
		{
			_particlePool.ReturnParticleBasket(particle);
			Debug.Log("Return particle 2");
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Hoop"))
			{
				if (GameManager.Instance.GameState == GameState.Playing)
				{
					float collisionSpeed = collision.relativeVelocity.magnitude;
					float volume = CalculateSpeed(collisionSpeed);
					SoundManager.Instance.BallSound.volume = volume;
					SoundManager.Instance.BallSound.Play();
				}
			}
		}

		private float CalculateSpeed(float collisionSpeed)
		{
			float maxCollisionSpeed = 150f;
			return Mathf.Clamp01(collisionSpeed / maxCollisionSpeed);
		}

		private void FreezeRigidbody()
		{

			_rigidbody2D.bodyType = RigidbodyType2D.Static;
			_rigidbody2D.velocity = Vector2.zero;
			_rigidbody2D.angularVelocity = 0f;
		}
	}
}