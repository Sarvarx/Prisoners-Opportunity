using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCounter : MonoBehaviour
{
    [HideInInspector] public int turret_1_sounds = 0;
    [HideInInspector] public int turret_2_sounds = 0;
    [HideInInspector] public int turret_3_sounds = 0;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void turret_1_reduse()
    {
        if (!(turret_1_sounds <= 0)) turret_1_sounds--;
    }
    public void turret_2_reduse()
    {
        if (!(turret_2_sounds <= 0)) turret_2_sounds--;
    }
    public void turret_3_reduse()
    {
        if (!(turret_3_sounds <= 0)) turret_3_sounds--;
    }
}
