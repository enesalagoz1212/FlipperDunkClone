using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Canvases;
using FlipperDunkClone.Controllers;

namespace FlipperDunkClone.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public ResetCanvas ResetCanvas => resetCanvas;
        public GameCanvas GameCanvas => gameCanvas;
        public EndCanvas EndCanvas => endCanvas;

        [SerializeField] private GameCanvas gameCanvas;
        [SerializeField] private SettingsCanvas settingsCanvas;
        [SerializeField] private EndCanvas endCanvas;
        [SerializeField] private ResetCanvas resetCanvas;
        
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

        public void Initialize(LevelManager levelManager ,BallController ballController)
        {
            endCanvas.Initialize();
            gameCanvas.Initialize(levelManager, settingsCanvas);
            resetCanvas.Initialize(ballController);
            settingsCanvas.Initialize(gameCanvas);
        }

    }
}

