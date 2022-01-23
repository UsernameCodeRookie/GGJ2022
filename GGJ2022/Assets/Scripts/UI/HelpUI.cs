using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HelpUI : MonoBehaviour
    {
        public List<Texture2D> HelpImage;

        public int frame;
        RawImage rawImage;

        private void Awake()
        {
            frame = 0;
            rawImage = GetComponentInChildren<RawImage>();

        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                frame++;
                if(frame <= 2)
                {
                    rawImage.texture = HelpImage[frame];
                }
                else if(frame == 3)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
