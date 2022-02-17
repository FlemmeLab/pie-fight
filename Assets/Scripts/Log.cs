using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{   
    public GameObject LogText;
    public float showLogTime ; 
    private float timeLeft = 0 ; 
    public void AddLogMessage(string message){
        LogText.GetComponent<UnityEngine.UI.Text>().text+= message+" \n" ; 
        LogText.SetActive(true) ; 
        timeLeft = showLogTime ; 
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft <= 0){
            if(LogText.activeSelf)
                LogText.SetActive(false) ; 
        }
        else
            timeLeft -= Time.deltaTime ;
    }

}
