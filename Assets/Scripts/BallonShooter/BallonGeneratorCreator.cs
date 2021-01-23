using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonGeneratorCreator : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject ballonGeneratorPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CreateGenerator();
        }
    }

    public void CreateGenerator()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(ray, out hitInfo, 100, layerMask))
        {
            // I have hit ground

            //Instantiate(ballonSpawnerPrefab, hitInfo.point + (Vector3.up * 0.125f), Quaternion.identity);
            Instantiate(ballonGeneratorPrefab, hitInfo.point, Quaternion.identity);
        }
    }
}
