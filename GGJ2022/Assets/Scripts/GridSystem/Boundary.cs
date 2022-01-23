using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Boundary : MonoBehaviour
    {
        public static Boundary instance;

        public GameObject BoundaryL;
        public GameObject BoundaryR;

        private void Awake()
        {
            instance = this;
            GameManager.instance.GameOver.AddListener(() => DestroyBoundary());
        }

        public void DestroyBoundary()
        {
            foreach(Wall o in GetComponentsInChildren<Wall>())
            {
                Destroy(o.gameObject);
            }
        }
    }
}
