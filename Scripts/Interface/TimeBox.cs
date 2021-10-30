using System;
using UnityEngine;
using TMPro;

public class TimeBox : MonoBehaviour {
    [SerializeField] private GameObject textObject;

    private string GetCurrentTime() => $"Time: {Math.Round(Time.time, 3)}";
    private void Update() => textObject.GetComponent<TextMeshProUGUI>().text = GetCurrentTime();
}
