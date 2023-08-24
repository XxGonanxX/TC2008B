using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3; // Privado pero visible en el Inspector
    private int currentHealth;

    public int CurrentHealth => currentHealth; // Propiedad para acceder a la salud actual

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destruir la nave cuando la salud llega a cero
            FindObjectOfType<YouLose>().ShowLoseMessage();
        }
    }
}
