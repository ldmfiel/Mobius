using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NPCState
{
    public string StateName;
    public MonoBehaviour stateComponent;
}


public class NPCComponent : MonoBehaviour {

    public string name = "Jerry";
    public NPCState currentState;
    public NPCMovementComponent movement;
    public AISense sense;


    public NPCState[] states;

	// Use this for initialization
	void Start () {
        movement = GetComponent<NPCMovementComponent>();
        sense = GetComponent<AISense>();
        SwitchState("Idle");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchState(string name)
    {
        foreach(NPCState s in states)
        {
            if(s.StateName == name)
            {
                currentState.stateComponent.enabled = false;                
                currentState = s;
                s.stateComponent.enabled = true;
                break;
            }
        }
    }
}
