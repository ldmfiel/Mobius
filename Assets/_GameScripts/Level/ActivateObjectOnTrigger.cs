using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectOnTrigger : MonoBehaviour {

    public GameObject GameObjectRef;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObjectRef.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObjectRef.SetActive(false);
        }
    }

}
