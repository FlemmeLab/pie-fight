using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed; //Rotation speed (deg/s)
    private float deltaAng;
    void Start()
    {
        deltaAng = rotationSpeed * Time.deltaTime; 
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0,deltaAng,0); 
    }
}
