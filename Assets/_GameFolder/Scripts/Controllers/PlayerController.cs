using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using DG.Tweening;


namespace FlipperDunkClone.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		private Rigidbody2D _rigidbody2D;
		private HingeJoint2D _hingeJoint;

		private bool _isMoving = false;
		public float moveSpeed;

		private Vector3 _initialPosition;
		private Quaternion _initialRotation;

		public SpriteRenderer flipperSprite;

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameEnd += OnGameEnd;
			ShopManager.OnFlipperSelected += OnFlipperSelected;
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameEnd = OnGameEnd;
			ShopManager.OnFlipperSelected -= OnFlipperSelected;
		}

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_hingeJoint = GetComponent<HingeJoint2D>();
		}

		private void Start()
		{
			_initialPosition = transform.position;
			_initialRotation = transform.rotation;
			ResetMotor();
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
					_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
					if (Input.GetMouseButtonDown(0))
					{
						_isMoving = true;
					}
					if (Input.GetMouseButtonUp(0))
					{
						_isMoving = false;
						ReturnToInitialPosition();
					}
					if (_isMoving)
					{
						MoveUp();
					}

					break;
				case GameState.Reset:
					break;
				case GameState.End:
					break;
				default:
					break;
			}
		}

		public void ChangeFlipperImage(Sprite newSprite)
		{
			flipperSprite.sprite = newSprite;
		}

		private void OnFlipperSelected(Sprite flipperSprite)
		{
			ChangeFlipperImage(flipperSprite);
		}

		private void MoveUp()
		{
			JointMotor2D motor = _hingeJoint.motor;
			motor.motorSpeed = moveSpeed;
			_hingeJoint.motor = motor;
		}

		private void ReturnToInitialPosition()
		{
			JointMotor2D motor = _hingeJoint.motor;
			motor.motorSpeed = -moveSpeed / 1.5f;
			_hingeJoint.motor = motor;
		}

		private void ResetMotor()
		{
			JointMotor2D motor = _hingeJoint.motor;
			motor.motorSpeed = 0f;
			_hingeJoint.motor = motor;
		}

		private void OnGameStart()
		{
			transform.rotation = Quaternion.identity;
			_rigidbody2D.bodyType = RigidbodyType2D.Static;
		}

		private void OnGameEnd(bool isSuccessful)
		{
			transform.position = _initialPosition;
			transform.rotation = Quaternion.identity;
			ResetMotor();
			_rigidbody2D.bodyType = RigidbodyType2D.Static;
		}
	}
}
