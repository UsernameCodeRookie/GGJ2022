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
        if (Input.GetMouseButtonDown(0))
        {
            if(!RandomGenerateFruit())
                RandomGenerateFruit();
        }
    }

    public bool RandomGenerateFruit()
    {
        int positionL = (int)(Random.value * width * height);
        int positionR = (int)(Random.value * width * height);
        Debug.Log(positionL);
        Debug.Log(positionR);
        if (positionL == positionR) return false;
        if (left.GetGridObject(positionL % width, positionL / width) | right.GetGridObject(positionR % width, positionR / width)) return false;

        left.SetGridObject(positionL % width, positionL / width, left.fruitPrefab);
        right.SetGridObject(positionR % width, positionR / width, right.fruitPrefab);
        return true;
    }
}
