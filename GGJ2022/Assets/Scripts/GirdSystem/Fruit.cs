using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Fruit : GridObject
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (LeftOrRight)
            {
                SetWallInGrid(right);
            }
            else
            {
                SetWallInGrid(left);
            }
            Destroy(gameObject);
        }

        private void SetWallInGrid(GridFactory gridFactory)
        {
            gridFactory.SetGridObject(x, y, gridFactory.wallPrefab);
        }
    }
}
