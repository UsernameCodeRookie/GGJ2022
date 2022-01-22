using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu]
    public class AttackSO : ScriptableObject
    {
        public AnimationCurve curve;
        public float AttackEndTime;
    }
}
