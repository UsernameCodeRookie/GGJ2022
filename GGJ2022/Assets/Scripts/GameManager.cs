using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GridSystem;
using Gameplay;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gridFactoryLeftPrefab, gridFactoryRightPrefab;
    [HideInInspector]
    public GridFactory left, right;
    public int width;
    public int height;
    public float cellSize;


    [HideInInspector]
    public Vector3 originOffset;
    [HideInInspector]
    public PlayerScript playerScriptL, playerScriptR;

    [Header("UnityEvent")]
    public UnityEvent GameStart;
    public UnityEvent GameOver;
    public UnityEvent GameReset;

    [Header("Winning Module")]
    public GameOverType winner;

    public AnimationCurve curve;
    public float totalTime;
    [HideInInspector]
    public float timer;

    public bool isPlaying = false;


    public enum GameOverType
    {
        LeftWin,RightWin,Draw
    }

    private void Awake()
    {
        instance = this;
        GameStart.AddListener(() =>
        {
            GridInitial();
            timer = 0;
            isPlaying = true;
        });

        GameOver.AddListener(() =>
        {
            WhoWins();
            DestroyGrid();
            isPlaying = false;
        });

        GameReset.AddListener(() => {
            DestroyGrid();
            GameStart.Invoke();
        });
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isPlaying)
        {
            /*
            right.rootPosition.position =
                new Vector3(-left.rootPosition.position.x - width * cellSize, left.rootPosition.position.y, 0);
            */

            if (Input.GetKeyDown(KeyCode.R))
                GameReset.Invoke();
        }
    }

    private void WhoWins()
    {
        if (playerScriptL != null && playerScriptR != null)
        winner = playerScriptL.hp > playerScriptR.hp ? GameOverType.LeftWin :
                 playerScriptL.hp < playerScriptR.hp ? GameOverType.RightWin : GameOverType.Draw;
    }

    public float SpeedMultiply()
    {
        return curve.Evaluate(timer/totalTime);
    }

    public void DestroyGrid()
    {
        if (left != null) GameObject.Destroy(left.gameObject);
        if (right != null) GameObject.Destroy(right.gameObject);
    }

    #region GridInitial
    private void GridInitial()
    {
        left = Instantiate(gridFactoryLeftPrefab, Vector3.zero, Quaternion.identity, gameObject.transform).GetComponent<GridFactory>();
        right = Instantiate(gridFactoryRightPrefab, Vector3.zero, Quaternion.identity, gameObject.transform).GetComponent<GridFactory>();

        right.rootPosition.position =
                new Vector3(-left.rootPosition.position.x - width * cellSize, left.rootPosition.position.y, 0);

        originOffset = left.rootPosition.position - right.rootPosition.position;
    }
    #endregion
}
