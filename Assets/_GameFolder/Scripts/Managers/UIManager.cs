using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Canvases;
using FlipperDunkClone.Controllers;
using FlipperDunkClone.Pooling;
using FlipperDunkClone.ScriptableObjects;

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
        [SerializeField] private MenuCanvas menuCanvas;
        [SerializeField] private StoreCanvas storeCanvas;
        
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

        public void Initialize(GameManager gameManager, LevelManager levelManager ,BallController ballController ,ShopManager shopManager,SoundManager soundManager,ParticlePool particlePool)
        {
            GameManager.OnGameEnd += OnGameEnd;
            
            endCanvas.Initialize(soundManager,particlePool);
            gameCanvas.Initialize(levelManager, settingsCanvas);
            resetCanvas.Initialize();
            settingsCanvas.Initialize(gameCanvas);
            menuCanvas.Initialize(gameManager, ballController,storeCanvas,settingsCanvas);
            storeCanvas.Initialize(shopManager, menuCanvas);
         
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

