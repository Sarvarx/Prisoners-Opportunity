using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string[] TargetTags;
    private Transform target;
    public float speed = 70;
    private int damage = 10;
    public float explosionRadius;
    public bool explosion = false;
    public bool hit;
    public bool lastBullet;
    public Transform GroundFlash;
    private Transform missedPoint;
    private Transform cursor;
    public Turret ownerTurret;
    
    public void Seek(Transform _targret,bool _hit, int _damage, bool _lastBullet)
    {
        target = _targret;
        Unit unit = target.GetComponent<Unit>();
        damage = _damage;
        lastBullet = _lastBullet;
        hit = _hit;
        if (unit.currentHealth<=0) hit = false;
        if (hit)
        {
            transform.LookAt(unit.Cursor);
            cursor = unit.Cursor;
        }
        else
        {
            missedPoint = unit.GetMissedPoint();
            transform.LookAt(missedPoint);
            cursor = missedPoint;
        }
    }
    public void Seek(Transform _targret, bool _hit,bool _lastBullet)
    {
        if (!_targret) return;
        target = _targret;
        hit = false;
        lastBullet = _lastBullet;

        missedPoint = _targret.GetComponent<Unit>().GetMissedPoint();
        transform.LookAt(missedPoint);
        cursor = missedPoint;

    }
    public void SeekExploson(Transform _targret, int _damage, bool _lastBullet, float explosionRadius)
    {
        explosion = true;
        target = _targret;
        this.explosionRadius = explosionRadius;
        Unit unit = target.GetComponent<Unit>();
        damage = _damage;
        lastBullet = _lastBullet;

        transform.LookAt(unit.Cursor);
        cursor = unit.Cursor;
        hit = true;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = cursor.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        transform.localScale = new Vector3(1, 1, Mathf.Clamp(transform.localScale.z * 5, 0.4f, 2));
        if (dir.magnitude <= distanceThisFrame)
        {
            if (explosion)
            {
                Explode();
            }
            else
            {
                if (hit)
                {
                    HitTarget();
                }
                else
                {
                    Missed();
                }
            }
            
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(cursor);
    }
    void HitTarget()
    {
        Unit unit = target.GetComponent<Unit>();
        unit.TakeDamage(damage);
        Destroy(gameObject);
        if (lastBullet)
        {
            if (unit.isDied)
            {
                ownerTurret.targetDestroyed = true;
            }
        }
    }
    void Explode()
    {
        foreach (string tag in TargetTags)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
            foreach(GameObject target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= explosionRadius) 
                {
                    target.GetComponent<Unit>().TakeDamage(damage);
                }
            }
        }
        Destroy(gameObject);
    }
    void Missed()
    {
        Transform groundHit = Instantiate(GroundFlash,transform.position,Quaternion.identity);
        groundHit.LookAt(Camera.main.transform);
        Destroy(groundHit.gameObject, 1);
        Destroy(gameObject);
    }
}
