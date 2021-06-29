using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public GameObject[] sprites;

    [Header("Camera")]
    private Camera mainCamera;
    private Vector2 screenBounds;
    private Vector3 lastScreenPosition;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        lastScreenPosition = transform.position;
    }

    void LateUpdate()
    {
        foreach (GameObject obj in sprites)
        {
            float parallaxSpeed = 1.2f - Mathf.Clamp01(Mathf.Abs(transform.position.z / obj.transform.position.z));
            float difference = transform.position.y - lastScreenPosition.y;
            obj.transform.Translate(Vector3.up * difference * parallaxSpeed);
        }
        lastScreenPosition = transform.position;
    }
}
