using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour

{
    //Se llaba el objeto player
    public GameObject player;

    //Se ajusta la cámara a cierta altura.
    private Vector3 offset = new Vector3(0,6,-7);
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void LateUpdate()
    {
        //Se ajusta la posición de la cámara al jugador, esta lo seguirá siempre.
        transform.position = player.transform.position + offset;
    }
}
