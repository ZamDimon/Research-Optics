using System;
using UnityEngine;
using TMPro;

public class RefractiveIndexText : MonoBehaviour {
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject boxObject;
    [SerializeField] private Vector3 boxOffset;
    [SerializeField] private Field field;

    private string GetRefractiveIndexText(Vector3 position) => $"n = {Math.Round(field.GetRefractionCoefficient(position.x, position.y), 3)}";

    private void Update() {
        Vector3 worldCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        boxObject.transform.position = new Vector3(worldCursorPosition.x, worldCursorPosition.y, boxObject.transform.position.z) + boxOffset;
        textObject.GetComponent<TextMeshProUGUI>().text = GetRefractiveIndexText(worldCursorPosition);
    }
}
