using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openSpeed = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine( OpenDoor() );
        }
    }

    private IEnumerator OpenDoor()
    {
        float currentTime = 0;

        while(currentTime < 1)
        {
            transform.rotation = Quaternion.Euler( 0, Mathf.Lerp( 0, 70, currentTime ), 0 );

            yield return null;

            currentTime += Time.deltaTime / openSpeed;
        }
    }
}
