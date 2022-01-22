using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GridSystem;

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

    [Header("Fruits")]
    public List<Fruit> fruitsL, fruitsR;

    public UnityEvent GameStart;


    private void Awake()
    {
        instance = this;
        GameStart.AddListener(GridInitial);
    }

    private void Start()
    {
        GameStart.Invoke();
    }

    private void Update()
    {
        //果子位置冲突逻辑：果子生成时会提供一个占位给对面，当果子刷新时消除占位
        if(fruitsL.Count == 0)
            GeneratFruit(left, fruitGenerateL, fruitsL);
        if(fruitsR.Count == 0)
            GeneratFruit(right, fruitGenerateR, fruitsR);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (left != null) GameObject.Destroy(left.gameObject);
            if (right != null) GameObject.Destroy(right.gameObject);
            fruitsL.Clear();
            fruitsR.Clear();
            GameStart.Invoke();
        }
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

    private void GridInitial()
    {
        GameObject L = Instantiate(gridFactoryLeftPrefab, Vector3.zero, Quaternion.identity);
        GameObject R = Instantiate(gridFactoryRightPrefab, Vector3.zero, Quaternion.identity);

        left = L.GetComponent<GridFactory>();
        right = R.GetComponent<GridFactory>();

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
    }
    
}
