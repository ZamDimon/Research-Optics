using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    private const float DERIVATIVE_STEP = 0.001f;

    private delegate float Function(float x, float y, float t);

    #region Field settings
    [SerializeField] private Vector2 fieldSize;
    [SerializeField] private float SIFactor;
    [SerializeField] private float cellSize;
    #endregion

    #region Refraction coefficient settings
    private Function formula;
    private float[,] refractionCoefficients;
    #endregion 

    public float GetSIFactor() => SIFactor;
    public float GetRefractionCoefficientCell (int x, int y) => refractionCoefficients[x, y];
    public float GetRefractionCoefficient(float x, float y) => formula(x * SIFactor, y * SIFactor, Time.time); 
    public Vector2 GetGradient(float x, float y) {
        float partial_x = (GetRefractionCoefficient(x + DERIVATIVE_STEP, y) - GetRefractionCoefficient(x, y))/(SIFactor * DERIVATIVE_STEP);
        float partial_y = (GetRefractionCoefficient(x, y + DERIVATIVE_STEP) - GetRefractionCoefficient(x, y))/(SIFactor * DERIVATIVE_STEP);

        return new Vector2(partial_x, partial_y);
    }

    public Vector2Int GetFieldSize() {
        int _sizeX = (int)(fieldSize.x / cellSize);
        int _sizeY = (int)(fieldSize.y / cellSize);
        return new Vector2Int(_sizeX, _sizeY);    
    }

    //Original dependence: (1f + 2f*Mathf.Exp(Mathf.Cos(t*(x*y)/2f))/(1 + y + 0.5f*Mathf.Cos(t)) + 2f * Mathf.Cos(x*y) + 1f * Mathf.Sin((x+y)*t/3f))

    private void Start() {
        refractionCoefficients = new float[GetFieldSize().x, GetFieldSize().y];
        formula = (x, y, t) => (1f + 2f*Mathf.Exp(Mathf.Cos(t*(x*y)/2f))/(1 + y + 0.5f*Mathf.Cos(t)) + 2f * Mathf.Cos(x*y) + 1f * Mathf.Sin((x+y)*t/3f));
    }

    public float GetCellSize() => cellSize;
    public bool IsInField(Vector2 point) => (point.x < fieldSize.x && point.x > 0f && point.y < fieldSize.y && point.y > 0);
    
    private void UpdateRefractionState(Function nFunction) {
        for (int x = 0; x < GetFieldSize().x; ++x) {
            for (int y = 0; y < GetFieldSize().y; ++y) {
                refractionCoefficients[x, y] = nFunction(x * cellSize * SIFactor, y * cellSize * SIFactor, Time.time);
            }
        }
    }

    private void Update() {
        UpdateRefractionState(formula);
        transform.GetComponent<FieldDraw>().UpdateColor();
    }
}
