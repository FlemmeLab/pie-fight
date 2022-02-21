using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    private Transform Projectiles; 
    public GameObject projectile; //Projectile utilisé
    public GameObject thrower; //Position de lancer
    public GameObject HUD; 
    private Vector3 throwForce ; //Vecteur force de lancer
    private Vector3 torqueForce = new Vector3(0, -100, 0); //Couple induisant une rotation de la tarte en mode frizbee

    private enum throwingMode{ //Modes de lancer 
        FLAT, FRIZBEE
    }
    private throwingMode currentThrowingMode = throwingMode.FLAT ; 

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
        if(currentThrowingMode == throwingMode.FRIZBEE){
            projectile.transform.rotation = Quaternion.Euler(-90, 0, 0) ;
            throwForce = Vector3.forward * 30;

        } 
        if(currentThrowingMode == throwingMode.FLAT){
            projectile.transform.rotation = Quaternion.Euler(0, 0, 0) ; 
            throwForce = Vector3.forward * 20 ; 
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

                if (Input.GetMouseButtonDown(1))
                {

                    if (currentThrowingMode == throwingMode.FLAT)
                    {
                        currentThrowingMode = throwingMode.FRIZBEE;
                        HUD.GetComponent<Log>().AddLogMessage("Frizbee mode");
                    }
                    else
                    {
                        currentThrowingMode = throwingMode.FLAT;
                        HUD.GetComponent<Log>().AddLogMessage("Flat mode");
                    }
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

                if (Input.GetMouseButtonDown(1))
                {

                    if (currentThrowingMode == throwingMode.FLAT)
                    {
                        currentThrowingMode = throwingMode.FRIZBEE;
                        HUD.GetComponent<Log>().AddLogMessage("Frizbee mode");
                    }
                    else
                    {
                        currentThrowingMode = throwingMode.FLAT;
                        HUD.GetComponent<Log>().AddLogMessage("Flat mode");
                    }
                }
                break;
        }

        //Faire rouler la molette pour changer d'arme
        if(Input.mouseScrollDelta.y > 0){

            if (projectile.transform.GetSiblingIndex() >= (Projectiles.childCount-1) )
                projectile = Projectiles.GetChild(0).gameObject;
            else
                projectile = Projectiles.GetChild(projectile.transform.GetSiblingIndex() + 1).gameObject;
            HUD.GetComponent<Log>().AddLogMessage("weaponSelection = " + projectile.name + " index = " + projectile.transform.GetSiblingIndex());

        }

        if (Input.mouseScrollDelta.y < 0)
        {
            if (projectile.transform.GetSiblingIndex() <= 0)
                projectile = Projectiles.GetChild((Projectiles.childCount - 1)).gameObject;
            else
                projectile = Projectiles.GetChild(projectile.transform.GetSiblingIndex() - 1).gameObject;
            HUD.GetComponent<Log>().AddLogMessage("weaponSelection = " + projectile.name + " index = " + projectile.transform.GetSiblingIndex());

        }


    }

}
