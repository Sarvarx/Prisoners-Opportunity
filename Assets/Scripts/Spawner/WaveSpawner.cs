using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour
{
    public Transform[] Waypoints;
    public Transform[] units;
    public Transform unitsParent;
    public Transform healthBar;
    public Transform healthBarSpawner;
    public Health health;
    public ResourceBase resourceBase;
    public Text waveText;
    public Text nextWaveTimeText;
    public Text waveTimeTitle;
    public Transform nextWaveTimePanel;
    public Transform endGamePanel;

    public int Wave;
    public int SubWave;
    public int UnitCount;
    public float NextWaveTime;
    [SerializeField] public WaveSpownerModel[] waveData;

    float _space;
    float _groupSpace;
    float _nextWaveTime;
    bool firstWave = true;
    bool readyToFinish = false;
    void Start()
    {
        Wave = 0;
        SubWave = 0;
        UnitCount = waveData[Wave].group[SubWave].unitCount;
        string _waveText = "Wave 1/" + waveData.Length;
        waveText.text = _waveText;
        _nextWaveTime = NextWaveTime;
    }

    void Update()
    {
        _nextWaveTime -= Time.deltaTime;
        if (firstWave)
        {
            waveTimeTitle.text = "First Wave";
        }
        if (Wave > waveData.Length-1)
        {
            nextWaveTimePanel.gameObject.SetActive(false);
            if (unitsParent.childCount == 0 && readyToFinish) Win();
            print("Wave End");
            return;
        }
        nextWaveTimeText.text = ((int)_nextWaveTime) + " sec";
        if (_nextWaveTime <= 0)
        {
            if (SubWave < waveData[Wave].group.Length)
            {
                _groupSpace -= Time.deltaTime;
                if (_groupSpace <= 0)
                {
                    if (UnitCount > 0)
                    {
                        _space -= Time.deltaTime;
                        if (_space <= 0)
                        {
                            ProduceUnit(waveData[Wave].group[SubWave].unit);
                            UnitCount--;
                            _space = waveData[Wave].group[SubWave].space;
                        }
                    }
                    else
                    {
                        SubWave++;
                        _groupSpace = waveData[Wave].groupSpace;
                        if (SubWave < waveData[Wave].group.Length)
                        {
                            UnitCount = waveData[Wave].group[SubWave].unitCount;
                        }
                    }
                }
            }
            else
            {
                if (Wave+1 > waveData.Length - 1)
                {
                    Wave++;
                    nextWaveTimePanel.gameObject.SetActive(false);
                    readyToFinish = true;
                    return;
                }
                firstWave = false;
                if (unitsParent.childCount != 0) return;
                nextWaveTimePanel.gameObject.SetActive(true);
                Wave++;

                if(Wave == waveData.Length-1) waveTimeTitle.text = "Last Wave";
                else waveTimeTitle.text = "Next Wave";

                waveText.text = "Wave " + ((int)Wave+1) + "/" + waveData.Length;
                _groupSpace = 0;
                SubWave = 0;
                if(Wave < waveData.Length) UnitCount = waveData[Wave].group[SubWave].unitCount;
                _nextWaveTime = NextWaveTime;
            }
        }
    }
    public void Win()
    {
        endGamePanel.gameObject.SetActive(true);
    }
    void ProduceUnit(Transform unitPreview)
    {
        nextWaveTimePanel.gameObject.SetActive(false);
        Transform unitTransform = Instantiate(unitPreview, transform.position, transform.rotation, unitsParent);
        Unit unit = unitTransform.GetComponent<Unit>();
        unit.health = health;
        unit.resource = resourceBase;
        unit.waypoints = Waypoints[Random.Range(0, 3)].GetComponent<Waypoints>();

        Vector2 pos = Camera.main.WorldToScreenPoint(unitTransform.position);
        Transform _healthBar = Instantiate(healthBar, pos,healthBar.rotation,healthBarSpawner);
        _healthBar.GetComponent<HealthCoordinates>().target = unitTransform;
        unit.healthBar = _healthBar.GetComponent<HealthBar>();

    }

    public void SkipNextWave()
    {
        _nextWaveTime = 0;
        nextWaveTimePanel.gameObject.SetActive(false);
    }
}
