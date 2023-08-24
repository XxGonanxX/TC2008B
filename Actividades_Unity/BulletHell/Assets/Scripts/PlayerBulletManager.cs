using System.Collections;
using UnityEngine;

public class PlayerBulletManager : MonoBehaviour
{
    public GameObject playerBulletPrefab; // El Prefab de la bala del jugador
    public Transform fireSpot; // Cambiado el nombre del punto de disparo a FireSpot
    public float fireRate = 0.2f;
    public float moveSpeed = 10f; // Ajusta la velocidad según tus necesidades

    private float nextFireTime = 0f;

    private void Start()
    {
        nextFireTime = Time.time + fireRate;
    }

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(playerBulletPrefab, fireSpot.position, fireSpot.rotation);
        Vector2 bulletDirection = Vector2.up; // Asegúrate de que el vector apunte hacia arriba
        bullet.GetComponent<PlayerBullet>().SetMoveDirection(bulletDirection.normalized * moveSpeed);


    }

   
}

