using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TapToSpawn : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject ballonGeneratorPrefab;
    public float doubleTapDistancePercentageLimit = 0.2f;

    private bool isWaitingForSecondTouch = false;
    public float doubleTapDelay = 0.5f;

    public Vector2 lastTouchPos;

    // Update is called once per frame
    void Update()
    {
        // if input button
            // create a ray
            // create a hit info
            // if Raycast()
                // Instantiate at the hitinfo.point

        // Is there anything touching the screen
        if (Input.touchCount > 0)
        {
            // Get information about the first thing touching the screen
            Touch touch = Input.GetTouch(0);

            // Did the touch just start i.e. OnTouchDown
            if (touch.phase == TouchPhase.Began)
            {
                //SpawnBallonGenerator(touch);
                if (isWaitingForSecondTouch)
                {
                    float distance = Vector2.Distance(lastTouchPos, touch.position);
                    float distancePercentage = distance / (new Vector2(Screen.width, Screen.height)).magnitude;

                    if (distancePercentage < doubleTapDistancePercentageLimit)
                    {
                        DoubleTapRegistered();
                        StopAllCoroutines();
                        isWaitingForSecondTouch = false;
                    }
                }
                else
                {
                    isWaitingForSecondTouch = true;
                    StartCoroutine(DoubleTapWaitTimer());
                }

                lastTouchPos = touch.position;
            }
        }
    }

    private IEnumerator DoubleTapWaitTimer()
    {
        yield return new WaitForSeconds(doubleTapDelay);

        SingleTapRegistered();
        isWaitingForSecondTouch = false;

    }

    private void SingleTapRegistered()
    {

    }

    private void DoubleTapRegistered()
    {
        SpawnBallonGenerator(lastTouchPos);
    }

    private void SpawnBallonGenerator(Vector2 pos)
    {
        if (raycastManager.Raycast(pos, hits))
        {
            Pose pose = hits[0].pose;
            Instantiate(ballonGeneratorPrefab, pose.position, pose.rotation);
        }
    }
}
