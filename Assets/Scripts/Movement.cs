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
    private Transform cameraTrans;
    public float stuff;
    public float bound = 0f;
    void Start()
    {
        cameraTrans = GameObject.Find("Camera").transform; 
    }

    // Update is called once per frame
    void Update()
    {

        translationalVector.Set(FoB(Input.GetKey(rightMoveKey)) - FoB(Input.GetKey(leftMoveKey)), 0, FoB(Input.GetKey(frontMoveKey)) - FoB(Input.GetKey(backMoveKey)));
        transform.Translate(translationalVector * playerSpeed * Time.deltaTime);


        transform.Rotate(new Vector3(0, Input.GetAxis(horizontalLookInput)*Sensitivity,0));
        stuff = cameraTrans.eulerAngles.x;
        cameraTrans.Rotate(new Vector3(-Input.GetAxis(verticalLookInput) * Sensitivity, 0, 0));

    }
    //float of bool
    private float FoB(bool B)
    {
        return B ? 1f : 0f ;
    }

}
