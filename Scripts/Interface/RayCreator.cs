using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCreator : MonoBehaviour
{
    [System.Serializable]
    private struct RayProperties {
        [SerializeField] public Vector2 initialCoordinates;
        [SerializeField] public float initialAngle;
        [SerializeField] public Color rayColor;
    }

    [SerializeField] private Field field;
    [SerializeField] private RayProperties[] rayProperties;
    private List<Ray> rays;

    private IEnumerator IStart() {
        rays = new List<Ray>();
        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < rayProperties.Length; ++i) 
            rays.Add(new Ray(field, rayProperties[i].initialCoordinates, rayProperties[i].initialAngle, rayProperties[i].rayColor));
    }

    private void Start() {
        IEnumerator startEnumerator = IStart();
        StartCoroutine(startEnumerator);
    }

    private void Update() {
        for (int i = 0; i < rays.Count; ++i) 
            rays[i].UpdateRay();
    }
}
