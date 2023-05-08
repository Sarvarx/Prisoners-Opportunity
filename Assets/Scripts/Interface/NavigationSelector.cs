using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class NavigationSelector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    public NavigationBar navigationBar;
    public Color selectedColor, unselectedColor;
    public void OnPointerClick(PointerEventData eventData)
    {
        foreach(Image image in navigationBar.buttonImages)
        {
            if(image.gameObject == gameObject)
            {
                image.color = selectedColor;
                image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(120,98);
            }
            else
            {
                image.color = unselectedColor;
                image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 98);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
