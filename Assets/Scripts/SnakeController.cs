using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public static SnakeController Instance;

    public int fieldWidth;
    public int fieldHeight;
    public List<SnakeMovPart> snakeParts;
    public TrailRenderer trailRenderer;
    public float moveSpeed;

    private Vector2 direction = Vector2.right;
    private Vector2 nextDirection = Vector2.right;
    private Vector2 lastDirection = Vector2.right;
    private Queue<Vector2> directionQueue = new Queue<Vector2>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        direction = Vector2.right;
        nextDirection = Vector2.right;
        StartCoroutine(MoveSnake());
        trailRenderer.time = moveSpeed * (snakeParts.Count);
    }

    private IEnumerator MoveSnake()
    {
        while (true)
        {
            // ÏÐÎÂÅÐÊÀ ÏÀÓÇÛ
            //if (GameManager.Instance != null && GameManager.Instance.IsGamePaused())
           // {
           //     yield return new WaitForSeconds(0.1f);
            //    continue;
            //}

            // Îñòàëüíîé êîä áåç èçìåíåíèé...
            if (directionQueue.Count > 0)
            {
                direction = directionQueue.Dequeue();
            }

            foreach (var part in snakeParts)
            {
                part.lastPos = part.transform.position;
            }

            Vector3 newPos = new Vector3(
                Mathf.Round(snakeParts[0].transform.position.x + direction.x),
                Mathf.Round(snakeParts[0].transform.position.y + direction.y),
                snakeParts[0].transform.position.z
            );

            snakeParts[0].transform.DOMove(newPos, moveSpeed).SetEase(Ease.Linear);

            for (int i = 1; i < snakeParts.Count; i++)
            {
                snakeParts[i].transform.DOKill();
                snakeParts[i].transform.DOMove(snakeParts[i - 1].lastPos, moveSpeed).SetEase(Ease.Linear);
            }

            float targetAngle = GetAngleFromDirection(direction);
            snakeParts[0].transform.DORotateQuaternion(
                Quaternion.Euler(0f, 0f, targetAngle),
                moveSpeed * 0.3f
            );

            lastDirection = direction;

            yield return new WaitForSeconds(moveSpeed);
        }
    }

    private void Update()
    {
        // ÏÐÎÂÅÐÊÀ ÏÀÓÇÛ
        if (GameManager.Instance != null && GameManager.Instance.IsGamePaused())
            return;

        if (directionQueue.Count > 1) return;

        if (Input.GetKeyDown(KeyCode.W) && lastDirection != Vector2.down)
            directionQueue.Enqueue(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S) && lastDirection != Vector2.up)
            directionQueue.Enqueue(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.A) && lastDirection != Vector2.right)
            directionQueue.Enqueue(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.D) && lastDirection != Vector2.left)
            directionQueue.Enqueue(Vector2.right);
    }

    private float GetAngleFromDirection(Vector2 dir)
    {
        if (dir == Vector2.up) return 0f;
        if (dir == Vector2.right) return -90f;
        if (dir == Vector2.down) return 180f;
        if (dir == Vector2.left) return 90f;
        return 0f;
    }

    public void Grow()
    {
        Vector3 newPartPos = snakeParts[snakeParts.Count - 1].lastPos;
        var newPart = Instantiate(snakeParts[snakeParts.Count - 1], newPartPos, Quaternion.identity);
        newPart.gameObject.name = "SnakePart";
        snakeParts.Add(newPart);
        trailRenderer.time = moveSpeed * (snakeParts.Count);
    }
}