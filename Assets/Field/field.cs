using System;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private GameObject _fieldCell;
    [SerializeField] private Color _primaryColor;
    [SerializeField] private Color _secondaryColor;

    private float GetCellSize()
    {
        return 1f / _size;
    }
    private Vector2 NormalizeCellPosition(float x, float y)
    {
        float cellSize = GetCellSize();
        return new Vector2((x + cellSize / 2) * transform.parent.localScale.x, (y - cellSize / 2) * transform.parent.localScale.y);
    }
    public List<Vector2> GetCellPositions()
    {
        List<Vector2> cellPositions = new List<Vector2>();

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                cellPositions.Add(NormalizeCellPosition(-0.5f + (float)j / _size, 0.5f - (float)i / _size));
            }
        }
        return cellPositions;
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = _primaryColor;
        float cellSize = GetCellSize();
        List<Vector2> cellPositions = GetCellPositions();
        int cellsCount = cellPositions.Count;
        for (int i = 0; i < cellsCount; i++)
        {
            int row = i / _size + 1;
            int col = i % _size;

            if ((row % 2 == 0) && (col % 2 == 0)) continue;
            if ((row % 2 == 1) && (col % 2 == 1)) continue;

            Vector2 cellPosition = cellPositions[i];
            GameObject createdCell = Instantiate(_fieldCell, cellPosition, Quaternion.identity, transform);
            createdCell.GetComponent<SpriteRenderer>().color = _secondaryColor;
            createdCell.transform.localScale = new Vector3(cellSize, cellSize, 1);
        }
    }
}
