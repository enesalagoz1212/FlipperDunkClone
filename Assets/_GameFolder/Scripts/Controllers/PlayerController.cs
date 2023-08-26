using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float rotationSpeed;

        private Rigidbody2D rb;
        private bool _isRotating;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            _isRotating = false;
        }

        private void Update()
        {
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
