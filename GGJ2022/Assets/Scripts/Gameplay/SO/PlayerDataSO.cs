using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerDataSO", order = 1)]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Basic Data")]
        public float initSpeed;
        public float rushSpeed;
        public float atkRad;
        public float maxSp;
        public float maxMp;
        public int maxHp;
        public float mpRate;
        public int atkCnt;

        [Header("Recover Amount Config")]
        public float spRecover;
        public float mpRecover;

        [Header("Second Level Data")]
        public float SpDecreaseAmount;
        public float LoseControlTime;
    }
}