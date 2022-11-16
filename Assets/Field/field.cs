using System;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] public int _fieldSize;
    [SerializeField] private GameObject _fieldCellContainer;
    [SerializeField] private Color _primaryColor;
    [SerializeField] private Color _secondaryColor;

    public List<Vector2> cellPositions;

    public float GetCellSize()
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
        return cellsPositions;
    }

    public Vector2 GetCellPosition(int x, int y) { return cellPositions[y * _fieldSize + x]; }

    private void Awake()
    {
        cellPositions = GetCellsPositions();
        gameObject.GetComponent<SpriteRenderer>().color = _primaryColor;
        float cellSize = GetCellSize();

        for(int i = 0; i < cellPositions.Count; i++)
        {
            int row = i / _fieldSize + 1;
            int col = i % _fieldSize;

            if ((row % 2 == 0) && (col % 2 == 0)) continue;
            if ((row % 2 == 1) && (col % 2 == 1)) continue;
            GameObject createdCellContainer = Instantiate(
                    _fieldCellContainer,
                    new Vector3(cellPositions[i].x, cellPositions[i].y, 0.5f),
                    Quaternion.identity,
                    gameObject.transform
                );
            createdCellContainer.transform.localScale = new Vector2(cellSize, cellSize);
            GameObject cell = createdCellContainer.transform.GetChild(0).gameObject;
            cell.GetComponent<SpriteRenderer>().color = _secondaryColor;
        }
    }
}
