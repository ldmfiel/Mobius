using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowComponent : MonoBehaviour {

    public GameObject throwingObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject QuickThrow(Vector2 direction)
    {
        GameObject thrownObject = Instantiate(throwingObject, transform.position, transform.rotation);

        thrownObject.GetComponent<ThrownObjectComponent>().StartThrow(150,direction);

        return thrownObject;
    }
}
