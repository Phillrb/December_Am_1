using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float minSpeed = 1f;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        GetComponent<MeshRenderer>().material.SetColor("_Color", Color.HSVToRGB(Random.value, 1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
    }
}
