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

        /// <summary>
        /// ´ýÐÞ¸Ä
        /// </summary>
        private bool[,] gridArray;
        public List<Vector2Int> emptyPosition;



        private void Start()
        {
            gridArray = new bool[width, height];

            SetGridObject(width / 2, height / 2, playerPrefab);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    emptyPosition.Add(new Vector2Int(x, y));
                }
            }
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

        #region SetGridObject

        public void SetGridObject(int x, int y, GameObject prefab, bool cover = false)
        {
            if (gridArray[x, y] == false | cover == true)
            {
                GameObject o = Instantiate(prefab, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, Quaternion.identity, origin);
                o.transform.localScale = Vector3.one * cellSize;

                GridObject gridObject = o.GetComponent<GridObject>();
                if (gridObject != null)
                {
                    gridObject.x = x;
                    gridObject.y = y;
                    gridObject.LeftOrRight = LeftOrRight;
                    gridArray[x, y] = true;
                }
            }
            else
            {
                Debug.LogError("There have been a gridObject.");
            }
        }

        public void SetGridObject<T>(int index, GameObject prefab, out T gridObject) where T:GridObject
        {
            var vector2Int = emptyPosition[index];
            int x = vector2Int.x;
            int y = vector2Int.y;
            if (gridArray[x, y] == false)
            {
                emptyPosition.RemoveAt(index);
                gridArray[x, y] = true;

                GameObject o = Instantiate(prefab, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, Quaternion.identity, origin);
                o.transform.localScale = Vector3.one * cellSize;

                gridObject = o.GetComponent<T>();
                if (gridObject != null)
                {
                    gridObject.x = x;
                    gridObject.y = y;
                    gridObject.LeftOrRight = LeftOrRight;
                }
            }
            else
            {
                gridObject = null;
                Debug.LogError("There have been a gridObject.");
            }
        }

        #endregion

        public void SetEmptyGridObject(int x,int y)
        {
            emptyPosition.Add(new Vector2Int(x, y));
            gridArray[x, y] = false;
        }

        public void RemoveEmptyGridObject(int x,int y)
        {
            emptyPosition.Remove(new Vector2Int(x, y));
            gridArray[x, y] = true;
        }

        public bool GetGridObject(int x, int y)
        {
            return gridArray[x, y];
        }
    }
}
