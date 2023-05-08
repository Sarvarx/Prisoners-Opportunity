using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PageOpener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    public Transform[] OpenPages;
    public Transform[] ClosePages;
    public void OnPointerClick(PointerEventData eventData)
    {
        foreach(Transform pages in ClosePages)
        {
            pages.gameObject.SetActive(false);
        }

        foreach (Transform pages in OpenPages)
        {
            pages.gameObject.SetActive(true);
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
