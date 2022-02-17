using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    
    public GameObject projectile; 
    public GameObject thrower;
    public GameObject HUD;  

    private enum throwingMode{
        FLAT, FRIZBEE
    }
    private throwingMode currentThrowingMode = throwingMode.FLAT ; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetMouseButtonDown(0)){
            GameObject projectileInstance = Instantiate(projectile, thrower.transform.position, projectile.transform.rotation) ;
            projectileInstance.SetActive(true) ; 
            projectileInstance.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse) ; 
        }

        if(Input.GetMouseButtonDown(1)){

            if(currentThrowingMode == throwingMode.FLAT){
                currentThrowingMode = throwingMode.FRIZBEE ;
                HUD.GetComponent<Log>().AddLogMessage("Frizbee mode") ; 
            }else{
                currentThrowingMode = throwingMode.FLAT ;
                HUD.GetComponent<Log>().AddLogMessage("Flat mode") ; 
            } 
        }
    }
}
