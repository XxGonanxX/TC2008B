using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public int speed;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Movimiento", 12.0f);
    }

    private void Movimiento()
    {
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) {
        // Now the object will move forward
        transform.Translate( Vector3.forward * speed * Time.deltaTime);
        }
    }
}
