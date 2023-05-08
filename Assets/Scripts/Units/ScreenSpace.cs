using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSpace : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private Camera mainCamera;
    public Text text;
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
