using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Canvases;

namespace FlipperDunkClone.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }



        [SerializeField] private GameCanvas gameCanvas;
        [SerializeField] private SettingsCanvas settingsCanvas;
        [SerializeField] private EndCanvas endCanvas;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Initialize(LevelManager levelManager)
        {

            gameCanvas.Initialize(levelManager, settingsCanvas);
        }

        void Start()
        {

        }


        void Update()
        {

        }
    }
}

