using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    
    public GameObject[] projectiles; //Liste de tous les projectiles à remplir dans l'éditeur de jeu
    private GameObject projectile; //Projectile utilisé
    public GameObject thrower; //Position de lancer
    public GameObject HUD; 
    private Vector3 throwForce ; //Vecteur force de lancer
    private Vector3 torqueForce = new Vector3(0, -100, 0); //Couple induisant une rotation de la tarte en mode frizbee
 
    private enum WeaponSelect
    {
        PIE = 0, MUFFIN = 1
    }
    private WeaponSelect weaponSelection = 0;
    private enum throwingMode{ //Modes de lancer 
        FLAT, FRIZBEE
    }
    private throwingMode currentThrowingMode = throwingMode.FLAT ; 

    // Start is called before the first frame update
    void Start()
    {
        projectile = (GameObject)projectiles.GetValue(0);

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
        switch (weaponSelection)
        {
            case WeaponSelect.PIE: 
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject projectileInstance = Instantiate((GameObject)projectiles.GetValue((int)weaponSelection), thrower.transform.position, projectile.transform.rotation);
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

            case WeaponSelect.MUFFIN:
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject projectileInstance = Instantiate((GameObject)projectiles.GetValue((int)weaponSelection), thrower.transform.position, projectile.transform.rotation);
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
        if(Input.mouseScrollDelta.y != 0){
            if ((int)weaponSelection >= projectiles.Length-1)
                weaponSelection = 0;
            else
                weaponSelection++;
            HUD.GetComponent<Log>().AddLogMessage("weaponSelection = " + weaponSelection + " (int) = " + (int)weaponSelection);

        }
        if (Input.mouseScrollDelta.y < 0)
        {
            if ((int)weaponSelection <= 0)
                weaponSelection = (WeaponSelect)(projectiles.Length - 1);
            else
                weaponSelection--;
            HUD.GetComponent<Log>().AddLogMessage("weaponSelection = " + weaponSelection + " (int) = " + (int)weaponSelection);

        }


    }

}
