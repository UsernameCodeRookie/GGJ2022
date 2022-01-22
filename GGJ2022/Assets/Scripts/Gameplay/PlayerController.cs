using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 direction;
        private float speed;

        private PlayerScript playerScript;
        private Collider2D playerCollider;

        private string Horizontal, Vertical;
        private KeyCode rushKey;
        public bool LeftOrRight;

        public bool loseControl;
        public bool isRushing;

        private void Start()
        {
            direction = new Vector3(0, -1f, 0);

            playerScript = GetComponent<PlayerScript>();
            playerCollider = GetComponent<Collider2D>();
            playerScript.Init();

            if (LeftOrRight)
            {
                Horizontal = "Horizontal L";
                Vertical = "Vertical L";
                rushKey = KeyCode.LeftShift;
            }
            else
            {
                Horizontal = "Horizontal R";
                Vertical = "Vertical R";
                rushKey = KeyCode.RightShift;
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
            if (!collision.gameObject.GetComponent<Wall>()) return;
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
            StartCoroutine("LoseControl");
            if (isRushing)
            {
                playerScript.BeDamaged(2);
            }
            else
            {
                playerScript.BeDamaged(1);
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
