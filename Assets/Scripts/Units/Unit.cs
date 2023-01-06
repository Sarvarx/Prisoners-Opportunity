using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int killPrice;
    public float speed = 2;
    private Transform target;
    private int wavepointIndex = 0;
    public Waypoints waypoints;
    public bool isDied = false;
    Animator animator;
    public Transform Cursor;
    public Transform MissedPoints;
    public HealthBar healthBar;
    [HideInInspector] public ResourceBase resource;
    
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
            animator.SetBool("isDied", true);
            return;
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        Vector3 relativePos = target.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 5 * Time.deltaTime);
        
    }

    private void GetNextWaypoint()
    {
        if(wavepointIndex >= waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
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
            resource.AddResource(killPrice);
            Destroy(gameObject, 5.5f);
        }
    }
}
