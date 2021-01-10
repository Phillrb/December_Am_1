using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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

    public float curverHeight = 2;
    public int positionCount = 10;

    public Transform teleportEffect;
    public float smoothnessValue = 10;

    public RawImage fadeScreen;
    public float fadeDuration = 0.8f;
    public float waitTime = 0.5f;

    private bool teleportLock = false;

    public struct SumInfo
    {
        public bool isGreaterThan5;
        public float sum;

    }

    public void Start()
    {
        lR.positionCount = positionCount;
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

            Vector3 startPoint = transform.position;

            TeleportationZone teleZone = hitInfo.collider.GetComponent<TeleportationZone>();

            Vector3 desiredPoint;
            if (teleZone != null)
            {
                desiredPoint = teleZone.GetSnapPos();
            }
            else
            {
                desiredPoint = hitInfo.point;
            }


            Vector3 vecToDesired = desiredPoint - teleportEffect.position;
            vecToDesired /= smoothnessValue;
            vecToDesired *= Time.deltaTime;
            Vector3 endPoint = vecToDesired + teleportEffect.position;


            teleportEffect.gameObject.SetActive(true);
            teleportEffect.position = endPoint;


            Vector3 midPoint = ((endPoint - startPoint) / 2f) + startPoint;
            midPoint.y += curverHeight;

            for (int i = 0; i < positionCount; i = i + 1)
            {
                Vector3 lerp1 = Vector3.Lerp(startPoint, midPoint, i / (float)positionCount);
                Vector3 lerp2 = Vector3.Lerp(midPoint, endPoint, i / (float)positionCount);
                Vector3 curvedPos = Vector3.Lerp(lerp1, lerp2, i / (float)positionCount);

                lR.SetPosition(i, curvedPos);
            }

            if (Input.GetButtonDown(handness.ToString() + "TriggerPress") && teleportLock == false)
            {
                StartCoroutine(FadeTeleport(endPoint));
            }
        }
        else
        {
            lR.enabled = false;
            teleportEffect.gameObject.SetActive(false);
        }
    }

    public IEnumerator FadeTeleport(Vector3 posToTeleTo)
    {
        teleportLock = true;

        // Initialize our state
        float currentTime = 0;
        fadeScreen.enabled = true;
        Color black = Color.black;
        Color clear = Color.clear;
        fadeScreen.color = clear;

        // Start our loop, and loop until time is up
        while(currentTime < 1)
        {
            // Fade the screen i.e. start making the screen black
            fadeScreen.color = Color.Lerp(clear, black, currentTime);

            // Wait one frame
            yield return null;

            // Increase Current time
            currentTime += Time.deltaTime / fadeDuration;

            // Loop again
        }

        // Make sure screen is fully black
        fadeScreen.color = black;

        // Wait just a little bit and teleport
        yield return new WaitForSeconds(waitTime);
        XRRig.position = posToTeleTo;

        // Then start fading back in

        // Initialize our state
        currentTime = 0;

        // Start our loop, and loop until time is up
        while (currentTime < 1)
        {
            // Fade the screen i.e. start making the screen black
            fadeScreen.color = Color.Lerp(black, clear, currentTime);

            // Wait one frame
            yield return null;

            // Increase Current time
            currentTime += Time.deltaTime / fadeDuration;

            // Loop again
        }

        // Make sure screen is fully black
        fadeScreen.color = clear;
        fadeScreen.enabled = false;

        teleportLock = false;

    }
}
