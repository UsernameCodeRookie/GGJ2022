using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem {
    public class GridFactory : MonoBehaviour
    {
        [Header("Settings")]
        public int width;
        public int height;
        public float cellSize;
        public Transform origin;
        public GridFactory otherGridFactory;
        public bool LeftOrRight;
        [Header("Prefabs")]
        public GameObject wallPrefab;
        public GameObject playerPrefab;
        public GameObject fruitPrefab;
        private bool[,] gridArray;



        private void Start()
        {
            gridArray = new bool[width, height];

            SetGridObject(0, 0, wallPrefab);

            SetGridObject(width / 2, height / 2, playerPrefab);

            //SetGridObject(0, 0, wallPrefab);
        }

        private void Update()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white);

        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + origin.position;
        }

        public void SetGridObject(int x, int y, GameObject prefab, bool cover = false)
        {
            if (gridArray[x, y] == false)
            {
                GameObject o = Instantiate(prefab, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, Quaternion.identity, origin);
                o.transform.localScale = Vector3.one * cellSize;
                GridObject gridObject = o.GetComponent<GridObject>();
                gridObject.x = x;
                gridObject.y = y;
                gridObject.LeftOrRight = LeftOrRight;

                gridArray[x, y] = cover;
            }
            else
            {
                Debug.LogError("There have been a gridObject.");
            }
        }

        public bool GetGridObject(int x, int y)
        {
            return gridArray[x, y];
        }
    }
}
