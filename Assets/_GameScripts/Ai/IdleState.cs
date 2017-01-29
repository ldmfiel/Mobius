using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState {

    public string StateToSwitchTo;
    public float IdleTime;

	// Use this for initialization
	void Start () {
        owner = gameObject.GetComponent<NPCComponent>();

        Invoke("EndIdle", IdleTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void EndIdle()
    {
        owner.SwitchState(StateToSwitchTo);
    }
}
