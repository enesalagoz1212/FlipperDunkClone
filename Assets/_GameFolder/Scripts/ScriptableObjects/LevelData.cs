using UnityEngine;

namespace FlipperDunkClone.ScriptableObjects
{
    [CreateAssetMenu(fileName ="NewLevel",menuName ="Levels/Level")]
    public class LevelData : ScriptableObject
    {
        public int maxScore;
    }
}

