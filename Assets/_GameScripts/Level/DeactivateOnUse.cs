using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnUse : MonoBehaviour {

    bool canActivate;
    public GameObject targetObject;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Submit") && canActivate && targetObject.active)
        {
            targetObject.SetActive(false);
        }

        else if (Input.GetButtonDown("Submit") && canActivate && !targetObject.active)
        {
            targetObject.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canActivate = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canActivate = false;
        }
    }

}
