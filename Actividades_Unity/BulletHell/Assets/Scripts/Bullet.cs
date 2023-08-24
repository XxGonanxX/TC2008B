using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke("DeactivateAfterDelay", 5f);
    }

    private void DeactivateAfterDelay()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 100f;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("BossBullets"), LayerMask.NameToLayer("BossBullets"));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Reduce la salud del jugador en 1
            }

            // Desactiva la bala después de colisionar con el jugador
            gameObject.SetActive(false);
        }
    }
}
