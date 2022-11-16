using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class control : MonoBehaviour
{
    private Direction direction = Direction.Up;
    private Snake snakeScript;

    public Direction GetDirection()
    {
        return direction;
    }
    private void Start()
    {
        snakeScript = GetComponent<Snake>();
    }
    private void Update()
    {
        Direction forbiddenDirection = snakeScript.GetForbiddenDirection();
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (forbiddenDirection != Direction.Up)
            {
                direction = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (forbiddenDirection != Direction.Left)
            {
                direction = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (forbiddenDirection != Direction.Down)
            {
                direction = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (forbiddenDirection != Direction.Right)
            {
                direction = Direction.Right;
            }
        }
    }
}
