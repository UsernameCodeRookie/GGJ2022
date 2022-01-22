using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameStart();
    }

    private void Update()
    {
        //����λ�ó�ͻ�߼�����������ʱ���ṩһ��ռλ�����棬������ˢ��ʱ����ռλ
        GeneratFruit(left, fruitGenerateL, fruitsL);
        GeneratFruit(right, fruitGenerateR, fruitsR);

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameStart();
        }
    }

    #region GenerateFruit
    public bool RandomGenerateFruit(GridFactory gridFactory,out Fruit gridObject)
    {
        gridObject = null;
        if (gridFactory.emptyPosition.Count != 0)
        {
            int index = (int)(Random.value * gridFactory.emptyPosition.Count);
            var position = gridFactory.emptyPosition[index];
            gridFactory.SetGridObject(index, gridFactory.fruitPrefab, out gridObject);

            gridFactory.otherGridFactory.RemoveEmptyGridObject(position.x, position.y);
        }
        return true;
    }

    public void GeneratFruit(GridFactory gridFactory, FruitGenerate fruitGenerate, List<Fruit> fruits)
    {
        if (fruitGenerate.UpdateTimer() | fruits.Count == 0)
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
    }
    #endregion

    private void GameStart()
    {
        if (left != null) GameObject.Destroy(left.gameObject);
        if (right != null) GameObject.Destroy(right.gameObject);

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
    }
    
}
