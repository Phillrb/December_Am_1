using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationZone : MonoBehaviour
{
    public Transform snapRef;
    
    public Vector3 GetSnapPos()
    {
        return snapRef.position;
    }
}
