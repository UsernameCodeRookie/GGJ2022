using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using MoreMountains.Feedbacks;

namespace GridSystem
{
    public class Wall : GridObject
    {
        public bool isBoundary;
        private MMFeedbacks mMFeedbacks;

        void Awake()
        {
            if (!isBoundary)
            {
                mMFeedbacks = gameObject.GetComponent<MMFeedbacks>();
                StartCoroutine("FeedBack");
            }
        }

        IEnumerator FeedBack()
        {
            yield return 0;
            mMFeedbacks.PlayFeedbacks();
            yield return 0;
        }

        public void Transfer()
        {
            if (!isBoundary)
            {
                if (LeftOrRight)
                {
                    SetWallInGrid(right);
                }
                else
                {
                    SetWallInGrid(left);
                }
                Disappear();
            }
        }

        private void SetWallInGrid(GridFactory gridFactory)
        {
            gridFactory.SetGridObject(x, y, gridFactory.wallPrefab, true);
        }

        public void Disappear()
        {
            if (LeftOrRight)
            {
                left.SetEmptyGridObject(x, y);
            }
            else
            {
                right.SetEmptyGridObject(x, y);
            }
            if (gameObject != null)
                GameObject.Destroy(gameObject);
        }
    }
}
