using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 500.0f; //Rotation speed (deg/s)
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0,rotationSpeed * Time.deltaTime,0); 
    }
}
