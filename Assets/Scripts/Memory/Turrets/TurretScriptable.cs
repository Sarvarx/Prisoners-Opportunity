
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "TurretLevels", menuName = "Wave/TurretLevel")]
public class TurretScriptable : ScriptableObject
{
    public string modelName;
    public int level;
    public int damage;
    public float accuracy;
    public float fireRange;
    public float rotationSpeed;
    public float targetingTime;
    public float reloadingTime;
    public string mainTarget;
}
