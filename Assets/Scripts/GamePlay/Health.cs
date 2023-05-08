using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    [SerializeField] Color healthColor;
    [SerializeField] Color unHealthColor;
    public Transform loseGamePanel;
    public Transform StopGameObjects;
    void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeHealth()
    {
        currentHealth--;

        if (currentHealth < 1)
        {
            loseGamePanel.gameObject.SetActive(true);
            StopGameObjects.gameObject.SetActive(false);
            print("GameOver");
        }

        int counter = 1;
        foreach(Transform healthItem in transform)
        {
            if(counter <= currentHealth)
            {
                healthItem.GetComponent<Image>().color = healthColor;
            }
            else
            {
                healthItem.GetComponent<Image>().color = unHealthColor;
            }
            counter++;
        }
    }
    
}
