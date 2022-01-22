using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

namespace Gameplay
{
    public class Attack : MonoBehaviour
    {

        private float attackRadius;
        private float lingerTime;
        private float CheckEndTime;
        private float AttackEndTime;

        private Collider2D playerCollider;

        public AttackSO so;

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
