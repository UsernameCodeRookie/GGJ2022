using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

namespace GridSystem
{
    public class Wall : GridObject
    {
        public bool isBoundary;

        private void OnCollisionEnter2D(Collision2D collision)
        {

        }
    }
}
