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
        protected GameManager gameManager;

        protected void Start()
        {
            gameManager = GameManager.instance;
            left = gameManager.left;
            right = gameManager.right;
        }

        public virtual void Init(int x,int y, bool LeftOrRight,float cellSize = 1)
        {
            this.x = x;
            this.y = y;
            this.LeftOrRight = LeftOrRight;
            transform.localScale = Vector3.one * cellSize;
        }
    }
}
