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

        public GameCanvas GameCanvas => gameCanvas;

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

        public void Initialize(LevelManager levelManager)
        {
            GameManager.OnGameEnd += OnGameEnd;
            
            endCanvas.Initialize();
            gameCanvas.Initialize(levelManager, settingsCanvas);
            resetCanvas.Initialize();
            settingsCanvas.Initialize(gameCanvas);
        }

        private void OnGameEnd(bool isSuccessful)
        {
            if (isSuccessful)
            {
                endCanvas.OnGameSuccess();
            }
            else
            {
                resetCanvas.OnGameFail();
            }
        }
    }
}

