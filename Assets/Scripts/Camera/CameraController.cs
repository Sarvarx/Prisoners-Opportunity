using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 hit_position = Vector3.zero;
    Vector3 current_position = Vector3.zero;
    Vector3 camera_position = Vector3.zero;
    public float y = 0.0f;

    public float xMin, xMax, zMin, zMax;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit_position = Input.mousePosition;
            camera_position = transform.position;

        }
        if (Input.GetMouseButton(0))
        {
            current_position = Input.mousePosition;
            LeftMouseDrag();
        }
    }

    void LeftMouseDrag()
    {
        current_position.z = hit_position.z = camera_position.y;

        Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);

        direction = direction * -1;

        Vector3 position = camera_position + direction;

        transform.position = new Vector3(CameraLimit(position.x,xMin,xMax), y, CameraLimit(position.z, zMin, zMax));
    }

    public float CameraLimit(float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }
}
