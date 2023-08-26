using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float rotationSpeed;
        public float maxRotation;
        public float returnSpeed;

        private Rigidbody2D rb;
        private bool isRotating = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isRotating)
            {
                StartRotation();
            }

            if (Input.GetMouseButtonUp(0) && isRotating)
            {
                StopRotation();
            }

            if (isRotating)
            {
                RotateStick();
            }
            else
            {
                ReturnToZero();
            }
        }

        private void StartRotation()
        {
            isRotating = true;
        }

        private void StopRotation()
        {
            isRotating = false;
        }

        private void RotateStick()
        {
            float currentRotation = transform.localRotation.eulerAngles.z;
            float newRotation = currentRotation - (rotationSpeed * Time.deltaTime);

            if (newRotation < maxRotation)
            {
                newRotation = maxRotation;
            }

            
            if (currentRotation > maxRotation)
            {
                newRotation -= (rotationSpeed*6) * Time.deltaTime; 
      
            }
            rb.MoveRotation(newRotation);
        }

        private void ReturnToZero()
        {
            float currentRotation = transform.localRotation.eulerAngles.z;

         
            float returnSpeedModified = returnSpeed * (1f - Mathf.Abs(currentRotation) / Mathf.Abs(maxRotation));

            float newRotation = currentRotation + (returnSpeedModified * Time.deltaTime);

        
            if (newRotation > 0)
            {
                newRotation = 0;
            }

            rb.MoveRotation(newRotation);
        }
    }
}
