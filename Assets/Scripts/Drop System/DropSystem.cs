using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    public Transform mainCamera;
    public Transform[] turrets;
    public Transform tilesParent;
    public Transform PlacementDialog;
    public Transform turretButtons;
    public Transform constructionButton;
    public Transform backScreen;
    public ResourceBase resourceBase;
    public SoundCounter soundCounter;

    private Transform previewTurret = null;
    public void Drop(int index)
    {
        if(!(resourceBase.resource >= turrets[index].GetComponent<Turret>().costPrice))
        {
            return;
        }

        Vector3 origin = mainCamera.transform.position;
        Vector3 direction = mainCamera.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity))
        {

            Transform tile = GetClosestTile(tilesParent, hit.point);
            previewTurret = Instantiate(turrets[index], tile.position,Quaternion.identity);
            previewTurret.GetComponent<TileFade>().Tiles = tilesParent;
            previewTurret.GetComponent<Turret>().resourceBase = resourceBase;
            previewTurret.GetComponent<Turret>().soundCounter = soundCounter;
            ChangeLayers(previewTurret.gameObject, "Display2");
            transform.gameObject.SetActive(false);
            PlacementDialog.gameObject.SetActive(true);
            PlacementDialog.GetComponent<PlacementDialog>().dropSystem = transform;
            PlacementDialog.GetComponent<PlacementDialog>().previousTurret = previewTurret;
            PlacementDialog.GetComponent<PlacementDialog>().currentTile = tile;
            PlacementDialog.GetComponent<PlacementDialog>().tilesParent = tilesParent;
            backScreen.gameObject.SetActive(false);
            tilesParent.gameObject.SetActive(true);
        }
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

    public void OpenClose()
    {
        turretButtons.gameObject.SetActive(!turretButtons.gameObject.activeInHierarchy);
        constructionButton.gameObject.SetActive(!constructionButton.gameObject.activeInHierarchy);
    }
    public void Open()
    {
        backScreen.gameObject.SetActive(true);
        turretButtons.gameObject.SetActive(true);
        constructionButton.gameObject.SetActive(false);

    }
    public void Close()
    {
        backScreen.gameObject.SetActive(false);
        turretButtons.gameObject.SetActive(false);
        constructionButton.gameObject.SetActive(true);
        //SetTilesColor(tilesParent, new Color(0, 0, 0, 0));
    }

    void SetTilesColor(Transform tiles, Color color)
    {
        foreach (Transform tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().color = color;
        }
    }
    public static void ChangeLayers(GameObject go, string name)
    {
        ChangeLayers(go, LayerMask.NameToLayer(name));
    }

    public static void ChangeLayers(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            ChangeLayers(child.gameObject, layer);
        }
    }
}
