using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour {
    [SerializeField] private GameObject lineIntersection;

    public void ShowObject(bool state) => lineIntersection.SetActive(state);
    
    private void Update() {
        Vector3 worldCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineIntersection.transform.position = worldCursorPosition;
    }
}
