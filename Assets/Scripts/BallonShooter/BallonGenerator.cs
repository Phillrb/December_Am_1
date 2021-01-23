using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonGenerator : MonoBehaviour
{
    public GameObject ballonPrefab;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        float randomWaitTime;
        while(true)
        {
            Instantiate(ballonPrefab, transform.position + Vector3.up, Quaternion.identity);

            randomWaitTime = Random.Range(minWaitTime, maxWaitTime);

            yield return new WaitForSeconds(randomWaitTime);
        }
    }
}
