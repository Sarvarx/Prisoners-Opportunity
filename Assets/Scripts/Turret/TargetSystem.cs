using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    public string TargetTag;
    public int radius = 15;
    Turret turret;
    private void Start()
    {
        turret = GetComponent<Turret>();
        InvokeRepeating("UpdateTarget", 1, 0.5f);
    }
    private void UpdateTarget(){
        GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTag);
        turret.target = GetClosestTarget(targets);
    }
    Transform GetClosestTarget(GameObject[] targets)
    {
        if (turret.target)
        {
            if (!turret.target.GetComponent<Unit>().isDied && Vector3.Distance(transform.position, turret.target.position)<=turret.fireRange)
            {
                return turret.target;
            }
        }
        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= GetComponent<Turret>().fireRange)
            {
                if (distanceToTarget < shortestDistance)
                {
                    nearestTarget = target.transform;
                    shortestDistance = distanceToTarget;
                }
            }
        }
        return nearestTarget;
    }
}
