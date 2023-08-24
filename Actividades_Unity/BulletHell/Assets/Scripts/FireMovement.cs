using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMovement : MonoBehaviour
{
    [SerializeField]
    public int bulletsAmount = 15;

    private enum FireMode { Circle, Star, Current }
    private FireMode currentFireMode = FireMode.Current;

    private bool bossIsDead = false;

    // Reference to the boss's fire point
    public Transform bossFirePoint;

    void Start()
    {
        InvokeRepeating("ChangeFireMode", 10f, 10f);
     
        InvokeRepeating("Fire", 0f, 0.5f);

    }

    private void Fire()
    {
        if (bossIsDead)
        {
            return;
        }

        float angleStep = 0f;
        float startAngle = 0f;

        switch (currentFireMode)
        {
            case FireMode.Circle:
                angleStep = (360f / bulletsAmount);
                Quaternion rotation = Quaternion.AngleAxis(angleStep, Vector3.forward);

                for (int i = 0; i < bulletsAmount; i++)
                {
                    Vector2 bulDir = rotation * Vector2.up;

                    GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                    if (bul != null)
                    {
                        bul.transform.position = bossFirePoint.position; // Use bossFirePoint position
                        bul.SetActive(true);
                        bul.GetComponent<Bullet>().SetMoveDirection(bulDir);
                    }

                    rotation *= Quaternion.AngleAxis(angleStep, Vector3.forward);
                }
                break;

            case FireMode.Star:
                angleStep = 360f / 5;
                startAngle = Random.Range(0f, 360f);

                for (int i = 0; i < bulletsAmount; i++)
                {
                    Vector2 bulDir = Quaternion.AngleAxis(startAngle, Vector3.forward) * Vector2.up;

                    GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                    if (bul != null)
                    {
                        bul.transform.position = bossFirePoint.position;
                        bul.SetActive(true);
                        bul.GetComponent<Bullet>().SetMoveDirection(bulDir);
                    }

                    startAngle += angleStep;
                }
                break;

            case FireMode.Current:
                angleStep = 200f / (bulletsAmount) - 5 ;
                startAngle = Random.Range(0f, 180f);

                for (int i = 0; i < bulletsAmount; i++)
                {
                    Vector2 bulDir = Quaternion.AngleAxis(startAngle, Vector3.forward) * Vector2.up;

                    GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                    if (bul != null)
                    {
                        bul.transform.position = bossFirePoint.position;
                        bul.SetActive(true);
                        bul.GetComponent<Bullet>().SetMoveDirection(bulDir);
                    }

                    startAngle += angleStep;
                }
                break;
        }
    }

    public void BossDied()
    {
        bossIsDead = true;
    }

    private void ChangeFireMode()
    {
        currentFireMode = (FireMode)(((int)currentFireMode + 1) % System.Enum.GetValues(typeof(FireMode)).Length);
    }

    void Update()
    {
        if (BulletPool.bulletPoolInstance.GetBullet() == null)
        {
            // Quiero que siga disparando las balas, que le regrese a la pool las balas que ya no se usan
            // y que no se quede sin balas
            GameObject bul = Instantiate(BulletPool.bulletPoolInstance.pooledBullet);
            bul.SetActive(false);
            BulletPool.bulletPoolInstance.bullets.Add(bul);

        }

    }
}