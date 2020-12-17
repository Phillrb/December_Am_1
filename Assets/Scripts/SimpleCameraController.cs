using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 3f;

    public float smoothFollow = 0.1f;
    public Transform followSphere;

    public Transform camera;
    public Vector3 offsetVector;
    // Start is called before the first frame update
    void Start()
    {
        offsetVector = camera.position - transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.position = transform.position + ( transform.forward * speed * Time.deltaTime );
        }
        if( Input.GetKey( "s" ) )
        {
            transform.position = transform.position - ( transform.forward * speed * Time.deltaTime );
        }
        if( Input.GetKey( "d" ) )
        {
            transform.position = transform.position + ( transform.right * speed * Time.deltaTime );
        }
        if( Input.GetKey( "a" ) )
        {
            transform.position = transform.position - ( transform.right * speed * Time.deltaTime );
        }


        transform.Rotate( 0, Input.GetAxis( "Mouse X" ) * mouseSensitivity, 0, Space.World );
        transform.Rotate( -Input.GetAxis( "Mouse Y" ) * mouseSensitivity, 0, 0, Space.Self );


        camera.LookAt( transform );
        Vector3 desiredPosition = followSphere.position;
        camera.position += (desiredPosition - camera.position) * smoothFollow;
    }
}
