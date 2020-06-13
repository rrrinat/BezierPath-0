using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPath : MonoBehaviour
{
    public List<Transform> points;

    [Range(0f, 1f)]
    public float step;

    private void Start()
    {

    }

    private void Update()
    {
        DrawLines();
        DrawCurve();
    }

    private void DrawLines()
    {
        var lastPoint = points[0].position;
        for (int i = 1; i < points.Count; i++)
        {
            var current = points[i].position;
            Debug.DrawLine(lastPoint, current, Color.blue);
            lastPoint = current;
        }
    }

    private void DrawCurve()
    {
        var drawPoints = new Queue<Vector3>();

        for (float i = 0; i < 1; i = i + step)
        {
            var point = Evaluate(points[0].position, points[1].position, points[2].position, points[3].position, i);
            drawPoints.Enqueue(point);
        }

        var lastPoint = drawPoints.Dequeue();
        while (drawPoints.Count > 0)
        {
            var current = drawPoints.Dequeue();
            Debug.DrawLine(lastPoint, current, Color.red);

            lastPoint = current;
        }
    }

    private Vector3 Evaluate(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float value)
    {
        var t = Mathf.Clamp01(value);
        var difference = 1f - t;

        return Mathf.Pow(difference, 3) * p1 + 3 * Mathf.Pow(difference, 2) * t * p2 +
                     3 * difference * t * t * p3 + t * t * t * p4;
    }

}
