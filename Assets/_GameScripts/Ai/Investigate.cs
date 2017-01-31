using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : AIState {

	// Use this for initialization
	void Start () {
        owner = GetComponent<NPCComponent>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 objectDirection = owner.movement.GetDirectionToVector(owner.sense.LastDetectedSoundSource.transform.position);

        if(Vector3.Distance(owner.sense.LastDetectedSoundSource.transform.position,transform.position) > 1.0f)
        {
            owner.movement.Walk(objectDirection);
        }
        else
        {
            owner.movement.StopMoving();
            owner.sense.LastDetectedSoundSource = null;
        }

	}
}
