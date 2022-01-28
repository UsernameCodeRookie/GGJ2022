using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Config/PlayerRuntimData", order = 3)]
    public class FruitGenerateConfigSO : ScriptableObject
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
