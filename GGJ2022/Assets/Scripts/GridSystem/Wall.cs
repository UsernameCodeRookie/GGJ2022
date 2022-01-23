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
        private MMFeedbacks[] mMFeedbackList;

        void Awake()
        {
            StartCoroutine("AppearFeedBack");
        }

        IEnumerator AppearFeedBack()
        {
            yield return 0;
            //mMFeedbacks = gameObject.GetComponentInChildren<MMFeedbacks>();
            mMFeedbackList = gameObject.GetComponentsInChildren<MMFeedbacks>();
            if(!isBoundary)
                AppearanceFeedback();
            yield return 0;
        }

        private void AppearanceFeedback()
        {
            mMFeedbackList[0].PlayFeedbacks();
        }

        public void ShakeFeedback()
        {
            if (isBoundary)
                mMFeedbackList[0].PlayFeedbacks();
            else
                mMFeedbackList[1].PlayFeedbacks();
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
