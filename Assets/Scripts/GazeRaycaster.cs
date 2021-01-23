using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeRaycaster : MonoBehaviour
{
    public Transform XRRig;

    public Transform teleportEffect;
    public Image gazeBar;

    public RawImage fadeScreen;
    public float fadeDuration = 0.8f;
    public float waitTime = 0.5f;

    private bool teleportLock = true;

    public TeleportationZone currentTeleZone = null;

    public float currentGazeTimer = 0;
    public float gazeDelay = 2f;

    public float startDelay = 10f;


    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        teleportLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo))
        {
            TeleportationZone teleZone = hitInfo.collider.GetComponent<TeleportationZone>();

            // Still looking at the same zone
            // So increase timer
            if (teleZone == currentTeleZone && hitInfo.distance > 2 && teleportLock == false)
            {
                // Tick the timer
                currentGazeTimer += Time.deltaTime;
                gazeBar.fillAmount = currentGazeTimer / gazeDelay;

                // If timer reaches end, then stop ticking and teleport user
                if (currentGazeTimer >= gazeDelay )
                {
                    StartCoroutine(FadeTeleport(currentTeleZone.GetSnapPos()));
                    EndGazeTimer();
                }
            }

            // Make sure we are looking at a tele zone
            // This means we are looking at a new zone
            else if (teleZone != null)
            {
                // Set new looked at zone
                currentTeleZone = teleZone;

                // Move the effect
                teleportEffect.gameObject.SetActive(true);
                teleportEffect.position = teleZone.GetSnapPos();

                // Start gaze timer
                currentGazeTimer = 0;
                gazeBar.fillAmount = 0;
            }

            // This means we are no longer looking at anything
            // But we are still increasing gaze timer so time to stop it
            else if (currentTeleZone != null)
            {
                EndGazeTimer();

            }

            // Do nothing
            else
            {

            }
        }
        else
        {

            EndGazeTimer();
        }
    }

    private void EndGazeTimer()
    {
        teleportEffect.gameObject.SetActive(false);

        // Stop ticking gaze timer
        currentTeleZone = null;

        // Stop timer
        teleportEffect.gameObject.SetActive(false);
        currentGazeTimer = -1;
        gazeBar.fillAmount = 0;
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
        while (currentTime < 1)
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
