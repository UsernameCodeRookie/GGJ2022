using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using MoreMountains.Feedbacks;

namespace GridSystem
{
    public class Fruit : GridObject
    {

        private MMFeedbacks mMFeedbacks;

        private void Awake()
        {
            StartCoroutine("FeedBack");
        }

        IEnumerator FeedBack()
        {
            yield return 0;
            mMFeedbacks = gameObject.GetComponentInChildren<MMFeedbacks>();
            mMFeedbacks.PlayFeedbacks();
            yield return 0;
        }

        private FruitManager fruitManager;

        public void SetFruitManager(FruitManager fruitManager)
        {
            this.fruitManager = fruitManager;
        }

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
            fruitManager.RemoveFruit(this);
            Disappear();
        }

        private void SetWallInGrid(GridFactory gridFactory)
        {
            gridFactory.RemoveEmptyPosition(x, y);
            gridFactory.SetGridObject<Wall>(x, y, gridFactory.wallPrefab);
        }

        public void Disappear()
        {
            left.AddEmptyPostion(x, y);
            right.AddEmptyPostion(x, y);
            if(gameObject != null)
                GameObject.Destroy(gameObject);
        }
    }
}
