
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Configurable par le joueur : 

    // Sensitivité de la souris
    public float Sensitivity = 5f;

    //Configurable depuis un menu?
    public string frontMoveKey = "z";
    public string backMoveKey = "s";
    public string leftMoveKey = "q";
    public string rightMoveKey = "d";
    public string jumpKey = "space";

    public string horizontalLookInput = "Mouse X";
    public string verticalLookInput = "Mouse Y";

    //-----------------

    //event lié à la mise à jour de la position du joueur 
    public delegate void MoveDelegate(Movement t);
    public event MoveDelegate moveEvent;

    //Vitesse du joueur 
    public float playerSpeed = 5f; 

    //Force de saut 
    private float jumpForce = 5f;

    private float jumpInterval = 0.1f;
    private float jumpTimeRemaining = 0f;


    //Vecteur de déplacement qui correspond à l'appuis sur les touches de déplacement (ex : (-1,0,1) pour q,z)
    //Je n'aime pas cette methode (méthode arithmétique?)

    public Vector3 translationalVector = Vector3.zero;

    private static float sin_pi_4 = 0.70710678118f;
    private static Vector3 direction_left = new Vector3(-1, 0, 0);
    private static Vector3 direction_up = new Vector3(0, 0, 1);
    private static Vector3 direction_right = new Vector3(1, 0, 0);
    private static Vector3 direction_down = new Vector3(0, 0, -1);
    private static Vector3 direction_up_left = new Vector3(-sin_pi_4, 0, sin_pi_4); 
    private static Vector3 direction_up_right = new Vector3(sin_pi_4, 0, sin_pi_4);
    private static Vector3 direction_down_left = new Vector3(-sin_pi_4, 0, -sin_pi_4);
    private static Vector3 direction_down_right = new Vector3(sin_pi_4, 0, -sin_pi_4);

    //Objet comprenant la camera et le lanceur de projectile qui nécessitent d'être orientés dans la même direction
    private Transform aimTrans;
    
    //Rotation selon l'axe x du joueur
    private float rotX;

    //Limites supérieures et inférieure de la vue verticale
    private float bottomClamp = -90f;
    private float topClamp = 90f;

    //Ground
    private Transform groundTrans;

    void Start()
    {
        aimTrans = GameObject.Find("Aim").transform;
        groundTrans = GameObject.Find("Ground").transform;
        //On s'abonne à l'event
        moveEvent += onMove ;

        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey || Input.GetAxis(verticalLookInput) != 0 || Input.GetAxis(horizontalLookInput) != 0)
        {
            if (moveEvent != null)
                moveEvent(this);
        }
        if (jumpTimeRemaining > 0)
            jumpTimeRemaining -= Time.deltaTime;
        if (jumpTimeRemaining < 0)
            jumpTimeRemaining = 0;

    }



    //Int of bool
    private int IoB(bool B)
    {
        return B ? 1 : 0 ;
    }

    // fonction qui retourne lfAngle si lfMax > lfAngle > lfMin, lfMin si lfAngle <= lfMin ou lfMax si lfAngle >= lfMax
    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private Vector3 DirectionInput(int input)
    {
        Debug.Log("Input: " + input); 
        switch (input)
        {
            case 1:
                return direction_right;
            case 2:
                return direction_up;
            case 3:
                return direction_up_right;
            case 4:
                return direction_left;
            case 6:
                return direction_up_left;
            case 8:
                return direction_down;
            case 9:
                return direction_down_right;
            case 12:
                return direction_down_left;
            default:
                return Vector3.zero; 
        }
    }

    private void onMove(Movement t)
    {
        Debug.Log(jumpTimeRemaining);
        if ( ( groundTrans.position.y >= (transform.position.y - transform.localScale.y) ) && (jumpTimeRemaining == 0f) && Input.GetKey(jumpKey)) {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jumpTimeRemaining = jumpInterval; 
        }
        int movement_code = IoB(Input.GetKey(rightMoveKey)) + 2 * IoB(Input.GetKey(frontMoveKey)) + 4 * IoB(Input.GetKey(leftMoveKey)) + 8 * IoB(Input.GetKey(backMoveKey));         
        transform.Translate(DirectionInput(movement_code) * playerSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.GetAxis(horizontalLookInput) * Sensitivity, 0));

        rotX -= Input.GetAxis(verticalLookInput) * Sensitivity;

        // clamp our pitch rotation
        rotX = ClampAngle(rotX, bottomClamp, topClamp);

        // On tourne la visee (cam + thrower) autour de l'axe x 
        aimTrans.transform.localRotation = Quaternion.Euler(rotX, 0.0f, 0.0f);
    }
}