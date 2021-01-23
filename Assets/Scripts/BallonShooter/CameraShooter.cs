using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShooter : MonoBehaviour
{
    public float mouseSensitivity = 3;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0, Space.World );
        transform.Rotate(-Input.GetAxis("Mouse Y") * mouseSensitivity, 0, 0,  Space.Self);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo, 100, layerMask))
            {
                Destroy(hitInfo.collider.gameObject);
            }
        }
    }
}
