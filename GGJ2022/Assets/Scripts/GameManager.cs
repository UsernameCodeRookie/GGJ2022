using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GridFactory left, right;
    public int width;
    public int height;
    public float cellSize;
    public FruitGenerate fruitGenerateL, fruitGenerateR;

    public List<Fruit> fruitsL, fruitsR;


    private void Awake()
    {
        instance = this;
        left.width = width;
        left.height = height;
        left.cellSize = cellSize;
        right.width = width;
        right.height = height;
        right.cellSize = cellSize;
    }

    private void Update()
    {
        //果子位置冲突逻辑：果子生成时会提供一个占位给对面，当果子刷新时消除占位
        GeneratFruit(left, fruitGenerateL, fruitsL);
        GeneratFruit(right, fruitGenerateR, fruitsR);
    }

    #region GenerateFruit
    public bool RandomGenerateFruit(GridFactory gridFactory,out Fruit gridObject)
    {
        gridObject = null;
        int index = (int)(Random.value * gridFactory.emptyPosition.Count);
        var position = gridFactory.emptyPosition[index];
        gridFactory.SetGridObject(index, gridFactory.fruitPrefab, out gridObject);

        gridFactory.otherGridFactory.RemoveEmptyGridObject(position.x, position.y);
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

    
}
