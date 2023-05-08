using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCoordinates : MonoBehaviour
{
    public Transform target;
    // To adjust the position of the icon
    public Vector3 offset;
    private Camera mainCamera;
    private bool oneTime;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }
        float minX = 0;
        float maxX = Screen.width - minX;

        float minY = 0;
        float maxY = Screen.height - minY;

        Vector2 pos = mainCamera.WorldToScreenPoint(target.position + offset);

        if (Vector3.Dot((target.position - mainCamera.transform.position), mainCamera.transform.forward) < 0)
        {
            Activation(false);
        }
        else
        {
            Activation(true);
        }
        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
        
    }
    void Activation(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
