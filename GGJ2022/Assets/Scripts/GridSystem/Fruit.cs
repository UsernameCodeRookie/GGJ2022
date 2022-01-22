using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

namespace GridSystem
{
    public class Fruit : GridObject
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var p = collision.gameObject.GetComponent<PlayerController>();
            p.playerScript.GetFruit();
			
            if (LeftOrRight)
            {
                SetWallInGrid(right);
            }
            else
            {
                SetWallInGrid(left);
            }
            gameManager.fruitsL.Remove(this);
            gameManager.fruitsR.Remove(this);
            Disappear();
        }

        private void SetWallInGrid(GridFactory gridFactory)
        {
            gridFactory.SetGridObject(x, y, gridFactory.wallPrefab, true);
        }

        public void Disappear()
        {
            left.SetEmptyGridObject(x, y);
            right.SetEmptyGridObject(x, y);
            if(gameObject != null)
                GameObject.Destroy(gameObject);
        }
    }
}
