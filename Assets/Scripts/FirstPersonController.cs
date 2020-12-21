using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController: MonoBehaviour
{
    public float speed = 5f;
    public float rotSpeed = 3f;
    
    private float currentXRot = 0;
    public float verticalRotLimit = 60f;

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

        
        currentXRot += -Input.GetAxis( "Mouse Y" ) * rotSpeed;
        currentXRot = Mathf.Clamp( currentXRot, -verticalRotLimit, verticalRotLimit );
        transform.rotation = Quaternion.Euler( currentXRot, transform.eulerAngles.y, transform.eulerAngles.z );

        transform.Rotate( 0, Input.GetAxis("Mouse X") * rotSpeed, 0, Space.World );
        //cam.Rotate( -Input.GetAxis( "Mouse Y" ) * rotSpeed, 0, 0, Space.Self );

    }
}
