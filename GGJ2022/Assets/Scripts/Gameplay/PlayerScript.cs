using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay
{
    public class PlayerScript : MonoBehaviour
    {
        public void Die()
        {
            Debug.LogError("Die");
            Destroy(gameObject);
        }
    }
}
