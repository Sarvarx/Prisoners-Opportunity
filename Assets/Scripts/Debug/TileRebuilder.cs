using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRebuilder : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(true);
    }
    void Start()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
