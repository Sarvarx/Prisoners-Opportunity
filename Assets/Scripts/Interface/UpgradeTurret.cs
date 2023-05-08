using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeTurret : MonoBehaviour
{
    public string modelName;
    public TurretDataManager turretDataManager;
    public TurretScriptable[] levels;
    public void Upgrade()
    {
        int curretLevel = turretDataManager.GetCurrentLevel(modelName);
        if(levels.Length > curretLevel) turretDataManager.Upgrade(modelName, levels[curretLevel]);
    }
}