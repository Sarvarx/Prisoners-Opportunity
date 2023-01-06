using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WaveSpownerModel
{
    public int column;
    public int line;
    public float space;
    public float groupSpace;
    public float waveTime;
    public WaveSpownerModel(int column, int line, float space, float groupSpace, float waveTime)
    {
        this.column = column;
        this.line = line;
        this.space = space;
        this.groupSpace = groupSpace;
        this.waveTime = waveTime;
    }
}
