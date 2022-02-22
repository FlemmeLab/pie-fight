using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD3DScript : MonoBehaviour
{
    private float itemSize = 1f;
    private Vector3 PositionOnScreen = new Vector3(0, -0.1f, 0.5f);
    private float spacing = 0.0125f;
    private Transform Projectiles; 
    private Transform current_proj ;
    private Transform prev_proj ;
    // Start is called before the first frame update
    public Vector3 centerOfHUD ; 
    public float distanceFomCenter ;
    void Start()
    {   
        centerOfHUD = transform.position + PositionOnScreen ; 
        Projectiles = GameObject.Find("Projectiles").gameObject.transform;
        foreach(Transform prjctl in Projectiles)
        {
            distanceFomCenter = (2*spacing)*( (prjctl.GetSiblingIndex()+1) - Projectiles.childCount + (prjctl.GetSiblingIndex()+1)/2 ) ; 
            GameObject instance = Instantiate(prjctl.gameObject, 
            centerOfHUD + (new Vector3(1,0,0)) * distanceFomCenter, 
            new Quaternion(0,0,0,0));

            instance.transform.name = prjctl.name ;
            instance.transform.SetParent(transform);
            instance.transform.localScale = new Vector3 (itemSize,itemSize,itemSize) ;
            Destroy(instance.transform.GetComponent("MeshCollider"));
            Destroy(instance.transform.GetComponent("Rigidbody"));
            Destroy(instance.transform.GetComponent("SphereCollider"));
            instance.layer = 5 ; // HUD layer
            instance.SetActive(true);
            instance.AddComponent<Rotate>().rotationSpeed = 30 ;
        }
        current_proj = transform.Find(Projectiles.GetChild(0).gameObject.transform.name) ;
        current_proj.localScale = itemSize*2*(new Vector3(1,1,1)) ;
        //On s'abonne à l'event
        FindObjectOfType<ThrowProjectile>().projectileChangeEvent += onProjectileChange;
    }

    // Update is called once per frame
    bool updateScale = false ; 
    void Update()
    {

        if(updateScale) {
            if(current_proj.localScale.x != itemSize*2){
                current_proj.localScale += new Vector3(0.5f,0.5f,0.5f) ;
            }
            if(prev_proj.localScale.x != itemSize){
                prev_proj.localScale -= new Vector3(0.5f,0.5f,0.5f) ;
            }
            updateScale = (prev_proj.localScale.x != itemSize) || (current_proj.localScale.x != itemSize*2) ; 
        }
      
    }

    void onProjectileChange(ThrowProjectile t){

            prev_proj = current_proj ;
            current_proj = transform.Find(t.projectile.name) ;
            updateScale = true ; 
    }
}
