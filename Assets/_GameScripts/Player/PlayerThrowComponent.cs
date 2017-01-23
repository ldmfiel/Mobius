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

    public GameObject QuickThrow(Vector2 direction, float distance = 150)
    {
        GameObject thrownObject = Instantiate(throwingObject, transform.position, transform.rotation);

        if(direction == Vector2.down)
        {
            thrownObject.GetComponent<ThrownObjectComponent>().StartThrow(distance, direction, new Vector3(0,-64));
        }
        else
        {
            thrownObject.GetComponent<ThrownObjectComponent>().StartThrow(distance, direction, Vector2.zero);
        }       

        return thrownObject;
    }
}
