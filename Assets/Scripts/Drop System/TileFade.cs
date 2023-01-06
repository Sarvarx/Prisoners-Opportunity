using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFade : MonoBehaviour
{
    public Transform Tiles;

    public void Update()
    {
        SetTilesAlpha(Tiles, 5);
    }

    void SetTilesAlpha(Transform tiles, float MaxDistance)
    {
        foreach (Transform tile in tiles)
        {
            float distance = Vector3.Distance(tile.position, transform.position);

            if (distance<=MaxDistance)
            {
                float normalized = 1 - ((distance / MaxDistance)-0.1f);
                float percent = (distance / MaxDistance) * 100;
                float colorValue = (255 / 100) * percent;

                tile.GetComponent<SpriteRenderer>().color = new Color(255 - colorValue, 255 - colorValue, 0 - colorValue, (normalized/100f)*50);
            }
            else
            {
                tile.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.1f);
            }
        }
    }

}
