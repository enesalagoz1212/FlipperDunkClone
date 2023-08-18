using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlipperDunkClone.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

		void Start()
		{

		}


		void Update()
		{

		}
	}

}
