using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD2DScript : MonoBehaviour
{
    private Transform Crosshairs ;
    void Start()
    {
        Crosshairs = GameObject.Find("Crosshairs").gameObject.transform; 
        //On s'abonne à l'event
        FindObjectOfType<ThrowProjectile>().throwingModeChangeEvent += onThrowingModeChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onThrowingModeChange(ThrowProjectile t){
        foreach(Transform crsshr in Crosshairs)
        {   
            if((int)t.currentThrowingMode == crsshr.GetSiblingIndex())
                crsshr.gameObject.SetActive(true) ; 
            else
                crsshr.gameObject.SetActive(false) ;        
        }
    }
}
