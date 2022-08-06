using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensX = 100.0f;
    [SerializeField] private float sensY = 100.0f;

    Transform aimTrans;
    //Transform cam;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        aimTrans = transform.Find("Aim");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

        aimTrans.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        //aimTrans.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
    }
}

