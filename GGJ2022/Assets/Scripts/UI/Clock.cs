using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Clock : MonoBehaviour
    {
        public int sec, min;
        float timer;
        Text text;


        private void Start()
        {
            text = GetComponentInChildren<Text>();
            GameManager gameManager = GameManager.instance;
            gameManager.GameStart.AddListener(Initialize);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            while (timer > 1)
            {
                timer -= 1;
                sec -= 1;
            }
            if(sec < 0)
            {
                sec = 59;
                min -= 1;
            }
            if(min < 0)
            {
                GameOver();
            }

            text.text = min.ToString() + ":" + sec.ToString();
        }

        public void GameOver()
        {
            Debug.LogError("GameOver");
        }

        public void Initialize()
        {
            min = 10;
            sec = 0;
        }
    }
}
