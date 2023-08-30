using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;
using FlipperDunkClone.ScriptableObjects;

namespace FlipperDunkClone.Controllers
{
	public class HoopController : MonoBehaviour
	{
		public Transform[] spawnPoints;
		public GameObject hoopPrefab;




		LevelManager _levelManager;
		private LevelData _currentLevelData;

		public void Initialize(LevelManager levelManager)
		{
			_levelManager = levelManager;
		}

		private void Start()
		{

		}




		public void SpawnRandomHoop()
		{
			
			int randomIndex;
			do
			{
				Debug.Log("Spawn random Points");
				randomIndex = Random.Range(0, spawnPoints.Length);


			} while (spawnPoints[randomIndex].position == hoopPrefab.transform.position);

			Transform spawnTransform = spawnPoints[randomIndex];
			if (randomIndex == 2 || randomIndex == 3)
			{
				Debug.Log("randomIndex 2 veya 3 ");
				Quaternion hooRotation = Quaternion.Euler(-180, 0, 180);
				var hoopRandomSpawn = Instantiate(hoopPrefab, spawnTransform.position, hooRotation);
			}
			else
			{
				var hoopRandomSpawn = Instantiate(hoopPrefab, spawnTransform.position, Quaternion.identity);
			}

		}


		//public void RemoveRandomHoop()
		//{
		//	if (hoopPrefab != null)
		//	{
		//		Destroy(hoopPrefab);
		//	}
		//}
	}
}

