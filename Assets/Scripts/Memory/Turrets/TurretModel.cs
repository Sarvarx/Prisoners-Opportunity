
using System;
[Serializable]
public class TurretModel
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

    public TurretModel(string modelName, int level, int damage, float accuracy, float fireRange, float rotationSpeed, float targetingTime, float reloadingTime, string mainTarget)
    {
        this.modelName = modelName;
        this.level = level;
        this.damage = damage;
        this.accuracy = accuracy;
        this.fireRange = fireRange;
        this.rotationSpeed = rotationSpeed;
        this.targetingTime = targetingTime;
        this.reloadingTime = reloadingTime;
        this.mainTarget = mainTarget;
    }
    public TurretModel()
    {

    }
}
