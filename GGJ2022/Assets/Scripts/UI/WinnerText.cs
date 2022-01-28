using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinnerText : MonoBehaviour
    {
        Text text;
        void Start()
        {
            text = GetComponent<Text>();
        }
        private void Update()
        {
            GameManager gameManager = GameManager.instance;

            text.text = gameManager.winner == GameManager.GameOverType.LeftWin ? "   小阳获胜 ！" :
                        gameManager.winner == GameManager.GameOverType.RightWin ? "   小阴获胜 ！" : "平手 ！";
        }

    }
}
