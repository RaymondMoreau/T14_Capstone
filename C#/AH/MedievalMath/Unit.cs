using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed = 2.0f;  // Speed of the unit
    public int attackDamage = 1;  // Damage dealt per attack
    public float attackInterval = 1.0f;  // Time between attacks

    private Transform target;  // The target base
    private Unit currentEnemy;  // Reference to the enemy unit being attacked
    private Animator animator;  // Reference to Animator
    private BaseHealth targetBaseHealth; // Reference to the enemy base's health
    private float attackTimer = 0f;  // Timer to control attack intervals

    void Start()
    {
        // Assign the target base based on the unit's tag
        if (CompareTag("PlayerUnit"))
        {
            target = GameObject.Find("EnemyBase")?.transform;
        }
        else if (CompareTag("EnemyUnit"))
        {
            target = GameObject.Find("PlayerBase")?.transform;
        }

        if (target == null)
        {
            Debug.LogError($"{name}: Target base not found! Ensure the base objects are correctly named in the scene.");
        }
        else
        {
            targetBaseHealth = target.GetComponent<BaseHealth>();
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FindNewEnemy();

        if (currentEnemy != null)
        {
            // Engage in combat with an enemy unit
            StopMovement();
            AttackEnemy();
        }
        else if (targetBaseHealth != null && IsNearBase())
        {
            // Attack the base if no enemies are nearby
            StopMovement();
            AttackBase();
        }
        else
        {
            // Move towards the enemy base if no other targets are available
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Play walking animation
            if (animator != null)
            {
                animator.SetBool("IsWalking", true);
            }
        }
    }

    private void StopMovement()
    {
        // Stop walking animation
        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void AttackEnemy()
    {
        if (currentEnemy == null) return;

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            animator?.SetTrigger("StartAttack");
            Health enemyHealth = currentEnemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                Debug.Log($"{name} attacked {currentEnemy.name} for {attackDamage} damage.");
            }
            attackTimer = 0f;
        }
    }

    private void AttackBase()
    {
        if (targetBaseHealth == null) return;

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            animator?.SetTrigger("StartAttack");
            targetBaseHealth.TakeDamage(attackDamage);
            Debug.Log($"{name} attacked the base for {attackDamage} damage.");
            attackTimer = 0f;
        }
    }

    private void FindNewEnemy()
    {
        float closestDistance = Mathf.Infinity;
        Unit closestEnemy = null;

        // Use Physics2D.OverlapCircle to find nearby units
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f); // Adjust radius as needed
        foreach (Collider2D collider in colliders)
        {
            Unit potentialEnemy = collider.GetComponent<Unit>();
            if (potentialEnemy != null && potentialEnemy.CompareTag(OppositeTag()))
            {
                float distance = Vector3.Distance(transform.position, potentialEnemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = potentialEnemy;
                }
            }
        }

        if (closestEnemy != null)
        {
            currentEnemy = closestEnemy;
        }
        else
        {
            currentEnemy = null;
        }
    }

    private bool IsNearBase()
    {
        if (target == null) return false;

        // Check if the unit is close to the base
        float distanceToBase = Vector3.Distance(transform.position, target.position);
        return distanceToBase <= 0.5f; // Adjust this threshold as needed
    }

    private string OppositeTag()
    {
        return CompareTag("PlayerUnit") ? "EnemyUnit" : "PlayerUnit";
    }
}