using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    public string[] TargetTags;
    private Transform target;
    public float speed = 70;
    private int damage = 10;
    public float explosionRadius;
    public AnimationCurve explosionFade;
    public bool hit;
    public bool lastBullet;
    public Transform GroundFlash;
    private Vector3 cursor;
    public Turret ownerTurret;

    public void SeekExploson(Transform _targret, int _damage, bool _lastBullet, float explosionRadius)
    {
        target = _targret;
        this.explosionRadius = explosionRadius;
        Unit unit = target.GetComponent<Unit>();
        damage = _damage;
        lastBullet = _lastBullet;

        transform.LookAt(unit.Cursor);
        cursor = _targret.position;
        hit = true;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = cursor - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        transform.localScale = new Vector3(1, 1, Mathf.Clamp(transform.localScale.z * 2, 0.2f, 1));
        if (dir.magnitude <= distanceThisFrame)
        {
            Explode();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(cursor);
    }
    void Explode()
    {
        transform.position = cursor;
        Transform groundHit = Instantiate(GroundFlash, transform.position, Quaternion.identity);
        groundHit.LookAt(Camera.main.transform);
        Destroy(groundHit.gameObject, 1);

        foreach (string tag in TargetTags)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= explosionRadius)
                {
                    float fadeCursor = distance / explosionRadius;
                    target.GetComponent<Unit>().TakeDamage((int)(damage * explosionFade.Evaluate(fadeCursor)));
                }
            }
        }
        Destroy(gameObject);
    }
}
