using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour
{
    float timer;
    float interval = 0.5f;
    List<Transform> objectsToMove = new List<Transform>();
    List<Vector3> moveDirections = new List<Vector3>(); // Almacenará las direcciones de movimiento correspondientes

    void Start()
    {
        Transform overlayTransform = GameObject.Find("Overlay").transform;
        Transform overlayNearTransform = GameObject.Find("OverlayNear").transform;

        if (overlayTransform == null)
        {
            Debug.LogError("No se encontró el objeto Overlay en la escena.");
        }

        if (overlayNearTransform == null)
        {
            Debug.LogError("No se encontró el objeto OverlayNear en la escena.");
        }


        objectsToMove.Add(overlayTransform);
        objectsToMove.Add(overlayNearTransform);

        // Agrega las direcciones de movimiento correspondientes
        moveDirections.Add(Vector3.up);      // Mover "Overlay" hacia arriba
        moveDirections.Add(Vector3.down);    // Mover "OverlayNear" hacia abajo


        print("Thanks for buying this, if you need any support, email support@dilapidatedmeow.com. " +
            "Please note I cannot help with scripting related problems.");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            for (int i = 0; i < objectsToMove.Count; i++)
            {
                objectsToMove[i].Translate(moveDirections[i]); // Mueve en la dirección correspondiente
            }

            timer = 0;
        }
    }
}