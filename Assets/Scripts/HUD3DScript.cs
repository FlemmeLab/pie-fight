using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD3DScript : MonoBehaviour
{
    private Vector3 itemSize = new Vector3(1,1,1);
    private Vector3 center = new Vector3(0, -0.1f, 0.5f);
    private float spacing = 0.025f;
    private Transform Projectiles; 
    private ThrowProjectile throwProjectileScript ;
    private Transform current_proj ;
    // Start is called before the first frame update
    void Start()
    {
        Projectiles = GameObject.Find("Projectiles").gameObject.transform;
        foreach(Transform prjctl in Projectiles)
        {
            GameObject instance = Instantiate(prjctl.gameObject, transform.position + center + (new Vector3(spacing,0,0))*(prjctl.GetSiblingIndex() - (Mathf.Floor(Projectiles.childCount / 2))), new Quaternion(0,0,0,0));

            instance.transform.name = prjctl.name ;
            instance.transform.SetParent(transform);
            instance.transform.localScale = itemSize ;
            Destroy(instance.transform.GetComponent("MeshCollider"));
            Destroy(instance.transform.GetComponent("Rigidbody"));
            Destroy(instance.transform.GetComponent("SphereCollider"));
            instance.layer = 5 ;
            instance.SetActive(true);
            instance.AddComponent<Rotate>().rotationSpeed = 30 ;
        }
        throwProjectileScript = (ThrowProjectile)GameObject.Find("Player").GetComponent("ThrowProjectile") ;

        current_proj = transform.Find(Projectiles.GetChild(0).gameObject.transform.name) ;
        current_proj.localScale = itemSize*2 ;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y != 0){
            changeFocus() ;
        }
    }

    void changeFocus(){

            current_proj.localScale = itemSize ;
            current_proj = transform.Find(throwProjectileScript.projectile.name) ;
            current_proj.localScale = itemSize*2 ;
    }
}
