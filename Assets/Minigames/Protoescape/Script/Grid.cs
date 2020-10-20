using System;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class Grid<TileObject>
    {
        int _column;
        int _row;
        float _cellSize;
        TileObject[,] _gridArray;

        public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
        public class OnGridValueChangedEventArgs : EventArgs
        {
            public int Column;
            public int Row;
        }

        public void TriggerGridObjectChanged(int column, int row)
        {
            OnGridValueChanged?.Invoke(this, new OnGridValueChangedEventArgs { Column = column, Row = row });
        }

        public Grid(int column, int row, float cellSize, Func<Grid<TileObject>, int, int, TileObject> createdGridObject)
        {
            _column = column;
            _row = row;
            _cellSize = cellSize;

            _gridArray = new TileObject[column, row];

            for (int c = 0; c < column; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    _gridArray[c, r] = createdGridObject(this, c, r);
                }
            }
        }
        public int GetWidth()
        {
            return _column;
        }

        public int GetHeight()
        {
            return _row;
        }

        public float GetCellSize()
        {
            return _cellSize;
        }

        //public IEnumerable<TileObject> GetAllGridObjects()
        //{
        //    for (int col = 0; col < _column; col++)
        //    {
        //        for (int row = 0; row < _row; row++)
        //        {
        //            yield return _gridArray[col, row];
        //        }
        //    }
        //}

        //public IEnumerable<TileObject> GetGridObjectsAtColumn(int column)
        //{
        //    for (int col = 0; col < _column; col++)
        //    {
        //        for (int row = 0; row < _row; row++)
        //        {
        //            if (col == column)
        //                yield return _gridArray[col, row];
        //            else
        //                continue;
        //        }
        //    }
        //}

        //public IEnumerable<TileObject> GetGridObjectsAtRow(int row)
        //{
        //    for (int col = 0; col < _column; col++)
        //    {
        //        for (int r = 0; r < _row; r++)
        //        {
        //            if (r == row)
        //                yield return _gridArray[col, r];
        //            else
        //                continue;
        //        }
        //    }
        //}

        public void SetGridObject(Vector2 worldPosition, TileObject value)
        {
            SetGridObject((int)GetXY(worldPosition).x, (int)GetXY(worldPosition).y, value);
        }

        public void SetGridObject(int column, int row, TileObject value)
        {
            if (column >= 0 && row >= 0 && column < _column && row < _row)
            {
                _gridArray[column, row] = value;
                TriggerGridObjectChanged(column, row);
            }
        }

        public Vector2 GetWorldPosition(int column, int row)
        {
            return new Vector2(column, row) * _cellSize;
        }

        public TileObject GetGridObject(Vector2 worldPosition)
        {
            return GetGridObject((int)GetXY(worldPosition).x, (int)GetXY(worldPosition).y);
        }

        public TileObject GetGridObject(int column, int row)
        {
            if (column >= 0 && row >= 0 && column < _column && row < _row)
            {
                return _gridArray[column, row];
            }
            return default;
        }

        //public void ShuffleGridObjects()
        //{
        //    var tileList = GetAllGridObjects().ToList();

        //    for (int col = 0; col < _column; col++)
        //    {
        //        for (int row = 0; row < _row; row++)
        //        {
        //            int random = UnityEngine.Random.Range(0, tileList.Count);

        //            TileObject tile = tileList[random];
        //            SetGridObject(col, row, tile);
        //            tileList.Remove(tile);
        //        }
        //    }
        //}

        private Vector2 GetXY(Vector2 worldPosition)
        {
            return new Vector2(Mathf.FloorToInt(worldPosition.x / _cellSize), Mathf.FloorToInt(worldPosition.y / _cellSize));
        }
    }
}