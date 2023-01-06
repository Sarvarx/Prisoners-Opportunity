using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public Transform horizontalTransform;
    public Transform verticalTransform;
    public Transform gun;
    public GameObject bulletPrefab;
    public Transform firePoint;
    [HideInInspector] public ResourceBase resourceBase;

    public bool targetInAngle = false;
    public bool isActived = false;
    public float turnSpeed;

    public int costPrice;
    public int damage;
    public int magazine;
    public float fireRate;
    public float fireRange;
    public float reloadTime;
    public float targetingTime;
    public int accuracy;

    private int _magazineCD;
    private float _fireRateCD;
    private float _reloadTimeCD;
    private float _targetingTimeCD;
    private bool isTargeted;

    private void Start()
    {
        _magazineCD = magazine;
        _fireRateCD = fireRate;
        _reloadTimeCD = 0;
        _targetingTimeCD = targetingTime;
    }
    private void Update()
    {

        if (target == null || !isActived) return;
        RotateToTarget();

        if (targetInAngle){
            _targetingTimeCD -= Time.deltaTime;
            if (_targetingTimeCD <= 0)
            {
                isTargeted = true;
            }
        }
        else
        {
            _targetingTimeCD = targetingTime;
            isTargeted = false;
        }

        //Shoot

        _reloadTimeCD -= Time.deltaTime;
        if(_reloadTimeCD <= 0)
        {

            _fireRateCD -= Time.deltaTime;
            if (_fireRateCD <= 0 && isTargeted)
            {
                _fireRateCD = fireRate;
                _magazineCD--;
                if (_magazineCD <= 0)
                {
                    _reloadTimeCD = reloadTime;
                }
                //Raycast
                Shoot();
                
            }
        }
        else { _magazineCD = magazine; }
    }
    void Shoot()
    {

        GetComponent<Animator>().Play("Shoot");
        bool hit = Random.Range(0, 100) < accuracy;


        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        bulletGO.transform.localScale = new Vector3(1,1,0.2f);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null)
        {
            
            bullet.Seek(target,hit,damage);
        }
            
            
    }
    void RotateToTarget()
    {
        Transform cursor = target.GetComponent<Unit>().Cursor;
        Quaternion toRotation = Quaternion.LookRotation(target.position - horizontalTransform.position);
        Quaternion rotation = Quaternion.RotateTowards(horizontalTransform.localRotation, toRotation, turnSpeed * Time.deltaTime);
        rotation.z = 0;
        rotation.x = 0;
        horizontalTransform.localRotation = rotation;


        
        Quaternion toRotation2 = Quaternion.LookRotation(cursor.position - verticalTransform.position);
        Quaternion rotation2 = Quaternion.RotateTowards(verticalTransform.localRotation, toRotation2, (turnSpeed*3) * Time.deltaTime);
        rotation2.z = 0;
        rotation2.y = 0;
        verticalTransform.localRotation = rotation2;

        targetInAngle = TargetInAngle(gun ,cursor, 6);
    }

    bool TargetInAngle(Transform from,Transform to, float edge)
    {
        Vector3 targetDir = to.position - from.position;
        float angle = Vector3.SignedAngle(targetDir, from.forward, Vector3.up);

        return angle < edge && angle > -edge;
    }

    public void Cost()
    {
        resourceBase.resource -= costPrice;
        isActived = true;
    }
}
