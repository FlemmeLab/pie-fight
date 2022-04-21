
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    // Sensitivit� de la souris
    public float Sensitivity = 5f;

    //Configurable depuis un menu?
    public string jumpKey = "space";

    public string horizontalLookInput = "Mouse X";
    public string verticalLookInput = "Mouse Y";

    //-----------------

    //Vitesse du joueur 
    private float playerSpeed = 10f;

    //Force de saut 
    private float jumpForce = 5f;

    //Timer entre les sauts
    private float jumpInterval = 0.5f;
    private float jumpTimeRemaining = 0f;

    //Objet comprenant la camera et le lanceur de projectile qui n�cessitent d'�tre orient�s dans la m�me direction
    private Transform aimTrans;
    
    //Rotation selon l'axe x du joueur
    private float rotX;

    //Limites sup�rieures et inf�rieure de la vue verticale
    private float bottomClamp = -90f;
    private float topClamp = 90f;

    //Ground
    private float GroundedRadius = 0.1f;
    public LayerMask EnvironnementLayer;
    public bool isNotGrounded;

    //Rigidbody
    private Rigidbody PlayerRigidbody; 

    void Start()
    {
        aimTrans = GameObject.Find("Aim").transform;

        Cursor.lockState = CursorLockMode.Locked;
        PlayerRigidbody = transform.GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
         //On verifie la presence du sol avant de sauter
        if (Physics.CheckBox(transform.position - new Vector3(0, GroundedRadius, 0), transform.localScale - new Vector3(0.01f,0.00f,0.01f), Quaternion.identity, EnvironnementLayer) && (jumpTimeRemaining <= 0f) && (Input.GetAxis("Jump") != 0))
        {
            transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isNotGrounded = false; 
            jumpTimeRemaining = jumpInterval;
        } else
        {
            isNotGrounded = true; 
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Sensitivity, 0));
        rotX -= Input.GetAxis("Mouse Y") * Sensitivity;

        // clamp our pitch rotation
        rotX = ClampAngle(rotX, bottomClamp, topClamp);

        // On tourne la visee (cam + thrower) autour de l'axe x 
        aimTrans.transform.localRotation = Quaternion.Euler(rotX, 0.0f, 0.0f);

        //Seul moyen d'avoir un mvmt propre et maitrise ( "velocite <= vitesse du joueur" )
        PlayerRigidbody.velocity = Vector3.ClampMagnitude(Input.GetAxis("Vertical") * transform.forward * playerSpeed
                                                   + Input.GetAxis("Horizontal") * transform.right * playerSpeed, playerSpeed)
                             + new Vector3(0, PlayerRigidbody.velocity.y, 0);

        //gestion du temps entre les sauts
        if (jumpTimeRemaining > 0)
            jumpTimeRemaining -= Time.deltaTime;
        if (jumpTimeRemaining < 0)
            jumpTimeRemaining = 0;

    }

    // fonction qui retourne lfAngle si lfMax > lfAngle > lfMin, lfMin si lfAngle <= lfMin ou lfMax si lfAngle >= lfMax
    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
/*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(isNotGrounded+" Collido"); 
        GameObject collided = collision.gameObject;
        if(collided.layer == 6 && isNotGrounded)
            Debug.Log("Mega Collido");
       
    }
*/

}