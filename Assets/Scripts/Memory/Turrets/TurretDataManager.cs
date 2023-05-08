using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.ComponentModel;
using System.Reflection;

public class TurretDataManager : MonoBehaviour
{
    List<TurretModel> list = new List<TurretModel>();
    public void Upgrade(string name, TurretScriptable model)
    {
        TurretModel newModel = new TurretModel(model.modelName, model.level, model.damage, model.accuracy, model.fireRange, model.rotationSpeed, model.targetingTime, model.reloadingTime, model.mainTarget);
        
        list.Clear();
        list.Add(newModel);
        FileHandler.SaveToJSON<TurretModel>(list, name+".json");
        print("UPGRADED!");
    }
    public void Read(string name)
    {
        List<TurretModel> list = new List<TurretModel>();
        string filename = name + ".json";
        if (File.Exists(GetPath(filename)))
        {
            list = FileHandler.ReadListFromJSON<TurretModel>(filename);
            foreach (TurretModel model in list)
            {
                print(model.modelName);
                print("READ!");
            }
        }
        else
        {
            Debug.LogWarning("FILE NOT FOUND: " + filename);
        }
    }
    private string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    public int GetCurrentLevel(string name)
    {
        List<TurretModel> list = new List<TurretModel>();
        string filename = name + ".json";
        if (File.Exists(GetPath(filename)))
        {
            list = FileHandler.ReadListFromJSON<TurretModel>(filename);
            foreach (TurretModel model in list)
            {
                print("LEVEL:" + model.level);
                return model.level;
            }
        }
        else
        {
            
        }
        Debug.LogWarning("FILE NOT FOUND: " + filename);
        return 1;
    }
}
