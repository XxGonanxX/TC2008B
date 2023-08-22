using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMovement : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;

    private enum FireMode { Circle, Star, Current }
    private FireMode currentFireMode = FireMode.Current;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeFireMode", 10f, 10f);
        InvokeRepeating("Fire", 0f, 2f);
    }

    private void Fire()
    {
        float angleStep = 0f;
        float startAngle = 0f;

        switch (currentFireMode)
        {
            case FireMode.Circle: 
                angleStep = 360f / bulletsAmount;
                break;
            case FireMode.Star: // No tan bueno, cambiar
                angleStep = 360f / 5;
                startAngle = Random.Range(0f, 360f);
                break;
            case FireMode.Current: // No funciona, corregir
                angleStep = 360f / (bulletsAmount - 1); // Distribución en 180 grados
                startAngle = 180f - (angleStep * (bulletsAmount - 1)) / 2; // Comenzar en el ángulo correcto para disparar la mitad
                break;
        }

        for (int i = 0; i < bulletsAmount; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin(Mathf.Deg2Rad * startAngle);
            float bulDirY = transform.position.y + Mathf.Cos(Mathf.Deg2Rad * startAngle);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            startAngle += angleStep;
        }
    }

    private void ChangeFireMode()
    {
        currentFireMode = (FireMode)(((int)currentFireMode + 1) % System.Enum.GetValues(typeof(FireMode)).Length);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
