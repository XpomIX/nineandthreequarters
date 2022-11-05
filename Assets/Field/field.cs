using UnityEngine;

public class field : MonoBehaviour
{
    [SerializeField] private int _cellsLength;
    [SerializeField] private GameObject _fieldCellContainer;
    [SerializeField] private Color _primaryColor;
    [SerializeField] private Color _secondaryColor;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = _primaryColor;
        Transform transform = gameObject.transform;
        float squareSide = transform.localScale.x;
        float cellSize = squareSide / _cellsLength;

        Vector2 parentScale = gameObject.transform.parent.transform.localScale;

        Vector2 leftTopCornerPosition = new Vector2(
            transform.position.x - parentScale.x / 2,
            transform.position.y - parentScale.y / 2
        );
        bool renderingEvenRow = false;

        for (int i = 0; i < _cellsLength; i++)
        {
            renderingEvenRow = !renderingEvenRow;
            for (int j = renderingEvenRow ? 0 : 1; j < _cellsLength; j = j + 2)
            {
                GameObject createdCellContainer = Instantiate(
                    _fieldCellContainer,
                    new Vector3(leftTopCornerPosition.x + cellSize * j * parentScale.x, leftTopCornerPosition.y + cellSize * (i + 1) * parentScale.y, 0.5f),
                    Quaternion.identity,
                    gameObject.transform
                );
                createdCellContainer.transform.localScale = new Vector2(cellSize, cellSize);
                GameObject cell = createdCellContainer.transform.GetChild(0).gameObject;
                cell.GetComponent<SpriteRenderer>().color = _secondaryColor;
            }
        }
    }
}
