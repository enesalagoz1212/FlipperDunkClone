using UnityEngine;
using UnityEngine.UI;
using FlipperDunkClone.Controllers;
using FlipperDunkClone.Managers;
using TMPro;

namespace FlipperDunkClone.Canvases
{
	public class ResetCanvas : MonoBehaviour
	{
		public GameObject resetPanel;
		public Button restartButton;

		public TextMeshProUGUI resetLevelText;
		public TextMeshProUGUI resetDiamondScore;
		
		public void Initialize()
		{
			
		}

		private void OnEnable()
		{
			GameManager.OnDiamondScored += OnDiamondScore;
		}
		
		private void OnDisable()
		{
			GameManager.OnDiamondScored = OnDiamondScore;
		}
		
		void Start()
		{
			resetPanel.SetActive(false);
			restartButton.onClick.AddListener(OnRestartButtonClicked);
		}

		private void OnRestartButtonClicked()
		{
			GameManager.Instance.ResetGame();
			
			resetPanel.SetActive(false);
		}

		public void OnGameFail()
		{
			resetPanel.SetActive(true);
			UpdateResetLevelText();
		}
		
		private void UpdateResetLevelText()
		{
			var currentLevel = PlayerPrefsManager.CurrentLevel;
			resetLevelText.text = "LEVEL " + currentLevel.ToString();
		}

		private void OnDiamondScore(int score)
		{
			resetDiamondScore.text = PlayerPrefsManager.DiamondScore.ToString();
		}
	}
}