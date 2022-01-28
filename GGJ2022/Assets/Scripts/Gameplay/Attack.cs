using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using MoreMountains.Feedbacks;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class Attack : MonoBehaviour
    {

        private float attackRadius;
        private float lingerTime;
        private float CheckEndTime;
        private float AttackEndTime;

        private Collider2D playerCollider;

        public AttackConfigSO so;
        private MMFeedbacks mMFeedbacks;

        void Awake()
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

        public void Init(Vector3 position, float attackRadius)
        {
            transform.position = position;
            transform.localScale = Vector3.zero;

            this.lingerTime = 0f;
            this.attackRadius = attackRadius;
            CheckEndTime = so.curve.keys[1].time;
            AttackEndTime = so.AttackEndTime;
            playerCollider = GetComponent<Collider2D>();
        }

        private void FixedUpdate()
        {
            lingerTime += Time.deltaTime;

            if(lingerTime >= AttackEndTime)
            {
                GameObject.Destroy(gameObject);
                return;
            }

            float ratio = lingerTime / AttackEndTime;
            float evaluate = so.curve.Evaluate(ratio);
            transform.localScale = Vector3.one * evaluate * attackRadius;
            if (ratio > CheckEndTime)
            {
                playerCollider.enabled = false;
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Wall o = collision.gameObject.GetComponent<Wall>();
            if (o != null)
            {
                o.Transfer();
            }
        }

    }
}
