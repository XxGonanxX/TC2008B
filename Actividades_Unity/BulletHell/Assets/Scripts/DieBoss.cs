using UnityEngine;

public class DieBoss : MonoBehaviour
{
    public int damageAmount = 1; // Cantidad de daño que inflige la bala

    private void Start()
    {
        Destroy(gameObject, 5f); // Destruir la bala después de 5 segundos
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BossHealth bossHealth = other.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damageAmount); // Aplicar el daño al jefe
            }

            Destroy(gameObject); // Destruir la bala después de impactar
        }
    }
}
