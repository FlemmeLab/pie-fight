using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /*
    //event lié à l'action avancer
    public delegate void frontMoveDelegate(Movement t);
    public event frontMoveDelegate frontMoveEvent;

    //event lié à l'action reculer
    public delegate void backMoveDelegate(Movement t);
    public event backMoveDelegate backMoveEvent;

    //event lié à l'action aller à gauche
    public delegate void leftMoveDelegate(Movement t);
    public event leftMoveDelegate leftMoveEvent;

    //event lié à l'action aller à gauche
    public delegate void rightMoveDelegate(Movement t);
    public event rightMoveDelegate rightMoveEvent;
    */

    public string frontMoveKey = "z";
    public string backMoveKey = "s";
    public string leftMoveKey = "q";
    public string rightMoveKey = "d";
    public string jumpKey = "space";

    public string horizontalLookInput = "Mouse X";
    public string verticalLookInput = "Mouse Y";
    

    private float playerSpeed = 5f;
    private Vector3 jumpForce = new Vector3(0, 5f, 0);

    public Vector3 translationalVector = Vector3.zero;
    public Vector3 rotationalVector = Vector3.zero;

    private float Sensitivity = 5f;
    // Start is called before the first frame update

    private float bottomClamp = -90f;
    private float topClamp = 90f; 
    private Transform aimTrans;
    private float rotX;
    void Start()
    {
        aimTrans = GameObject.Find("Aim").transform; 
    }

    // Update is called once per frame
    void Update()
    {

        translationalVector.Set(FoB(Input.GetKey(rightMoveKey)) - FoB(Input.GetKey(leftMoveKey)), 0, FoB(Input.GetKey(frontMoveKey)) - FoB(Input.GetKey(backMoveKey)));
        transform.Translate(translationalVector * playerSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.GetAxis(horizontalLookInput)*Sensitivity,0));

        rotX -= Input.GetAxis(verticalLookInput) * Sensitivity ;

        // clamp our pitch rotation
        rotX = ClampAngle(rotX, bottomClamp, topClamp);

        // Update Cinemachine camera target pitch
        aimTrans.transform.localRotation = Quaternion.Euler(rotX, 0.0f, 0.0f);

    }
    //float of bool
    private float FoB(bool B)
    {
        return B ? 1f : 0f ;
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

}
