using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    public string[] TargetTags;
    public LayerMask targetMask;
    public int radius = 15;
    public bool DenseTarget;
    Turret turret;
    private void Start()
    {
        turret = GetComponent<Turret>();
        if (DenseTarget)
        {
            InvokeRepeating("UpdateDenseTarget", 1, 1f);
        }
        else
        {
            InvokeRepeating("UpdateTarget", 1, 0.3f);
        }
    }
    public void UpdateTarget(){
        if (turret.target) return;
        foreach (string tag in TargetTags)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
            Transform target = GetClosestTarget(targets);
            if (target)
            {
                turret.target = target;
                turret.unit = target.GetComponent<Unit>();
                turret.readyToFire = true;
                break;
            }
            else
            {
                turret.target = null;
                turret.unit = null;
            }
        }
    }
    public void UpdateDenseTarget()
    {
        if (turret.target) return;
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, targetMask);
        Transform target = GetDenseTarget(targets);
        if (target)
        {
            turret.target = target;
            turret.unit = target.GetComponent<Unit>();
            turret.readyToFire = true;
        }
        else
        {
            turret.target = null;
            turret.unit = null;
        }
    }
    public void ChangeTarget()
    {
        if (DenseTarget)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, radius, targetMask);
            Transform target = GetDenseTarget(targets);
            if (target)
            {
                turret.target = target;
                turret.unit = target.GetComponent<Unit>();
                turret.readyToFire = true;
            }
            else
            {
                turret.readyToFire = false;
                turret.target = null;
                turret.unit = null;
            }
        }
        else
        {
            foreach (string tag in TargetTags)
            {
                GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
                Transform target = GetClosestTarget(targets);
                if (target)
                {
                    turret.target = target;
                    turret.unit = target.GetComponent<Unit>();
                    turret.readyToFire = true;
                    break;
                }
                else
                {
                    turret.readyToFire = false;
                    turret.target = null;
                    turret.unit = null;
                }
            }
        }
    }
    public Transform GetClosestTarget(GameObject[] targets)
    {
        
        if (turret.target)
        {
            if (turret.target.GetComponent<Unit>().isDied == false /*IF NOT DIED*/ )
            {
                float distance = Vector3.Distance(transform.position, turret.target.position);
                if (distance < turret.fireRange)
                {
                    return turret.target;
                }
            }
        }
        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= turret.fireRange)
            {
                if (distanceToTarget < shortestDistance)
                {
                    if (!target.GetComponent<Unit>().unitActive) continue;
                    nearestTarget = target.transform;
                    shortestDistance = distanceToTarget;
                    turret.targetDestroyed = false;
                }
            }
        }
        return nearestTarget;
    }

    public Transform GetDenseTarget(Collider[] targets)
    {
        if (turret.target)
        {
            if (turret.target.GetComponent<Unit>().isDied == false /*IF NOT DIED*/ )
            {
                float distance = Vector3.Distance(transform.position, turret.target.position);
                if (distance < turret.fireRange)
                {
                    return turret.target;
                }
            }
        }
        Transform denseTarget = null;
        float denseRadius = turret.explosionRadius;
        int denseCount = 0;
        foreach (Collider target in targets)
        {
            Collider[] dense = Physics.OverlapSphere(target.transform.position,denseRadius,targetMask);
            if (denseCount < dense.Length)
            {
                if (!target.GetComponent<Unit>().unitActive) continue;
                denseTarget = target.transform;
                denseCount = dense.Length;
            }
        }

        return denseTarget ? denseTarget : null;
    }
}
