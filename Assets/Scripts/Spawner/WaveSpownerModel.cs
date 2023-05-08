using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WaveSpownerModel
{
    public float groupSpace;
    public SubWaveSpawnerModel[] group;
    public WaveSpownerModel(float groupSpace, SubWaveSpawnerModel[] group)
    {
        this.groupSpace = groupSpace;
        this.group = group;
    }
}
