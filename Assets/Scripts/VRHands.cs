using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VRHands : MonoBehaviour
{
    public enum Handness
    {
        Left,
        Right,
    }

    public Animator anim;
    public Handness handness = Handness.Left;
    public Transform XRRig;
    public LineRenderer lR;

    public struct SumInfo
    {
        public bool isGreaterThan5;
        public float sum;

    }

    public void Start()
    {
        lR.positionCount = 2;
        lR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(handness.ToString() + "GripPress"))
        {
            anim.SetBool("FistClosed", true);
        }
        if (Input.GetButtonUp(handness.ToString() + "GripPress"))
        {
            anim.SetBool("FistClosed", false);
        }

        //Ray ray = ;
        //RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo))
        {
            lR.enabled = true;

            lR.SetPosition(0, transform.position);
            lR.SetPosition(1, hitInfo.point);

            if (Input.GetButtonDown(handness.ToString() + "TriggerPress"))
            {
                XRRig.position = hitInfo.point;
            }
        }
        else
        {
            lR.enabled = false;
        }
    }
}
