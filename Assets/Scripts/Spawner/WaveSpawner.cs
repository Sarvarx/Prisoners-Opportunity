using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] Waypoints;
    public Transform[] units;
    public ResourceBase resourceBase;

    public int Wave;
    public int Column;
    public int Line;
    public float Space;
    public float GroupSpace;
    public float WaveTime;
    public float NextWaveTime;
    [SerializeField] public WaveSpownerModel[] waveData;

    float _space;
    float _groupSpace;
    float _waveTime;
    float _nextWaveTime;
    int _line;

    void Start()
    {
        Wave = 0;
        _waveTime = waveData[Wave].waveTime;
    }

    void Update()
    {
        
        _waveTime -= Time.deltaTime;
        if(!(_waveTime <= 0))
        {
            _nextWaveTime = NextWaveTime;
            _groupSpace -= Time.deltaTime;
            _space -= Time.deltaTime;

            if (_groupSpace <= 0)
            {
                if (_space <= 0)
                {

                    ProduseUnit(units[0], waveData[Wave].column);
                    _space = waveData[Wave].space;
                    _line--;
                    if (_line <= 0)
                    {
                        _groupSpace = waveData[Wave].groupSpace;
                        _line = waveData[Wave].line;
                        
                    }
                }
            }
        }
        else
        {
            
            _nextWaveTime -= Time.deltaTime;
            if(_nextWaveTime <= 0)
            {
                Wave++;
                _waveTime = waveData[Wave].waveTime ;
            }
        }
    }
    void ProduseUnit(Transform unitPreview,int column)
    {
        for (int i = 0; i < Mathf.Clamp(column,1,3); i++)
        {
            if (column == 1)
            {
                Transform unitTransform = Instantiate(unitPreview, transform.position, transform.rotation);
                Unit unit = unitTransform.GetComponent<Unit>();
                unit.resource = resourceBase;
                unit.waypoints = Waypoints[Random.Range(0,3)].GetComponent<Waypoints>();
            }
            else
            {
                Transform unitTransform = Instantiate(unitPreview, transform.position, transform.rotation);
                Unit unit = unitTransform.GetComponent<Unit>();
                unit.resource = resourceBase;
                unit.waypoints = Waypoints[i].GetComponent<Waypoints>();
            }
        }
    }
}
