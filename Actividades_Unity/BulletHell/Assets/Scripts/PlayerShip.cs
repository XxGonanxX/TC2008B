using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speed = 150f; // Velocidad de movimiento de la nave
    public float reducedSpeed = 75f;
    

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = reducedSpeed; // Asignar la velocidad reducida
        }
        else
        {
            speed = 150f; // Asignar la velocidad original
        }

        // Obtener la entrada del jugador en los ejes horizontal y vertical
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular la dirección de movimiento
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;

        // Calcular la nueva posición de la nave
        Vector3 newPosition = transform.position + movement;

        // Obtener los límites de la cámara en unidades del mundo
        Camera mainCamera = Camera.main;
        float camHeight = 18f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        float xMin = mainCamera.transform.position.x - camWidth / 2;
        float xMax = mainCamera.transform.position.x + camWidth / 2;
        float yMin = mainCamera.transform.position.y - camHeight / 4;
        float yMax = mainCamera.transform.position.y + camHeight / 2;

        // Restringir la posición de la nave dentro de los límites de la cámara
        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);

        // Actualizar la posición de la nave
        transform.position = newPosition;
    }
}
