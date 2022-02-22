using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD2DScript : MonoBehaviour
{
    private Transform Crosshairs ;
    public int CTM = -1 ; 
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
            CTM = (int)t.currentThrowingMode ; 
            if((int)t.currentThrowingMode == crsshr.GetSiblingIndex())
                crsshr.gameObject.SetActive(true) ; 
            else
                crsshr.gameObject.SetActive(false) ;        
        }
    }
}
