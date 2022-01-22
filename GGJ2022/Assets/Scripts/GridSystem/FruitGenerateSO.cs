using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    [CreateAssetMenu]
    public class FruitGenerateSO : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        [Header("Settings")]
        public float generateFrequency;
        public int generateAmount;
        public float frequencyVelocity;
        public float frequencyAddAmount;
        public float amountVelocity;
    }
}
