using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour {

    protected NPCComponent owner;

    // Use this for initialization
    void Start () {
        owner = gameObject.GetComponent<NPCComponent>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
