using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlipperDunkClone.ScriptableObjects
{
    [CreateAssetMenu(fileName ="GameSettings",menuName ="Data/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public Vector3 ballTransformPosition;
        public float jumpForce;
        public float fallSpeed;
        public float maxFallSpeed;

    }
}

