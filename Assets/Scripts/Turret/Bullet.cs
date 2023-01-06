using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private Transform target;
    public float speed = 70;
    private int damage = 10;
    public bool hit;
    public Transform GroundFlash;
    private Transform missedPoint;
    private Transform cursor;
    
    public void Seek(Transform _targret,bool _hit, int _damage)
    {
        target = _targret;
        hit = _hit;
        damage = _damage;

        if (hit)
        {
            transform.LookAt(target.GetComponent<Unit>().Cursor);
            cursor = target.GetComponent<Unit>().Cursor;
        }
        else
        {
            missedPoint = target.GetComponent<Unit>().GetMissedPoint();
            transform.LookAt(missedPoint);
            cursor = missedPoint;
        }
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
        transform.localScale = new Vector3(1, 1, Mathf.Clamp(transform.localScale.z * 2, 0.2f, 2));
        if (dir.magnitude <= distanceThisFrame)
        {
            if (hit)
            {
                HitTarget();
            }
            else
            {
                Missed();
            }
            
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(cursor);
    }
    void HitTarget()
    {
        target.GetComponent<Unit>().TakeDamage(damage);
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
