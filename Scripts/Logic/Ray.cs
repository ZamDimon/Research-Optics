using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour {
    private const int ITERATION_NUMBER = 1000;
    private const int SORTING_LAYER = 10;
    private float STEP = 0.1f;
    private const float LINE_WIDTH = 0.05f;

    #region Initial properties
    private Field field;
    private Vector2 initialPosition;
    private Vector2 initialDirection;
    private float initialAngle;
    #endregion

    #region Line renderer settings
    private LineRenderer lineRenderer;
    #endregion

    public Ray(Field field, Vector2 initialPosition, float initialAngle, Color lineColor) { 
        this.field = field;
        this.initialAngle = initialAngle;
        this.initialPosition = initialPosition;
        this.initialDirection = new Vector2(STEP * Mathf.Cos(initialAngle), STEP * Mathf.Sin(initialAngle));

        GameObject rayObject = new GameObject("Ray object");
        rayObject.transform.position = new Vector3(0, 0, 0);
        lineRenderer = rayObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = LINE_WIDTH;
        lineRenderer.endWidth = LINE_WIDTH;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.sortingOrder = SORTING_LAYER;

        UpdateRay();
    }

    public void Erase() => lineRenderer.positionCount = 0;

    public void UpdateRay() {
        Erase();
        lineRenderer.positionCount = ITERATION_NUMBER;

        Vector2 currentPosition = initialPosition;
        Vector2 currentDirection = initialDirection;

        for (int i = 0; i < ITERATION_NUMBER; ++i) {
            lineRenderer.SetPosition(i, currentPosition);
            
            Vector2 gradient = field.GetGradient(currentPosition.x, currentPosition.y);
            float scalarProduct = Vector2.Dot(gradient, currentDirection);
            Vector2 parallelDirection = ((gradient.magnitude == 0)? Vector2.zero : gradient.normalized * scalarProduct / gradient.magnitude);
            Vector2 perpendicularDirection = currentDirection - parallelDirection;

            Vector2 newPosition = currentPosition + currentDirection;
            Vector2 newPerpendicularDirection = (field.GetRefractionCoefficient(currentPosition.x, currentPosition.y) / field.GetRefractionCoefficient(newPosition.x, newPosition.y)) * perpendicularDirection;
            Vector2 newParallelDirection = (currentDirection.magnitude < newPerpendicularDirection.magnitude)? Vector2.zero : Mathf.Sqrt(currentDirection.magnitude * currentDirection.magnitude - newPerpendicularDirection.magnitude * newPerpendicularDirection.magnitude) * gradient.normalized * Mathf.Sign(scalarProduct);

            currentPosition += currentDirection;
            currentDirection = newPerpendicularDirection + newParallelDirection;

            if (!field.IsInField(currentPosition))  {
                lineRenderer.positionCount = i;
                break;
            }
        }
    }
}
