using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.ScriptableObjects;

namespace FlipperDunkClone.Managers
{
	public class GameSettingsManager : MonoBehaviour
	{
		public static GameSettingsManager Instance { get; private set; }

		public GameSettings gameSettings;

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
		}

	}
}

