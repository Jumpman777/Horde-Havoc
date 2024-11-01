using UnityEngine;

public class Defender : MonoBehaviour
{
    public float damage = 5f;
    public float attackRate = 1f;
    private float nextAttackTime = 0f;

    private HealthBar healthBar;
    private Transform target;
    private HealthBar targetHealthBar;

    void Start()
    {
        healthBar = GetComponent<HealthBar>();
    }

    void Update()
    {
        FindTarget();

        if (target != null && Time.time >= nextAttackTime)
        {
            if (IsInRange(target))
            {
                nextAttackTime = Time.time + 1f / attackRate;
                Attack(targetHealthBar);
            }
        }
    }

    private void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                target = hitCollider.transform;
                targetHealthBar = hitCollider.GetComponent<HealthBar>();
                break;
            }
        }
    }

    private bool IsInRange(Transform target)
    {
        return Vector3.Distance(transform.position, target.position) <= 1.5f;
    }

    private void Attack(HealthBar targetHealthBar)
    {
        if (targetHealthBar != null)
        {
            targetHealthBar.TakeDamage(damage);
        }
    }

    public void TakeDamage(float amount)
    {
        if (healthBar != null)
        {
            healthBar.TakeDamage(amount);
            if (healthBar.currentHealth <= 0)
            {
                FindObjectOfType<DefenderSpawner>().OnDefenderDestroyed();
                Destroy(gameObject);
            }
        }
    }
}









