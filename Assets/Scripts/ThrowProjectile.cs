using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    
    public GameObject projectile; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetMouseButtonDown(0)){
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation) ;
            projectileInstance.SetActive(true) ; 
            projectileInstance.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10 , ForceMode.Impulse) ; 
        }
    }
}
