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
    private FruitGenerate fruitGenerateL, fruitGenerateR;
    public FruitGenerateSO fruitGenerateSO;

    [HideInInspector]
    public Vector3 originOffset;
    [HideInInspector]
    public PlayerScript playerScriptL, playerScriptR;

    [Header("Fruits")]
    public List<Fruit> fruitsL, fruitsR;

    [Header("GameSetting")]
    public UnityEvent GameStart;
    public UnityEvent GameOver;

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
            fruitsL.Clear();
            fruitsR.Clear();
            isPlaying = false;
        });
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isPlaying)
        {
            right.rootPosition.position =
                new Vector3(-left.rootPosition.position.x - width * cellSize, left.rootPosition.position.y, 0);

            if (fruitsL.Count == 0)
                GeneratFruit(left, fruitGenerateL, fruitsL);
            if (fruitsR.Count == 0)
                GeneratFruit(right, fruitGenerateR, fruitsR);

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (left != null) GameObject.Destroy(left.gameObject);
                if (right != null) GameObject.Destroy(right.gameObject);
                fruitsL.Clear();
                fruitsR.Clear();
                GameStart.Invoke();
            }

            WhoWins();
        }
    }

    public void Reset()
    {
        if (left != null) GameObject.Destroy(left.gameObject);
        if (right != null) GameObject.Destroy(right.gameObject);
        fruitsL.Clear();
        fruitsR.Clear();
        GameStart.Invoke();
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

    #region GenerateFruit
    public bool RandomGenerateFruit(GridFactory gridFactory,out Fruit gridObject)
    {
        gridObject = null;
        if (gridFactory.emptyPosition.Count != 0)
        {
            int index = (int)(UnityEngine.Random.value * gridFactory.emptyPosition.Count);
            var position = gridFactory.emptyPosition[index];
            gridFactory.SetGridObject(index, gridFactory.fruitPrefab, out gridObject);

            gridFactory.otherGridFactory.RemoveEmptyGridObject(position.x, position.y);
        }
        return true;
    }

    public void GeneratFruit(GridFactory gridFactory, FruitGenerate fruitGenerate, List<Fruit> fruits)
    {

        fruitGenerate.generateTimer = 0;
        for (int i = fruits.Count - 1; i >= 0; i--)
        {
            Fruit fruit = fruits[i];
            fruits.RemoveAt(i);
            fruit.Disappear();
        }

        for (int i = 0; i < fruitGenerate.generateAmount; i++)
        {
            Fruit fruit;
            RandomGenerateFruit(gridFactory, out fruit);
            fruits.Add(fruit);
        }
    }
    #endregion

    #region GridInitial
    private void GridInitial()
    {
        GameObject L = Instantiate(gridFactoryLeftPrefab, Vector3.zero, Quaternion.identity,gameObject.transform);
        GameObject R = Instantiate(gridFactoryRightPrefab, Vector3.zero, Quaternion.identity,gameObject.transform);


        left = L.GetComponent<GridFactory>();
        right = R.GetComponent<GridFactory>();

        right.rootPosition.position =
                new Vector3(-left.rootPosition.position.x - width * cellSize, left.rootPosition.position.y, 0);

        left.SetValue(width, height, cellSize);
        left.Initialize();
        left.otherGridFactory = right;
        right.SetValue(width, height, cellSize);
        right.Initialize();
        right.otherGridFactory = left;

        fruitGenerateL = L.GetComponent<FruitGenerate>();
        fruitGenerateR = R.GetComponent<FruitGenerate>();

        fruitGenerateL.SetValue(fruitGenerateSO);
        fruitGenerateR.SetValue(fruitGenerateSO);

        fruitGenerateL.UpdateTimerEvent.AddListener(() => GeneratFruit(left, fruitGenerateL, fruitsL));
        fruitGenerateR.UpdateTimerEvent.AddListener(() => GeneratFruit(right, fruitGenerateR, fruitsR));

        originOffset = left.rootPosition.position - right.rootPosition.position;
    }
    #endregion
}
