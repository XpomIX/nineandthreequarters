using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class field : MonoBehaviour
{
    [SerializeField] private int _fieldSize;
    [SerializeField] private GameObject _fieldCellContainer;
    [SerializeField] private Color _primaryColor;
    [SerializeField] private Color _secondaryColor;
    
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
        Debug.Log(cellsPositions.Count);
        return cellsPositions;
    }

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = _primaryColor;
        float cellSize = GetCellSize();

        bool isEvenCell = true;
        foreach(Vector2 cellPosition in GetCellsPositions())
        {
            if(isEvenCell)
            {
                GameObject createdCellContainer = Instantiate(
                    _fieldCellContainer,
                    new Vector3(cellPosition.x, cellPosition.y, 0.5f),
                    Quaternion.identity,
                    gameObject.transform
                );
                createdCellContainer.transform.localScale = new Vector2(cellSize, cellSize);
                GameObject cell = createdCellContainer.transform.GetChild(0).gameObject;
                cell.GetComponent<SpriteRenderer>().color = _secondaryColor;
                isEvenCell = false;
            } else
            {
                isEvenCell = true;
            }
        }
        
    }
}
