using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

namespace GridSystem
{
    public class Wall : GridObject
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var p = collision.gameObject.GetComponent<PlayerScript>();
            p.Die();
        }
    }
}
