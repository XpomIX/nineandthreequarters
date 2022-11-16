using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class field : MonoBehaviour
{
    [SerializeField] public int _fieldSize;
    [SerializeField] private GameObject _fieldCellContainer;
    [SerializeField] private Color _primaryColor;
    [SerializeField] private Color _secondaryColor;

    public List<Vector2> cellPositions;

    public float GetCellSize()
    {
        return transform.localScale.x / _fieldSize;
    }
    public List<Vector2> GetCellsPositions()
    {
        float cellSize = GetCellSize();
        Vector2 parentScale = transform.parent.transform.localScale;
        Vector2 leftTopCornerPosition = new Vector2(
            transform.position.x - parentScale.x / 2,
            transform.position.y - parentScale.y / 2
        );
        List<Vector2> cellsPositions = new List<Vector2>();
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                cellsPositions.Add(new Vector2(leftTopCornerPosition.x + cellSize * j * parentScale.x, leftTopCornerPosition.y + cellSize * (i + 1) * parentScale.y));
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
