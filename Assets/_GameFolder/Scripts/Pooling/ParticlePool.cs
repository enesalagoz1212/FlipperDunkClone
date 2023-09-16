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
		public GameObject particleEffects;

		private Stack<GameObject> basketPooledParticle = new Stack<GameObject>();
		private Stack<GameObject> levelCompletedPooledParticle = new Stack<GameObject>();

		private void OnEnable()
		{
			GameManager.OnGameEnd += OnGameEnd;
		}

		private void OnDisable()
		{
			GameManager.OnGameEnd -= OnGameEnd;

		}

		public void Initialize()
		{
			for (int i = 0; i < basketInitializePoolSize; i++)
			{
				GameObject basketParticle = Instantiate(basketParticleEffect, particleEffects.transform);
				basketParticle.SetActive(false);
				basketPooledParticle.Push(basketParticle);
			}

			for (int i = 0; i < levelCompeletedInitializePoolSize; i++)
			{
				GameObject levelCompeleted = Instantiate(levelCompletedEffect, particleEffects.transform);
				levelCompeleted.SetActive(false);
				levelCompletedPooledParticle.Push(levelCompeleted);
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
				GameObject newBasketParticle = Instantiate(basketParticleEffect, position, Quaternion.identity);
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
				GameObject newLevelCompletedParticle = Instantiate(basketParticleEffect, position, Quaternion.identity);
				return newLevelCompletedParticle;
			}
		}

		public void ReturnParticleLevelCompleted(GameObject particleLevelCompleted)
		{
			particleLevelCompleted.SetActive(false);
			basketPooledParticle.Push(particleLevelCompleted);

		}

		private void OnGameEnd(bool isSuccessful)
		{
			if (isSuccessful)
			{
				Vector3 pos = new Vector3(0f, 10.5f, 0f);
				GameObject levelCompletedEffect = GetParticleLevelCompleted(pos);


				DOVirtual.DelayedCall(6f, () =>
				{
					ReturnParticleLevelCompleted(levelCompletedEffect);
				});

			}
		}
	}
}

