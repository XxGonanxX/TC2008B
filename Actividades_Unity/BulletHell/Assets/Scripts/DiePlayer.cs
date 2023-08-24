using UnityEngine;

public class DiePlayer : MonoBehaviour
{
    public int damageAmount = 1; // Cantidad de daño que inflige la bala

    private void Start()
    {
      //  Destroy(gameObject, 5f); // Destruir la bala después de 5 segundos
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Aplicar el daño al jugador
            }

            Destroy(gameObject); // Destruir la bala después de impactar
        }
    }
}
