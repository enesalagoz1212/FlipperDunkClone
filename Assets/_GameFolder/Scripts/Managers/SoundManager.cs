using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlipperDunkClone.Managers
{
	public class SoundManager : MonoBehaviour
	{
		public static SoundManager Instance { get; private set; }

		private AudioSource _ballSound;
		private AudioSource _levelCompletedSound;
		private AudioSource _gameOverSound;
		private AudioSource _basketScoreSound;
		private AudioSource _applauseSound;

		[SerializeField] private AudioClip ballSoundClip;
		[SerializeField] private AudioClip levelCompletedSoundClip;
		[SerializeField] private AudioClip gameOverSoundClip;
		[SerializeField] private AudioClip basketScoreSoundClip;
		[SerializeField] private AudioClip applauseSoundClip;

		public void Initialize()
		{

		}

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
		void Start()
		{
			_ballSound = gameObject.AddComponent<AudioSource>();
			_levelCompletedSound = gameObject.AddComponent<AudioSource>();
			_gameOverSound = gameObject.AddComponent<AudioSource>();
			_basketScoreSound = gameObject.AddComponent<AudioSource>();
			_applauseSound = gameObject.AddComponent<AudioSource>();

			_ballSound.clip = ballSoundClip;
			_levelCompletedSound.clip = levelCompletedSoundClip;
			_gameOverSound.clip = gameOverSoundClip;
			_basketScoreSound.clip = basketScoreSoundClip;
			_applauseSound.clip = applauseSoundClip;
		}

		public void PlayBallSound()
		{
			_ballSound.Play();
		}

		public void PlayLevelCompletedSound()
		{
			_levelCompletedSound.Play();
		}

		public void PlayGameOverSound()
		{
			_gameOverSound.Play();
		}

		public void PlayBasketScoreSound()
		{
			_basketScoreSound.Play();
		}
		public void PlayApplauseSound()
		{
			_applauseSound.Play();
		}
	}
}

