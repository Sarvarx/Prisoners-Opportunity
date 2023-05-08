using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    public Text text;
    void Start()
    {
        
    }
    public void ChangeSpeed()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
            text.text = "2X";
        }
        else if (Time.timeScale == 2)
        {
            Time.timeScale = 0.5f;
            text.text = "0.5X";
        }
        else 
        { 
            Time.timeScale = 1;
            text.text = "1X";
        }
    }
}
