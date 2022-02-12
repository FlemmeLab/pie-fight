using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    
    public Rigidbody bullet; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetMouseButtonDown(0)){
            Rigidbody projectile = Instantiate(bullet, transform.position, transform.rotation) ;
            projectile.velocity = transform.TransformDirection(Vector3.forward * 10);
        }
    }
}
