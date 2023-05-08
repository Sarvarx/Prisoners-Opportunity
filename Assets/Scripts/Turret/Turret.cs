using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ RequireComponent(typeof(AudioSource))]
public class Turret : MonoBehaviour
{
    public TurretName turretName;
    public Transform target;
    public Unit unit;
    public Transform lastTargetPoint;
    public Transform horizontalTransform;
    public Transform verticalTransform;
    public Transform gun;
    public GameObject bulletPrefab;
    public Transform muzzleFlash;
    public Transform firePoint;
    [HideInInspector] public ResourceBase resourceBase;
    [HideInInspector] public SoundCounter soundCounter;

    public AudioClip shootSound;

    public bool targetInAngle = false;
    public bool isActived = false;
    public bool readyToFire = true;
    public bool targetDestroyed;
    public float turnSpeed;

    public int costPrice;
    public int damage;
    public int magazine;
    public float fireRate;
    public float fireRange;
    public float reloadTime;
    public float targetingTime;
    public int accuracy;

    private int _magazine;
    private float nextTimeToFire;
    private float _reloadTime;
    private float _targetingTimeCD;
    private bool isTargeted;
    private int firePointsCount;
    
    AudioSource audioSource;
    TargetSystem targetSystem;

    [Space]
    [Header("GunTower")]

    public Transform[] firePoints;

    public float explosionRadius;
    private void Start()
    {
        _magazine = magazine;
        _reloadTime = 0;
        _targetingTimeCD = targetingTime;
        audioSource = GetComponent<AudioSource>();
        targetSystem = GetComponent<TargetSystem>();
    }
    private void Update()
    {
        if (!isActived) return;
        RotateToTarget();
        isTargeted = TargetInRadius();
        if (unit && _magazine == magazine)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (unit.isDied || distance > fireRange)
            {
                readyToFire = false;
                target = null;
                targetSystem.ChangeTarget();
            }
        }
        //Shoot
        if(turretName == TurretName.Explosion)
        {
            ExplosionGun();
        }
        else if(turretName == TurretName.Rocket)
        {

        }
        else if(turretName == TurretName.Gunmachine)
        {

        }
        else
        {
            SampleGun();
        }
        
    }
    void ExplosionGun()
    {
        _reloadTime -= Time.deltaTime;
        if (_reloadTime <= 0)
        {
            if (waitNextTimeToFire() && isTargeted && readyToFire)
            {
                ShootExplosion();
                _magazine--;
                if (_magazine <= 0) _reloadTime = reloadTime;
                if (_magazine <= 0) targetSystem.ChangeTarget();
            }
        }
        else
        {
            _magazine = magazine;
        }
    }
    void GunMashine()
    {
        if (waitNextTimeToFire() && isTargeted && readyToFire)
        {
            Shoot();
            _magazine--;
            if (_magazine <= 0) _reloadTime = reloadTime;
            if (_magazine <= 0) targetSystem.ChangeTarget();
        }
    }
    void SampleGun()
    {
        _reloadTime -= Time.deltaTime;
        if (_reloadTime <= 0)
        {
            if (waitNextTimeToFire() && isTargeted && readyToFire)
            {
                Shoot();
                _magazine--;
                if (_magazine <= 0) _reloadTime = reloadTime;
                if (_magazine <= 0) targetSystem.ChangeTarget();
            }
        }
        else
        {
            _magazine = magazine;
        }
    }

    bool waitNextTimeToFire()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            return true;
        }
        return false;
    }
    void ShootExplosion()
    {
        if (_magazine == magazine)
        {
            PlayShootSound();
        }
        if (target)
        {
            lastTargetPoint = target;
        }
        GetComponent<Animator>().Play("Shoot");
        Transform firepoint = GetFirePoints();
        if (muzzleFlash)
        {
            Instantiate(muzzleFlash, firepoint.position, firepoint.rotation, horizontalTransform);
        }
        GameObject bulletGO = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);

        bulletGO.transform.localScale = new Vector3(1, 1, 0.2f);
        ExplosionBullet bullet = bulletGO.GetComponent<ExplosionBullet>();
        bool lastBullet = _magazine == 1 ? true : false;
        if (bullet != null)
        {
            bullet.ownerTurret = this;
            if (target)
            {
                bullet.SeekExploson(target, damage, lastBullet,explosionRadius);
            }
            else
            {
                bullet.SeekExploson(lastTargetPoint, damage, lastBullet, explosionRadius);
            }
        }
    }
    void Shoot()
    {
        if (_magazine == magazine)
        {
            PlayShootSound();
        }
        if (target)
        {
            lastTargetPoint = target;
        }
        GetComponent<Animator>().Play("Shoot");
        bool hit = Random.Range(0, 100) < accuracy;

        if (muzzleFlash)
        {
            Instantiate(muzzleFlash, firePoint.position, firePoint.rotation,horizontalTransform);
        }

        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        bulletGO.transform.localScale = new Vector3(1,1,0.2f);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bool lastBullet = _magazine == 1 ? true:false;
        if (bullet != null)
        {
            bullet.ownerTurret = this;
            if (target)
            {
                bullet.Seek(target, hit, damage,lastBullet);
            }
            else
            {
                bullet.Seek(lastTargetPoint, false, lastBullet);
            }
        }
    }
    void RotateToTarget()
    {
        if (target == null) return;
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

        targetInAngle = TargetInAngle(gun ,cursor, 20);
    }

    bool TargetInAngle(Transform from,Transform to, float edge)
    {
        Vector3 targetDir = to.position - from.position;
        float angle = Vector3.SignedAngle(targetDir, from.forward, Vector3.up);

        return angle < edge && angle > -edge;
    }
    bool TargetInRadius()
    {
        if (targetInAngle)
        {
            _targetingTimeCD -= Time.deltaTime;
            if (_targetingTimeCD <= 0)
            {
                return true;
            }
            else
            {
                return isTargeted;
            }
        }
        else
        {
            _targetingTimeCD = targetingTime;
            return false;
        }
    }
    public void Cost()
    {
        resourceBase.resource -= costPrice;
        isActived = true;
    }

    public void PlayShootSound()
    {
        if(turretName == TurretName.LightGun)
        {
            if (soundCounter.turret_1_sounds == 0)
            {
                float volume = Random.Range(1, 1);
                audioSource.PlayOneShot(shootSound, volume);
                soundCounter.turret_1_sounds++;
                soundCounter.Invoke("turret_1_reduse", 0.3f);
            }
        }else if (turretName == TurretName.Sniper)
        {
            if (soundCounter.turret_2_sounds == 0)
            {
                float volume = Random.Range(1, 1);
                audioSource.PlayOneShot(shootSound, volume);
                soundCounter.turret_2_sounds++;
                soundCounter.Invoke("turret_2_reduse", 0.3f);
            }
        }
        else if (turretName == TurretName.Explosion)
        {
            if (soundCounter.turret_3_sounds == 0)
            {
                float volume = Random.Range(1, 1);
                audioSource.PlayOneShot(shootSound, volume);
                soundCounter.turret_3_sounds++;
                soundCounter.Invoke("turret_3_reduse", 0.3f);
            }
        }
    }

    public Transform GetFirePoints()
    {
        Transform point = firePoints[firePointsCount];
        firePointsCount++;
        if (firePointsCount == firePoints.Length) firePointsCount = 0;
        return point;
    }
}

public enum TurretName{
    LightGun,
    Sniper,
    Explosion,
    Gunmachine,
    Rocket
}
