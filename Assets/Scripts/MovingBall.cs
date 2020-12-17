using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingBall : MonoBehaviour
{
    public float m_Speed = 0.01f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        float speed2 = 3;

        transform.position = transform.position + ( -transform.forward * m_Speed );
        speed2 += 4;
        if( Input.GetKeyDown( "space" ) )
        {
            m_Speed = m_Speed * -1;

        }
    }

    
}