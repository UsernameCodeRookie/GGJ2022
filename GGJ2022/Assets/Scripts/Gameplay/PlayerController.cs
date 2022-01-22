using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GridSystem;

namespace Gameplay
{
    [RequireComponent(typeof(PlayerScript))]
    public class PlayerController : MonoBehaviour
    {
        private Vector3 direction;
        private float speed;
        private float attackRadius;

        private PlayerScript playerScript;

        private string Horizontal, Vertical;
        private KeyCode rushKey, attackKey;
        public bool LeftOrRight;

        public bool loseControl;
        public bool isRushing;

        public Attack attackPrefab;
        public _Attack _attackPrefab;

        public UnityEvent AttackEvent;

        public UnityEvent Collision;

        private void Start()
        {
            direction = new Vector3(0, -1f, 0);

            playerScript = GetComponent<PlayerScript>();
            playerScript.Init(LeftOrRight);
            attackRadius = playerScript.data.atkRad;
            AttackEvent.AddListener(() => Instantiate(attackPrefab).Init(transform.position, attackRadius));
            AttackEvent.AddListener(() => Instantiate(_attackPrefab).Init(transform.position, attackRadius, LeftOrRight));

            if (LeftOrRight)
            {
                Horizontal = "Horizontal L";
                Vertical = "Vertical L";
                rushKey = KeyCode.V;
                attackKey = KeyCode.B;
            }
            else
            {
                Horizontal = "Horizontal R";
                Vertical = "Vertical R";
                rushKey = KeyCode.Alpha1;
                attackKey = KeyCode.Alpha2;
            }
        }

        private void Update()
        {
            if (Input.GetKey(rushKey))
            {
                isRushing = true;
                if(playerScript.SpDrop())
                    speed = playerScript.rushSpeed;
            }
            else
            {
                isRushing = false;
                speed = playerScript.speed;
            }

            if (Input.GetKeyDown(attackKey))
            {
                AttackEvent.Invoke();
            }
        }

        private void FixedUpdate()
        {
            Vector3 moveInput = new Vector3(Input.GetAxis(Horizontal), Input.GetAxis(Vertical), 0);
            if (moveInput != Vector3.zero & !loseControl)
                direction = moveInput.normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Wall>())
            {
                direction = Vector3.Reflect(direction, collision.contacts[0].normal);
                StartCoroutine("LoseControl");
                if (isRushing)
                {
                    playerScript.BeDamaged(2);
                    playerScript.Hurt.Invoke();
                }
                else
                {
                    playerScript.BeDamaged(1);
                    playerScript.Hurt.Invoke();
                }

                Collision.Invoke();
            }
        }


        IEnumerator LoseControl()
        {
            float loseControlProgress;
            loseControl = true;
            for (loseControlProgress = 0; loseControlProgress < playerScript.data.LoseControlTime; loseControlProgress += Time.deltaTime)
            {
                yield return 0;
            }
            loseControl = false;
        }
    }
}
