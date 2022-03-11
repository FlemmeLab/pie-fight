
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483

    // Configurable par le joueur : 

    // Sensitivit� de la souris
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

    //event li� � la mise � jour de la position du joueur 
    public delegate void MoveDelegate(Movement t);
    public event MoveDelegate moveEvent;

    //Vitesse du joueur 
    private float playerSpeed = 10f; 
    
    //Force de saut 
    private float jumpForce = 5f;

    private float jumpInterval = 0.5f;
    private float jumpTimeRemaining = 0f;


    //Vecteur de d�placement qui correspond � l'appuis sur les touches de d�placement (ex : (-1,0,1) pour q,z)
    //Je n'aime pas cette methode (m�thode arithm�tique?)

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

    private static float bumperRadius = 0.5f; 
    public LayerMask bumpLayers;

    //Objet comprenant la camera et le lanceur de projectile qui n�cessitent d'�tre orient�s dans la m�me direction
    private Transform aimTrans;
    
    //Rotation selon l'axe x du joueur
    private float rotX;

    //Limites sup�rieures et inf�rieure de la vue verticale
    private float bottomClamp = -90f;
    private float topClamp = 90f;

    //Ground
    private Transform groundTrans;
    private float GroundedRadius = 0.1f;
    public LayerMask GroundLayers; 

    void Start()
    {
        aimTrans = GameObject.Find("Aim").transform;
        groundTrans = GameObject.Find("Ground").transform;
        //On s'abonne � l'event
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
            case 7:
                return direction_up;
            case 8:
                return direction_down;
            case 9:
                return direction_down_right;
            case 11:
                return direction_right;
            case 12:
                return direction_down_left;
            case 13:
                return direction_down;
            case 14:
                return direction_left;
            default:
                return Vector3.zero; 
        }
    }

    private void onMove(Movement t)
    {
        if ( Physics.CheckSphere( transform.position - transform.localScale, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore) && (jumpTimeRemaining == 0f) && Input.GetKeyDown(jumpKey)) {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jumpTimeRemaining = jumpInterval; 
        }

        int movement_code = IoB(Input.GetKey(rightMoveKey)) * IoB(!Physics.CheckSphere(transform.position + transform.right * transform.localScale.x / 2, bumperRadius, bumpLayers, QueryTriggerInteraction.Ignore))
                            + 2 * IoB(Input.GetKey(frontMoveKey)) * IoB(!Physics.CheckSphere(transform.position + transform.forward*transform.localScale.z / 2 , bumperRadius, bumpLayers, QueryTriggerInteraction.Ignore))
                            + 4 * IoB(Input.GetKey(leftMoveKey)) * IoB(!Physics.CheckSphere(transform.position - transform.right * transform.localScale.x / 2 , bumperRadius, bumpLayers, QueryTriggerInteraction.Ignore))
                            + 8 * IoB(Input.GetKey(backMoveKey)) * IoB(!Physics.CheckSphere(transform.position - transform.forward * transform.localScale.z / 2 , bumperRadius, bumpLayers, QueryTriggerInteraction.Ignore));   
        
        transform.Translate(DirectionInput(movement_code) * playerSpeed * Time.deltaTime);
        //transform.GetComponent<Rigidbody>().MovePosition(transform.position + DirectionInput(movement_code) * playerSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(0, Input.GetAxis(horizontalLookInput) * Sensitivity, 0));

        rotX -= Input.GetAxis(verticalLookInput) * Sensitivity;

        // clamp our pitch rotation
        rotX = ClampAngle(rotX, bottomClamp, topClamp);

        // On tourne la visee (cam + thrower) autour de l'axe x 
        aimTrans.transform.localRotation = Quaternion.Euler(rotX, 0.0f, 0.0f);
    }
}