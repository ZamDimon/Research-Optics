using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormulaLine : MonoBehaviour {
    private const float LINE_WIDTH = 0.05f;
    private const float STEP = 0.001f;
    private const int SORTING_LAYER = 10;

    public delegate float Function(float x, float t);

    private Function formula;
    private Field field;
    private Vector2 interval;

    private LineRenderer lineRenderer;

    public FormulaLine(Field field, Function formula, Vector2 interval, Color lineColor) {
        this.formula = formula;
        this.field = field;
        this.interval = interval;

        GameObject rayObject = new GameObject("Ray object");
        rayObject.transform.position = new Vector3(0, 0, 0);
        lineRenderer = rayObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.sortingOrder = SORTING_LAYER;

        lineRenderer.startWidth = LINE_WIDTH;
        lineRenderer.endWidth = LINE_WIDTH;
        UpdateLine();
    }

    public void Erase() => lineRenderer.positionCount = 0;

    public void UpdateLine() {
        Erase();
        lineRenderer.positionCount = (int)((interval.y - interval.x)/STEP);

        for (int i = 0; i < lineRenderer.positionCount; ++i) {
            float x = interval.x + i * STEP;
            lineRenderer.SetPosition(i, new Vector2(x, formula(x * field.GetSIFactor(), Time.time) / field.GetSIFactor()));
        }
    }

    public float GetYCoordinate(float x) => formula(x * field.GetSIFactor(), Time.time) / field.GetSIFactor();
}
