using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool unitActive = false;
    public int maxHealth;
    public int currentHealth;
    public int killPrice;
    public float speed = 2;
    public float rotationSpeed = 5;
    private Transform target;
    private int wavepointIndex = 0;
    public Waypoints waypoints;
    public Health health;
    public bool isDied = false;
    Animator animator;
    public Transform Cursor;
    public Transform MissedPoints;
    public HealthBar healthBar;
    [HideInInspector] public ResourceBase resource;
    public Transform CoinBar;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        animator = GetComponent<Animator>();
        target = waypoints.points[0];
    }


    void Update()
    {
        if (isDied)
        {
            healthBar.transform.localScale = Vector3.Lerp(healthBar.transform.localScale,Vector3.zero,Time.deltaTime*15);
            animator.SetBool("isDied", true);
            return;
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }

        Vector3 relativePos = target.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        
    }

    private void GetNextWaypoint()
    {
        if(wavepointIndex >= waypoints.points.Length - 1)
        {
            health.TakeHealth();
            Destroy(gameObject);
            return;
        }
        else
        {
            unitActive = true;
        }
        wavepointIndex++;
        target = waypoints.points[wavepointIndex];
    }

    public Transform GetMissedPoint()
    {
        int length = MissedPoints.childCount;
        int randomIndex = Random.Range(0, length);
        return MissedPoints.GetChild(randomIndex);
    }

    public void TakeDamage(int damage)
    {
        if (isDied) return;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            isDied = true;
            gameObject.tag = "Ignore";
            gameObject.layer = LayerMask.NameToLayer("Default");
            resource.AddResource(killPrice);

            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            Transform coinBar = Instantiate(CoinBar, pos, CoinBar.rotation, healthBar.transform.parent);
            ScreenSpace screenSpace = coinBar.GetComponent<ScreenSpace>();
            screenSpace.target = transform;
            if (screenSpace.text)
            {
                screenSpace.text.text = "+" + killPrice;
            }
            Destroy(gameObject, 5.5f);
        }
    }
}
