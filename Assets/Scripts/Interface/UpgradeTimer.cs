using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class UpgradeTimer : MonoBehaviour
{
    public string turretName;
    private bool inProgress;
    private DateTime TimerStart;
    private DateTime TimerEnd;

    private Coroutine lastTimer;
    private Coroutine lastDisplay;

    [Header("Production Time")]
    public int Days;
    public int Hours;
    public int Minutes;
    public int Seconds;

    [Header("UI")]
    [SerializeField] private GameObject UpgradeWindow;
    [SerializeField] private GameObject TooltipWindow;
    [SerializeField] private TMP_Text timeLeftText;
    [SerializeField] private Button StartButton;
    [SerializeField] private Button SkipButton;
    private void Awake()
    {
        if(FileHandler.LoadData("timer") == "true"&& FileHandler.LoadData("upgradeName") == turretName)
        {
            LoadTimer();
            print("Loaded");
        }
    }

    private void Start()
    {
        StartButton.onClick.AddListener(StartTimer);
        SkipButton.onClick.AddListener(Skip);
    }
    
    private void InitializeWindow()
    {
        print("Start Time: " + TimerStart);
        print("End Time: " + TimerEnd);
        TooltipWindow.SetActive(true);
        UpgradeWindow.SetActive(false);
        lastDisplay = StartCoroutine(DisplayTime());
    }

    private IEnumerator DisplayTime()
    {
        DateTime start = DateTime.Now;
        TimeSpan timeLeft = TimerEnd - start;
        double totalSecondsLeft = timeLeft.TotalSeconds;
        double totalSeconds = (TimerEnd - TimerStart).TotalSeconds;
        string text;

        while (TooltipWindow.activeSelf)
        {
            text = "";
            if (totalSecondsLeft > 1)
            {
                if(timeLeft.Days != 0)
                {
                    text += timeLeft.Days + "d ";
                    text += timeLeft.Hours + "h";
                    yield return new WaitForSeconds(timeLeft.Minutes * 60);
                }
                else if(timeLeft.Hours != 0)
                {
                    text += timeLeft.Hours + "h ";
                    text += timeLeft.Minutes + "m";
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                    text += ts.Minutes + "m ";
                    text += ts.Seconds + "s";
                }
                else
                {
                    text += Mathf.FloorToInt((float)totalSecondsLeft) + "s";
                }
                timeLeftText.text = text;

                totalSecondsLeft -= Time.deltaTime;
                yield return null;
            }
            else
            {
                FileHandler.SaveData("timer", "false");
                timeLeftText.text = "Finished";
                TooltipWindow.SetActive(false);
                UpgradeWindow.SetActive(true);
                inProgress = false;
                break;
            }
        }
        yield return null;
    }
    
    private void StartTimer()
    {
        TimerStart = DateTime.Now;
        TimeSpan time = new TimeSpan(Days, Hours, Minutes, Seconds);
        TimerEnd = TimerStart.Add(time);
        inProgress = true;
        FileHandler.SaveData("upgrade_start_time", TimerStart.ToString());
        FileHandler.SaveData("upgrade_end_time", TimerEnd.ToString());
        FileHandler.SaveData("upgrade_start_time", TimerStart.ToString());
        FileHandler.SaveData("timer", "true");
        FileHandler.SaveData("upgradeName", turretName);
        lastTimer = StartCoroutine(Timer());
        InitializeWindow();
    }
    private void LoadTimer()
    {
        TimerStart = DateTime.Parse(FileHandler.LoadData("upgrade_start_time"));
        TimerEnd = DateTime.Parse(FileHandler.LoadData("upgrade_end_time"));
        lastTimer = StartCoroutine(Timer());
        InitializeWindow();
        inProgress = true;
    }
    private IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        double secondsToFinished = (TimerEnd - start).TotalSeconds;
        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));

        inProgress = false;
        Debug.Log("Finished");
        FileHandler.SaveData("timer", "false");
    }
    
    private void Skip()
    {
        FileHandler.SaveData("timer", "false");
        TimerEnd = DateTime.Now;
        inProgress = false;
        timeLeftText.text = "Finished!";
        StopCoroutine(lastTimer);
        StopCoroutine(lastDisplay);
        TooltipWindow.SetActive(false);
        UpgradeWindow.SetActive(true);
    }
}