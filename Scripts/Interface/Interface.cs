using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour {
    [SerializeField] private FieldDraw fieldDraw;
    [SerializeField] private Field field;
    [SerializeField] private GameObject pointerObject;
    [SerializeField] private Sprite defaultSprite;

    public void DrawField() {
        fieldDraw.Draw();
        fieldDraw.UpdateColor();
    }
    public void EraseField() => fieldDraw.Erase();

    private void CreatePoint(float x, float y) {
        GameObject point = new GameObject();
                
        SpriteRenderer spriteRenderer = point.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
        spriteRenderer.color = Color.green;
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = new Vector2(0.1f, 0.1f);
        spriteRenderer.sortingOrder = 10;
        point.transform.position = new Vector3(x, y, 10f);
    }

    private void Start() => DrawField();
}
