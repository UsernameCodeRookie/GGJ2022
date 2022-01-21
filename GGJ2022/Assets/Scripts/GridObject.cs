using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GridSystem
{
    public class GridObject : MonoBehaviour
    {
        public int x, y;
        public bool LeftOrRight;
        protected GridFactory left, right;

        protected void Start()
        {
            left = GameObject.FindGameObjectWithTag("LeftGrid").GetComponent<GridFactory>();
            right = GameObject.FindGameObjectWithTag("RightGrid").GetComponent<GridFactory>();
        }
    }
}
