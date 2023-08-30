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

		private const string VibrationKey = "IsVibrationOn";
		private const string SoundKey = "IsSoundOn";


		public static bool IsVibrationOn
		{
			get
			{
				if (PlayerPrefs.HasKey(VibrationKey))
				{
					return bool.Parse(PlayerPrefs.GetString(VibrationKey));
				}
				return true;
			}
			set => PlayerPrefs.SetString(VibrationKey, value.ToString());
		}

		public static bool IsSoundOn
		{
			get
			{
				if (PlayerPrefs.HasKey(SoundKey))
				{
					return bool.Parse(PlayerPrefs.GetString(SoundKey));
				}
				return true;
			}
			set => PlayerPrefs.SetString(SoundKey, value.ToString());
		}

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

