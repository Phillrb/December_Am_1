using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRShooter : MonoBehaviour
{
    public Handness handness = Handness.Left;
    public GameObject fireballPrefab;
    public Transform shootRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(handness + "TriggerPress"))
        {
            // Trigger was pressed
            Instantiate(fireballPrefab, shootRef.position, shootRef.rotation);
        }
    }
}
