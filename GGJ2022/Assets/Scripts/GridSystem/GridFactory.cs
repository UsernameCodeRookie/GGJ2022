using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem {
    public class GridFactory : MonoBehaviour
    {
        [Header("Settings")]
        private int width;
        private int height;
        private float cellSize;
        public Transform rootPosition;
        public Transform origin;
        [HideInInspector]
        //public GridFactory otherGridFactory;
        public bool LeftOrRight;
        [Header("Prefabs")]
        public GameObject wallPrefab;
        public GameObject playerPrefab;
        public GameObject fruitPrefab;

        private EmptyPosition emptyPosition;

        private FruitManager fruitManager;

        private void Awake()
        {
            fruitManager = GetComponent<FruitManager>();
            Initialize();
        }

        private void Update()
        {
            DrawDebugLine();
        }

        private void DrawDebugLine()
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
            return new Vector3(x, y) * cellSize + rootPosition.position;
        }

        #region SetGridObject

        public T SetGridObject<T>(int x, int y, GameObject prefab) where T : GridObject
        {
            T gridObject = InstantiateOnGrid(x, y, prefab).GetComponent<T>();
            gridObject.Init(x, y, LeftOrRight, cellSize);
            return gridObject;
        }

        public Fruit RandomGenerateFruit()
        {
            if (emptyPosition.IsEmpty()) return null;
            var position = emptyPosition.ChooseRandomPosition();
            Fruit fruit = SetGridObject<Fruit>(position.x, position.y, fruitPrefab);
            fruit.SetFruitManager(fruitManager);
            RemoveEmptyPosition(position.x, position.y);
            return fruit;
        }

        public void AddEmptyPostion(int x, int y)
        {
            emptyPosition.AddEmptyPosition(x, y);
        }

        public void RemoveEmptyPosition(int x,int y)
        {
            emptyPosition.RemoveEmptyPosition(x, y);
        }

        public GameObject InstantiateOnGrid(int x, int y, GameObject prefab)
        {
            return Instantiate(prefab, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, Quaternion.identity, origin);
        }

        #endregion

        public void Initialize()
        {

            GameManager gameManager = GameManager.instance;
            this.width = gameManager.width;
            this.height = gameManager.height;
            this.cellSize = gameManager.cellSize;

            InstantiateOnGrid(width / 2, height / 2, playerPrefab).transform.localScale = Vector3.one * cellSize;

            emptyPosition = new EmptyPosition(width, height);

            #region Boundary

            GameObject b = LeftOrRight ? Boundary.instance.BoundaryL : Boundary.instance.BoundaryR;

            for (int x = -1; x <= width; x++)
            {
                GameObject o = Instantiate(b, GetWorldPosition(x, -1) + new Vector3(cellSize, cellSize) * 0.5f, Quaternion.identity, Boundary.instance.transform);
                o.transform.localScale = Vector3.one * cellSize;

                GameObject t = Instantiate(b, GetWorldPosition(x, height) + new Vector3(cellSize, cellSize) * 0.5f, Quaternion.identity, Boundary.instance.transform);
                t.transform.localScale = Vector3.one * cellSize;
            }

            for (int y = 0; y < height; y++)
            {
                GameObject o = Instantiate(b, GetWorldPosition(-1, y) + new Vector3(cellSize, cellSize) * 0.5f, Quaternion.identity, Boundary.instance.transform);
                o.transform.localScale = Vector3.one * cellSize;

                GameObject t = Instantiate(b, GetWorldPosition(width, y) + new Vector3(cellSize, cellSize) * 0.5f, Quaternion.identity, Boundary.instance.transform);
                t.transform.localScale = Vector3.one * cellSize;
            }

            #endregion
        }
    }
}
