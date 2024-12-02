using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health for the base
    private int currentHealth;

    private GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        currentHealth = maxHealth;

        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();

        // Check if GameManager exists
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene. Please ensure there is a GameManager object.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{name} took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            DestroyBase();
        }
    }

    private void DestroyBase()
    {
        Debug.Log($"{name} has been destroyed!");

        // Notify the GameManager about the game outcome
        if (gameManager != null)
        {
            if (CompareTag("PlayerBase"))
            {
                gameManager.EndGame(false); // Player loses
            }
            else if (CompareTag("EnemyBase"))
            {
                gameManager.EndGame(true); // Player wins
            }
        }

        Destroy(gameObject); // Optionally destroy the base
    }
}