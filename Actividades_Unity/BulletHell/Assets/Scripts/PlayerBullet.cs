using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Vector2 moveDirection;
    public float moveSpeed = 10f;

    private void Start()
    {
        Destroy(gameObject, 3f); // Destruir la bala después de 3 segundos
    }

    private void Update()
    {
        MoveBullet();
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void MoveBullet()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss")) // Verificar si colisiona con el jefe
        {
            BossHealth bossHealth = collision.GetComponent<BossHealth>();
            if (bossHealth != null) // Verificar si el componente BossHealth está presente
            {
                bossHealth.TakeDamage(1); // Reducir la salud del jefe en 1
                Destroy(gameObject); // Destruir la bala al impactar
            }
        }
    }
}
