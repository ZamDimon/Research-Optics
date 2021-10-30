using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDraw : MonoBehaviour {
    private const float INITIAL_Z_POSITION = 0;

    [SerializeField] private Field field;
    [SerializeField] private Sprite cellSprite;
    [SerializeField] private float maxBlackness;
    [SerializeField] private float minBlackness;

    private GameObject[,] cellObjects;

    private Vector2 GetExtrememRefractionCoefficients() {
        float maxValue = -1f, minValue = 1000000f;

        for (int x = 0; x < field.GetFieldSize().x; ++x) {
            for (int y = 0; y < field.GetFieldSize().y; ++y) {
                maxValue = Mathf.Max(maxValue, field.GetRefractionCoefficientCell(x, y));
                minValue = Mathf.Min(minValue, field.GetRefractionCoefficientCell(x, y));
            }
        }

        return new Vector2(minValue, maxValue);
    }

    private float GetColorValue(int x, int y) {
        float n = field.GetRefractionCoefficientCell(x, y), n_max = GetExtrememRefractionCoefficients().y, n_min = GetExtrememRefractionCoefficients().x;
        return (minBlackness * n_max - maxBlackness * n_min)/(n_max - n_min) + (maxBlackness - minBlackness) * n / (n_max - n_min);
    }

    public void Draw() {
        Erase();

        for (int x = 0; x < field.GetFieldSize().x; ++x) {
            for (int y = 0; y < field.GetFieldSize().y; ++y) {
                cellObjects[x, y] = new GameObject($"Cell ({x}, {y})");
                
                SpriteRenderer newCell_spriteRenderer = cellObjects[x, y].AddComponent<SpriteRenderer>();
                newCell_spriteRenderer.sprite = cellSprite;
                newCell_spriteRenderer.color = new Color(GetColorValue(x, y), GetColorValue(x, y), GetColorValue(x, y));
                newCell_spriteRenderer.drawMode = SpriteDrawMode.Sliced;
                newCell_spriteRenderer.size = new Vector2(field.GetCellSize(), field.GetCellSize());
                cellObjects[x, y].transform.position = new Vector3(field.GetCellSize() * x + field.GetCellSize()/2, field.GetCellSize() * y + field.GetCellSize()/2, INITIAL_Z_POSITION);
            }
        }
    }

    public void Erase() {
        for (int x = 0; x < field.GetFieldSize().x; ++x) {
            for (int y = 0; y < field.GetFieldSize().y; ++y) {
                if (cellObjects[x, y] != null)
                    Destroy(cellObjects[x, y]);
            }
        }

        cellObjects = new GameObject[field.GetFieldSize().x, field.GetFieldSize().y];
    }

    public void UpdateColor() {
        for (int x = 0; x < field.GetFieldSize().x; ++x) {
            for (int y = 0; y < field.GetFieldSize().y; ++y) {
                if (cellObjects[x, y] != null)
                    cellObjects[x, y].GetComponent<SpriteRenderer>().color = new Color(GetColorValue(x, y), GetColorValue(x, y), GetColorValue(x, y));
            }
        }
    }

    private void Start() {
        cellObjects = new GameObject[field.GetFieldSize().x, field.GetFieldSize().y];
    }
}
