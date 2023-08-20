using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlipperDunkClone.ScriptableObjects
{
    [CreateAssetMenu(fileName ="NewLevel",menuName ="Levels/Level")]
    public class LevelData : ScriptableObject
    {
        public string levelName;
        public int maxScore;
        public GameObject hoopPrefab;
        public Vector3 hoopPosition;
        public int hoopHeight;
    }
}

