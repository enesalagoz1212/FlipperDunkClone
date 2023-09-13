using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlipperDunkClone.Managers
{
	public class VibrationManager : MonoBehaviour
	{
		//[SerializeField] private RichtapEffectSource _vibration;

		private void OnEnable()
		{
			GameManager.OnGameEnd += OnGameEnd;
		}
		private void OnDisable()
		{
			
			GameManager.OnGameEnd -= OnGameEnd;
		}
		public void VibrationOnBasketScore()
		{
		//	_vibration.Play();
		}

		public void VibrationOnGameEnd()
		{
		//	_vibration.Play();
		}

		private void OnGameEnd(bool isSuccesful)
		{
			Debug.Log("Vibration worked");
			VibrationOnGameEnd();
		}
	}
}

