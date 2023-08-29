using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlipperDunkClone.Controllers
{
    public class HoopController : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public GameObject hoopPrefab;
       

       
        public void SpawnRandomHoop()
		{
            Debug.Log("Spawn random Points");
            int randomIndex = Random.Range(0, spawnPoints.Length);

            Transform spawnTransform = spawnPoints[randomIndex];

            var hoopRandomSpawn = Instantiate(hoopPrefab, spawnTransform.position, Quaternion.identity);
		}

    }
}

