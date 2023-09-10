using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FlipperDunkClone.Managers
{
    public class InputManager : MonoBehaviour
    {
       public static InputManager Instance { get; private set; }
       public bool isInputEnabled { get; private set; } = true;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void Initialize()
		{

		}

        void Start()
        {

        }

      
        void Update()
        {

        }

        public void OnScreenTouch(PointerEventData eventData)
        {
          
        }
    }
}

