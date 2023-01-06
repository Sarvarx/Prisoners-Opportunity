using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        InvokeRepeating("Billboard", 0,1);
    }
    public void SetHealth(int health)
    {
        slider.value = health;

    }
    void Billboard()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
