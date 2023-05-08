using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class UpgradeTimerData
{
    public string timeStart;
    public string timeEnd;
    public UpgradeTimerData()
    {
        timeStart = "";
        timeEnd = "";
    }

    public UpgradeTimerData(DateTime start, DateTime end)
    {
        timeStart = start.ToString();
        timeEnd = end.ToString();
    }

}
