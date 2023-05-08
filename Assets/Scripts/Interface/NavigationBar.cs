using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationBar : MonoBehaviour
{
    public Transform[] Pages;
    public Image[] buttonImages;
    
    public void Open(int index)
    {
        int position = 0;
        foreach (Transform page in Pages)
        {
            if (index == position)
            {
                page.gameObject.SetActive(true);
            }
            else
            {
                page.gameObject.SetActive(false);
            }
            position++;
        }
    }
}
