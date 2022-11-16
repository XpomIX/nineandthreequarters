using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject _snakePart;

    private field field;
    private control control;
    [SerializeField] private float _snakeSpeed = 0.2f;
    private Coroutine Coroutine;
    private Vector2Int headPosition;
    private Direction forbiddenDirection = Direction.Down;

    private List<GameObject> snake = new List<GameObject>();
    private List<Vector2Int> snakeCellPosition = new List<Vector2Int>();

    public Direction GetForbiddenDirection()
    {
        return forbiddenDirection;
    }

    private Vector2Int NormalizeCellPosition(Vector2Int cellPosition)
    {
        if (cellPosition.x < 0) { cellPosition = new Vector2Int(field._fieldSize - 1, cellPosition.y); }
        if (cellPosition.x > field._fieldSize - 1) { cellPosition = new Vector2Int(0, cellPosition.y); }
        if (cellPosition.y < 0) { cellPosition = new Vector2Int(cellPosition.x, field._fieldSize - 1); }
        if (cellPosition.y > field._fieldSize - 1) { cellPosition = new Vector2Int(cellPosition.x, 0); }
        return cellPosition;
    }
    private void GetNextHeadPosition()
    {
        Direction snakeDirection = control.GetDirection();
        if (snakeDirection == Direction.Up)
        {
            forbiddenDirection = Direction.Down;
            headPosition = new Vector2Int(headPosition.x, headPosition.y + 1);
        }
        if (snakeDirection == Direction.Down)
        {
            forbiddenDirection = Direction.Up;
            headPosition = new Vector2Int(headPosition.x, headPosition.y - 1);
        }
        if (snakeDirection == Direction.Left)
        {
            forbiddenDirection = Direction.Right;
            headPosition = new Vector2Int(headPosition.x - 1, headPosition.y);
        }
        if (snakeDirection == Direction.Right)
        {
            forbiddenDirection = Direction.Left;
            headPosition = new Vector2Int(headPosition.x + 1, headPosition.y);
        }
        headPosition = NormalizeCellPosition(headPosition);
        snakeCellPosition[0] = headPosition;
    }

    private void CreateSnakeCell(int x = -1, int y = -1)
    {
        if (x == -1)
        {
            x = snakeCellPosition[snakeCellPosition.Count - 1].x;
            y = snakeCellPosition[snakeCellPosition.Count - 1].y - 1;
        }
        Vector2Int cellPosition = NormalizeCellPosition(new Vector2Int(x, y));
        snakeCellPosition.Add(cellPosition);
        GameObject body = Instantiate(_snakePart, field.GetCellPosition(cellPosition.x, cellPosition.y), Quaternion.identity, transform);
        body.transform.localScale = new Vector2(field.GetCellSize(), field.GetCellSize());
        snake.Add(body);
    }
    private void Start()
    {
        control = gameObject.GetComponent<control>();

        field = gameObject.GetComponent<field>();
        headPosition = new Vector2Int(field._fieldSize / 2, field._fieldSize / 2);
        GameObject head = Instantiate(_snakePart, field.GetCellPosition(headPosition.x, headPosition.y), Quaternion.identity,transform);
        head.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        head.transform.localScale = new Vector2(field.GetCellSize(), field.GetCellSize());
        snake.Add(head);
        snakeCellPosition.Add(headPosition);
        for (int tailLength = 18; tailLength > 0;tailLength--)
        {
            CreateSnakeCell();
        }
        Coroutine = StartCoroutine("SnakeStep");
    }

    private void Die()
    {
        StopCoroutine(Coroutine);
    }

    IEnumerator SnakeStep()
    {
        while (true)
        {
            GetNextHeadPosition();
            for(int i = snake.Count - 1; i > 0; i--)
            {
                snake[i].transform.position = snake[i - 1].transform.position;
                snakeCellPosition[i] = snakeCellPosition[i - 1];
            }
            snake[0].transform.position = field.GetCellPosition(headPosition.x, headPosition.y);

            yield return new WaitForSeconds(_snakeSpeed);
        }
    }
}
