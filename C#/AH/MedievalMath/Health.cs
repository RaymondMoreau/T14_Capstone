using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false; // Prevents multiple death triggers

    public delegate void UnitDefeated(GameObject defeatedUnit);
    public static event UnitDefeated OnUnitDefeated;

    private Animator animator; // Reference to Animator
    private Collider2D collider2D; // Reference to the Collider

    void Start()
    {
        currentHealth = maxHealth;

        // Get the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning($"No Animator attached to {name}. Death animation will not play.");
        }

        // Get the Collider component
        collider2D = GetComponent<Collider2D>();
        if (collider2D == null)
        {
            Debug.LogWarning($"No Collider attached to {name}. Other units may collide unnecessarily.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Ignore damage if already dead

        currentHealth -= damage;
        Debug.Log($"{name} took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // Prevent multiple calls
        isDead = true;

        Debug.Log($"{name} has been defeated.");

        // Notify listeners about the defeated unit
        OnUnitDefeated?.Invoke(gameObject);

        // Disable the Collider to avoid blocking other units
        if (collider2D != null)
        {
            collider2D.enabled = false;
        }

        // Play death animation if Animator is available
        if (animator != null)
        {
            animator.SetTrigger("StartDeath"); // Trigger the death animation
        }

        // Destroy the unit after a short delay
        Destroy(gameObject, 1.5f); // Adjust this delay to match the animation length
    }
}