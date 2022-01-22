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
        public float AttackEndTime;

        private Collider2D playerCollider;

        public AnimationCurve curve;

        public void Init(Vector3 position, float attackRadius)
        {
            this.lingerTime = 0f;
            transform.position = position;
            this.attackRadius = attackRadius;
            CheckEndTime = curve.keys[1].time;
            playerCollider = GetComponentInChildren<Collider2D>();
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
            float evaluate = curve.Evaluate(ratio);
            Debug.LogError(evaluate);
            transform.localScale = Vector3.one * evaluate * attackRadius;
            if (ratio > CheckEndTime)
            {
                playerCollider.gameObject.SetActive(false);
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

        }
    }
}
