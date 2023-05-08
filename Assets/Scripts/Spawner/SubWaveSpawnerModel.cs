using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SubGroup", menuName = "Wave/SubGroup")]
public class SubWaveSpawnerModel : ScriptableObject
{
    public Transform unit;
    public int unitCount;
    public float space;
    public SubWaveSpawnerModel(Transform unit, int unitCount,float space)
    {
        this.unit = unit;
        this.unitCount = unitCount;
        this.space = space;
    }
}
