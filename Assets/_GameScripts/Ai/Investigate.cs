using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : AIState {

    public GameObject detectedObject;
    Vector3 lastKnownPos;
    bool isSound;
    Vector3 objectDirection;
    public bool investigating = false;

    // Use this for initialization
    void Start () {
        owner = GetComponent<NPCComponent>();
	}
	
	// Update is called once per frame
	void Update () {

        if(detectedObject)
        {
            objectDirection = owner.movement.GetDirectionToVector(lastKnownPos);

            if (Vector3.Distance(lastKnownPos, transform.position) > 64.0f)
            {
                owner.movement.Walk(objectDirection);
            }
            else
            {
                owner.movement.StopMoving();

                if(owner.sense.LastDetectedEnemy)
                {
                    CancelInvoke("InvestigationComplete");
                    Destroy(owner.sense.LastDetectedEnemy);
                    Debug.Log("ATTACK");
                }
                else
                {
                    Invoke("InvestigationComplete", 4.0f);
                }
            }
        }
	}

    // Called when the AI has detected a visible enemy
    void InvestigateSighting()
    {
        if(!investigating)
            owner.movement.StopMoving();

        detectedObject = owner.sense.LastDetectedEnemy;
        objectDirection = owner.movement.GetDirectionToVector(detectedObject.transform.position);
        owner.movement.facingDirection = objectDirection;        
        lastKnownPos = detectedObject.transform.position;
        investigating = true;
    }

    // Called when the AI has heard a distrubance
    void InvestigateDisturbance()
    {
        if (!investigating)
            owner.movement.StopMoving();

        detectedObject = owner.sense.LastDetectedSoundSource;
        objectDirection = owner.movement.GetDirectionToVector(detectedObject.transform.position);
        owner.movement.facingDirection = objectDirection;
        lastKnownPos = detectedObject.transform.position;
        investigating = true;
    }

    void InvestigationComplete()
    {
        owner.sense.LastDetectedEnemy = null;
        owner.sense.LastDetectedSoundSource = null;
        owner.sense.soundIcon.SetActive(false);
        owner.sense.sightIcon.SetActive(false);
        investigating = false;
        owner.SwitchState("Idle");
    }
}
