using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Transform cameraTransform;
    [SerializeField] private float speed;
    [SerializeField] private float scrollSpeed;

    private void Start() {
        cameraTransform = transform;
    }

    private void Update() {
        cameraTransform.position += new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0);
        cameraTransform.GetComponent<Camera>().orthographicSize += (-1) * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    }
}
