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

        private bool isRotating = false;

        void Update()
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

        void StartRotation()
        {
            isRotating = true;
        }

        void StopRotation()
        {
            isRotating = false;
        }

        void RotateStick()
        {
       
            float rotation = transform.localRotation.eulerAngles.z;
            rotation -= rotationSpeed * Time.deltaTime;

         
            if (rotation < maxRotation)
            {
                rotation = maxRotation;
            }

            transform.localRotation = Quaternion.Euler(0, 0, rotation);
        }

        void ReturnToZero()
        {
          
            float rotation = transform.localRotation.eulerAngles.z;
            rotation += returnSpeed * Time.deltaTime;

          
            if (rotation > 0)
            {
                rotation = 0;
            }

            transform.localRotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}
