using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		private HingeJoint2D hingeJoint;
		private bool isMoving = false;

		public float moveSpeed = 100f;
		private bool isClosed = false;

		public Vector3 initialPosition;
		public Quaternion initialRotation;

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{			
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameEnd = OnGameEnd;
		}
		private void Start()
		{
			isClosed = false;
			hingeJoint = GetComponent<HingeJoint2D>();
			ResetMotor();
		}

		private void Update()
		{
			if (GameManager.Instance.GameState != GameState.Playing || isClosed)
			{
				return;
			}
			if (Input.GetMouseButtonDown(0))
			{
				isMoving = true;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				isMoving = false;
				ReturnToInitialPosition();
			}

			if (isMoving)
			{
				MoveUp();
			}
		}

		private void MoveUp()
		{
			JointMotor2D motor = hingeJoint.motor;
			motor.motorSpeed = moveSpeed;
			hingeJoint.motor = motor;
		}

		private void ReturnToInitialPosition()
		{
			JointMotor2D motor = hingeJoint.motor;
			motor.motorSpeed = -moveSpeed / 2;
			hingeJoint.motor = motor;
		}

		private void ResetMotor()
		{
			JointMotor2D motor = hingeJoint.motor;
			motor.motorSpeed = 0f;
			hingeJoint.motor = motor;
		}

		private void OnGameStart()
		{
			initialPosition = transform.position;
			initialRotation = transform.rotation;
			isClosed = false;
		}

		private void OnGameEnd(bool isSuccessful)
		{
			transform.position = initialPosition;
			transform.rotation = initialRotation;
			isClosed = true;
		}









		/* 
		 * Eski Kod
		 * 
		public float rotationSpeed;

		private Rigidbody2D rb;
		private bool _isRotating;

		private void OnEnable()
		{

			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{

			GameManager.OnGameEnd -= OnGameEnd;

		}
		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			_isRotating = false;
		}

		private void Update()
		{
			if (GameManager.Instance.GameState != GameState.Playing)
			{
				return;
			}
			if (Input.GetMouseButtonDown(0))
			{
				_isRotating = true;
			}
			else if (Input.GetMouseButton(0))
			{
				_isRotating = true;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				_isRotating = false;
			}

			if (_isRotating)
			{
				IncreaseRotation();
			}
			else
			{
				DecreaseRotation();
			}
		}

		public void SetIsRotating(bool isRotating)
		{
			Debug.Log("SetIsRotating");
			_isRotating = isRotating;
		}


		private void OnGameEnd(bool IsSuccesful)
		{
			_isRotating = false;
		}
		private void IncreaseRotation()
		{
			// 0 to -65
			var currentEulerZ = transform.localEulerAngles.z;
			if (currentEulerZ > 180f)
			{
				currentEulerZ -= 360f;
			}
			currentEulerZ -= Time.deltaTime * rotationSpeed;
			currentEulerZ = Mathf.Clamp(currentEulerZ, -75f, 0f);
			rb.MoveRotation(currentEulerZ);
			// transform.rotation = Quaternion.Euler(0f, 0f, currentEulerZ);
		}

		private void DecreaseRotation()
		{
			// -65 to 0
			var currentEulerZ = transform.localEulerAngles.z;
			if (currentEulerZ > 180f)
			{
				currentEulerZ -= 360f;
			}
			currentEulerZ += Time.deltaTime * rotationSpeed;
			currentEulerZ = Mathf.Clamp(currentEulerZ, -75f, 0f);
			rb.MoveRotation(currentEulerZ);
			// transform.rotation = Quaternion.Euler(0f, 0f, currentEulerZ);
		}

		*/






		//**************************************************************

		// private void RotateStick()
		// {
		//     float currentRotation = transform.localRotation.eulerAngles.z;
		//     float newRotation = currentRotation - (rotationSpeed * Time.deltaTime);
		//
		//     if (newRotation < maxRotation)
		//     {
		//         newRotation = maxRotation;
		//     }
		//
		//     
		//     if (currentRotation > maxRotation)
		//     {
		//         newRotation -= (rotationSpeed*6) * Time.deltaTime; 
		//
		//     }
		//     rb.MoveRotation(newRotation);
		// }
		//
		// private void ReturnToZero()
		// {
		//     float currentRotation = transform.localRotation.eulerAngles.z;
		//
		//  
		//     float returnSpeedModified = returnSpeed * (1f - Mathf.Abs(currentRotation) / Mathf.Abs(maxRotation));
		//
		//     float newRotation = currentRotation + (returnSpeedModified * Time.deltaTime);
		//
		//
		//     if (newRotation > 0)
		//     {
		//         newRotation = 0;
		//     }
		//
		//     rb.MoveRotation(newRotation);
		// }
	}
}
