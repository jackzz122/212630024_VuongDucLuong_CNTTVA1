using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;  
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camera = transform.position;
        camera.x = player.position.x;
        transform.position = camera;
    }
}
