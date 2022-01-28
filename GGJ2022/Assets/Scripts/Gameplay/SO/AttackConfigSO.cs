using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Config/PlayerRuntimData", order = 2)]

    public class AttackConfigSO : ScriptableObject
    {
        public AnimationCurve curve;
        public float AttackEndTime;
    }
}
