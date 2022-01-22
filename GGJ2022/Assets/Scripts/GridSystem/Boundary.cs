using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Boundary : MonoBehaviour
    {
        public static Boundary instance;

        private void Start()
        {
            instance = this;
        }
    }
}
