using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Config/PlayerConfigSO", order = 1)]
    public class PlayerConfigSO : ScriptableObject
    {
        [Header("Basic Data")]
        public float initSpeed;
        public float rushSpeed;
        public float attackRadius;
        public float maxSp;
        public float maxMp;
        public int maxHp;
        public float mpRecoverRate;
        public int maxAttackCount;

        [Header("Recover Amount Config")]
        public float spRecoverAmount;
        public float mpRecoverAmount;

        [Header("Second Level Data")]
        public float spDecreaseAmount;
        public float loseControlTime;
    }
}