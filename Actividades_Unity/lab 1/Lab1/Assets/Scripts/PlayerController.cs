using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    //Vehicle speed
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Mover vehículo hacia adelante
        //transform.Translate(0,0,1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
}
