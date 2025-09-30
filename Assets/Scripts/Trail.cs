using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SmoothSnakeTail : MonoBehaviour
{
    public Transform head;             // Головна точка (голова змійки)
    public float minDistance = 0.1f;   // Відстань між "ключовими" точками хвоста
    public int smoothness = 10;        // Кількість інтерпольованих точок між вузлами
    public int maxLength = 20;         // Максимальна довжина (кількість сегментів)

    private LineRenderer line;
    private List<Vector3> nodes = new List<Vector3>();

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        nodes.Add(head.position);
    }

    void Update()
    {
        // додаємо нову точку якщо голова віддалилась достатньо
        if (Vector3.Distance(nodes[nodes.Count - 1], head.position) > minDistance)
        {
            nodes.Add(head.position);

            // обмежуємо довжину хвоста
            if (nodes.Count > maxLength)
                nodes.RemoveAt(0);
        }

        // створюємо інтерпольовану криву
        List<Vector3> smoothPoints = GenerateSmoothPoints(nodes);

        // малюємо лінію
        line.positionCount = smoothPoints.Count;
        line.SetPositions(smoothPoints.ToArray());
    }

    /// <summary>
    /// Генерує Catmull-Rom криву по заданих вузлах
    /// </summary>
    private List<Vector3> GenerateSmoothPoints(List<Vector3> points)
    {
        List<Vector3> smoothPoints = new List<Vector3>();

        if (points.Count < 2)
            return points;

        for (int i = 0; i < points.Count - 1; i++)
        {
            // сусідні точки для Catmull-Rom
            Vector3 p0 = i == 0 ? points[i] : points[i - 1];
            Vector3 p1 = points[i];
            Vector3 p2 = points[i + 1];
            Vector3 p3 = i + 2 < points.Count ? points[i + 2] : points[i + 1];

            for (int j = 0; j < smoothness; j++)
            {
                float t = j / (float)smoothness;
                Vector3 newPoint = CatmullRom(p0, p1, p2, p3, t);
                smoothPoints.Add(newPoint);
            }
        }

        // додаємо останню точку
        smoothPoints.Add(points[points.Count - 1]);

        return smoothPoints;
    }

    /// <summary>
    /// Формула Catmull-Rom інтерполяції
    /// </summary>
    private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t
        );
    }

    /// <summary>
    /// Збільшити довжину хвоста
    /// </summary>
    public void Grow(int amount = 5)
    {
        maxLength += amount;
    }
}