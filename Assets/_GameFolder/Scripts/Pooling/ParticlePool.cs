using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Pooling
{
	public class ParticlePool : MonoBehaviour
	{
		public GameObject levelCompletedEffect;
		public GameObject basketParticleEffect;
		public int basketInitializePoolSize;
		public int levelCompeletedInitializePoolSize;
		public GameObject basketParticle;
		public GameObject levelParticle;

		private Stack<GameObject> basketPooledParticle = new Stack<GameObject>();
		private Stack<GameObject> levelCompletedPooledParticle = new Stack<GameObject>();

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameEnd += OnGameEnd;
			GameManager.OnGameReset += OnGameReset;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameEnd -= OnGameEnd;
			GameManager.OnGameReset -= OnGameReset;
		}

		public void Initialize()
		{

		}

		private void Start()
		{

			for (int i = 0; i < basketInitializePoolSize; i++)
			{
				GameObject basket = Instantiate(basketParticleEffect, basketParticle.transform);
				basket.SetActive(false);
				basketPooledParticle.Push(basket);
			}

			for (int i = 0; i < levelCompeletedInitializePoolSize; i++)
			{
				GameObject levelCompleted = Instantiate(levelCompletedEffect, levelParticle.transform);
				levelCompleted.SetActive(false);
				levelCompletedPooledParticle.Push(levelCompleted);
			}
		}


		public GameObject GetParticleBasket(Vector3 position)
		{
			if (basketPooledParticle.Count > 0)
			{
				GameObject particle = basketPooledParticle.Pop();
				particle.transform.position = position;
				particle.SetActive(true);
				return particle;
			}
			else
			{
				GameObject newBasketParticle = Instantiate(basketParticle, position, Quaternion.identity);
				return newBasketParticle;
			}
		}

		public void ReturnParticleBasket(GameObject particleBasket)
		{
			particleBasket.SetActive(false);
			basketPooledParticle.Push(particleBasket);
		}

		public GameObject GetParticleLevelCompleted(Vector3 position)
		{
			if (levelCompletedPooledParticle.Count > 0)
			{
				GameObject particleEffect = levelCompletedPooledParticle.Pop();
				particleEffect.transform.position = position;
				particleEffect.SetActive(true);
				return particleEffect;
			}
			else
			{
				GameObject newLevelCompletedParticle = Instantiate(levelParticle, position, Quaternion.identity);
				return newLevelCompletedParticle;
			}
		}

		public void ReturnParticleLevelCompleted(GameObject particleLevelCompleted)
		{
			particleLevelCompleted.SetActive(false);
			levelCompletedPooledParticle.Push(particleLevelCompleted); ;
		}

		private void OnGameStart()
		{
			DOVirtual.DelayedCall(4f, () =>
			{
				levelParticle.gameObject.SetActive(true);

			});
		}

		private void OnGameEnd(bool isSuccessful)
		{
			if (isSuccessful)
			{
				Vector3 pos = new Vector3(0f, 10.5f, 0f);
				GameObject levelCompletedEffect = GetParticleLevelCompleted(pos);

				DOVirtual.DelayedCall(4f, () =>
				{
					ReturnParticleLevelCompleted(levelCompletedEffect);
				});
			}
		}

		private void OnGameReset()
		{
			levelParticle.gameObject.SetActive(false);
		}
	}
}

