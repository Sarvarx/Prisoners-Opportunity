using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementDialog : MonoBehaviour
{
    [HideInInspector] public Transform dropSystem;
    [HideInInspector] public Transform previousTurret;
    [HideInInspector] public Transform currentTile;
    [HideInInspector] public Transform tilesParent;
    private Vector2 previewsPosition;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previewsPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(VectorEquals(previewsPosition, Input.mousePosition))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    currentTile = GetClosestTile(tilesParent, hit.point);
                    //previousTurret.position = currentTile.position;
                }
            }
        }
        previousTurret.position = Vector3.Lerp(previousTurret.position, currentTile.position, 20 * Time.deltaTime);
    }
    public void Build()
    {

        previousTurret.gameObject.SetActive(true);
        previousTurret.GetComponent<Turret>().Cost();
        DropSystem.ChangeLayers(previousTurret.gameObject, "Default");
        dropSystem.gameObject.SetActive(true);
        gameObject.SetActive(false);
        currentTile.GetComponent<TileItem>().isBusy = true;
        dropSystem.GetComponent<DropSystem>().OpenClose();
        tilesParent.gameObject.SetActive(false);
        previousTurret.GetComponent<TileFade>().enabled = false;
    }
    public void Cancel()
    {
        Destroy(previousTurret.gameObject);
        dropSystem.gameObject.SetActive(true);
        gameObject.SetActive(false);
        dropSystem.GetComponent<DropSystem>().Open();
        tilesParent.gameObject.SetActive(false);
    }

    Transform GetClosestTile(Transform parent, Vector3 point)
    {
        Transform target = null;
        float minDistance = Mathf.Infinity;
        foreach (Transform child in parent)
        {
            float dist = Vector3.Distance(child.position, point);
            if (dist < minDistance && !child.GetComponent<TileItem>().isBusy)
            {
                target = child;
                minDistance = dist;
            }
        }
        return target;
    }

    public bool VectorEquals(Vector2 position1, Vector2 position2)
    {
        if((position1.x-3 >= position2.x || position1.x + 3 <= position2.x) && (position1.y - 3 >= position2.y || position1.y + 3 <= position2.y))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
