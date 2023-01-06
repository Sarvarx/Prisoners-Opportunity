using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class FPSManager : MonoBehaviour
{
    public Text fpsText;
    float avg = 0F;
    long ramNumber;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        Profiler.maxUsedMemory = 1024 * 1048576;
    }
    void Update()
    {
        avg += ((Time.deltaTime / Time.timeScale) - avg) * 0.03f; //run this every frame
        int displayValue = (int) (1F / avg); //display this value
        long size = Profiler.maxUsedMemory / 1048576;

        fpsText.text = "FPS: " + displayValue;
    }
    static double ConvertBytesToMegabytes(long bytes)
    {
        return (bytes / 1024f) / 1024f;
    }
}
