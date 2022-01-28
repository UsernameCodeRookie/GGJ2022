using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class EmptyPosition
    {
        private int width, height;
        private List<Vector2Int> _emptyPosition;
        private bool[,] _positionState;

        public EmptyPosition(int width, int height)
        {
            this.width = width;
            this.height = height;
            _positionState = new bool[width, height];
            _emptyPosition = new List<Vector2Int>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _positionState[x, y] = false;
                    _emptyPosition.Add(new Vector2Int(x, y));
                }
            }
        }

        public void AddEmptyPosition(int x, int y)
        {
            if (!LegalPosition(x, y)) return;
            _emptyPosition.Add(new Vector2Int(x, y));
            _positionState[x, y] = true;
        }

        public void RemoveEmptyPosition(int x, int y)
        {
            if (!LegalPosition(x, y) || !HavePosition(x, y)) return;
            _emptyPosition.Remove(new Vector2Int(x, y));
            _positionState[x, y] = true;
        }

        public Vector2Int ChooseRandomPosition()
        {
            int randomPositionIndex = (int)(UnityEngine.Random.value * _emptyPosition.Count);
            return _emptyPosition[randomPositionIndex];
        }

        public bool HavePosition(int x, int y)
        {
            if (_positionState[x, y] == false)
                return false;
            else
                return true;
        }

        public bool LegalPosition(int x, int y)
        {
            if (x > width || y > height)
                return false;
            else
                return true;
        }

        public bool IsEmpty()
        {
            return _emptyPosition.Count <= 0;
        }
    }
}
