using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : AIState {

    public Transform positionA;
    public Transform positonB;

	// Use this for initialization
	void Start () {
        owner = GetComponent<NPCComponent>();
	}
	
	// Update is called once per frame
	void Update () {
        owner.movement.Patrol(positionA.position, positonB.position);
	}
}
