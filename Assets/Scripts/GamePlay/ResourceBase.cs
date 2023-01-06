using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceBase : MonoBehaviour
{
    public int resource;
    public int resourceIncome;
    public int resourceBase;
    public int totalResources;

    public Text text;

    private void Start()
    {
        InvokeRepeating("ResourceUpdate",0,0.5f);
        InvokeRepeating("Income", 0, 1f);
        resource = resourceBase;
        totalResources = resource;
    }
    void Income()
    {
        resource += resourceIncome;
        totalResources += resourceIncome;
    }
    void ResourceUpdate()
    {
        text.text = resource+"";

    }
    public void AddResource(int value)
    {
        resource += value;
        totalResources += value;
        text.text = resource + "";
    }
}
