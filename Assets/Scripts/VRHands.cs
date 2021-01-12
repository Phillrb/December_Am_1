using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Handness
{
    Left,
    Right,
}

public class VRHands : MonoBehaviour
{
    public Animator anim;
    public Handness handness = Handness.Left;

    public Transform hoveredObject = null;
    public Transform heldObjectRef;

    private Vector3 lastPos;

    private void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(handness.ToString() + "GripPress"))
        {
            anim.SetBool("FistClosed", true);
            if (hoveredObject != null)
            {
                //Pick up
                hoveredObject.SetParent(heldObjectRef);
                hoveredObject.localPosition = Vector3.zero;

                hoveredObject.GetComponent<Rigidbody>().useGravity = false;
                hoveredObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        if (Input.GetButtonUp(handness.ToString() + "GripPress"))
        {
            anim.SetBool("FistClosed", false);

            // Drop Object
            if (hoveredObject != null)
            {
                hoveredObject.SetParent(null);

                hoveredObject.GetComponent<Rigidbody>().useGravity = true;
                hoveredObject.GetComponent<Rigidbody>().isKinematic = false;
                hoveredObject.GetComponent<Rigidbody>().velocity = (transform.position - lastPos) / Time.deltaTime;
            }
        }

        lastPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("I just touched something!");
        if (other.tag == "Interactable")
        {
            hoveredObject = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("I just touched something!");
        if (other.transform == hoveredObject)
        {
            hoveredObject = null;
        }
    }
}
