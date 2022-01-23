using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GridSystem;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 direction;
        private float speed;
        private float attackRadius;

        public PlayerScript playerScript;

        private string Horizontal, Vertical;
        private KeyCode rushKey, attackKey;
        public bool LeftOrRight;

        public bool loseControl;
        public bool isRushing;

        public Attack attackPrefab;
        public _Attack _attackPrefab;

        public UnityEvent AttackEvent;

        public Camera mCamera;
        public CameraController cameraController;
        //public UnityAction cameraShakeAction;

        private void Start()
        {
            direction = new Vector3(0, -1f, 0);

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

            mCamera = Camera.main;
            cameraController = mCamera.GetComponentInParent<CameraController>();
            //cameraShakeAction += cameraController.CameraShake;
            AttackEvent.AddListener(cameraController.CameraShake);
        }

        private void Update()
        {
            if (Input.GetKey(rushKey))
            {
                isRushing = true;
                if (playerScript.sp > 0)
                    speed = playerScript.rushSpeed;
                else
                    speed = playerScript.speed;
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
                collision.gameObject.GetComponent<Wall>().ShakeFeedback();
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
