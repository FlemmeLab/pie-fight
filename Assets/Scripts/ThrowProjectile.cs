using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    private Transform Projectiles; 
    public GameObject projectile; //Projectile utilisé
    public GameObject thrower; //Position de lancer
    public GameObject HUD; 
    private Vector3 throwForce ; //Vecteur force de lancer
    private Vector3 torqueForce = new Vector3(0, -100, 0); //Couple induisant une rotation de la tarte en mode frizbee
    
    //event lié au changement de mode de tir
    public delegate void throwingModeChangeDelegate(ThrowProjectile t) ; 
    public event throwingModeChangeDelegate throwingModeChangeEvent ;
    public enum throwingMode{ //Modes de lancer 
        FLAT=0, 
        FRIZBEE=1
    }
    public throwingMode currentThrowingMode = throwingMode.FLAT ; 
    //event lié au changement de projectile / arme
    public delegate void projectileChangeDelegate(ThrowProjectile t) ; 
    public event projectileChangeDelegate projectileChangeEvent ;
    
    // Start is called before the first frame update
    void Start()
    {
        //projectile = (GameObject)projectiles.GetValue(0);
        Projectiles = GameObject.Find("Projectiles").gameObject.transform; 
        projectile = Projectiles.GetChild(0).gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(1))
                {

                    if (currentThrowingMode == throwingMode.FLAT)
                    {
                        currentThrowingMode = throwingMode.FRIZBEE;
                        //HUD.GetComponent<Log>().AddLogMessage("Frizbee mode");
                    }
                    else
                    {
                        currentThrowingMode = throwingMode.FLAT;
                        //HUD.GetComponent<Log>().AddLogMessage("Flat mode");
                    }
                    if(throwingModeChangeEvent != null)
                        throwingModeChangeEvent(this) ; 
                }
            
        if(currentThrowingMode == throwingMode.FRIZBEE){
            projectile.transform.rotation = Quaternion.Euler(-90, 0, 0) ;
            throwForce = transform.forward * 30;

        } 
        if(currentThrowingMode == throwingMode.FLAT){
            projectile.transform.rotation = Quaternion.Euler(0, 0, 0) ; 
            throwForce = transform.forward * 20 ; 
        }
        switch (projectile.transform.name)
        {
            case "Pie": 
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject projectileInstance = Instantiate(projectile, thrower.transform.position, projectile.transform.rotation);
                    projectileInstance.SetActive(true);
                    projectileInstance.GetComponent<Rigidbody>().AddForce(throwForce, ForceMode.Impulse);
                    if (currentThrowingMode == throwingMode.FRIZBEE)
                        projectileInstance.GetComponent<Rigidbody>().AddTorque(torqueForce);
                }

               
                break;

            case "Muffin":
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject projectileInstance = Instantiate(projectile, thrower.transform.position, projectile.transform.rotation);
                    projectileInstance.SetActive(true);
                    projectileInstance.GetComponent<Rigidbody>().AddForce(throwForce, ForceMode.Impulse);
                    if (currentThrowingMode == throwingMode.FRIZBEE)
                        projectileInstance.GetComponent<Rigidbody>().AddTorque(torqueForce);
                }

                break;
        }
            

        //Faire rouler la molette pour changer d'arme
        if(Input.mouseScrollDelta.y > 0){

            if (projectile.transform.GetSiblingIndex() >= (Projectiles.childCount-1) )
                projectile = Projectiles.GetChild(0).gameObject;
            else
                projectile = Projectiles.GetChild(projectile.transform.GetSiblingIndex() + 1).gameObject;
            //HUD.GetComponent<Log>().AddLogMessage("weaponSelection = " + projectile.name + " index = " + projectile.transform.GetSiblingIndex());
            if(projectileChangeEvent != null)
                projectileChangeEvent(this) ; 
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            if (projectile.transform.GetSiblingIndex() <= 0)
                projectile = Projectiles.GetChild((Projectiles.childCount - 1)).gameObject;
            else
                projectile = Projectiles.GetChild(projectile.transform.GetSiblingIndex() - 1).gameObject;
            //HUD.GetComponent<Log>().AddLogMessage("weaponSelection = " + projectile.name + " index = " + projectile.transform.GetSiblingIndex());
            if(projectileChangeEvent != null)
                projectileChangeEvent(this) ; 
        }


    }

}
