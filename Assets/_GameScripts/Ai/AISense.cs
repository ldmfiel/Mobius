using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISense : MonoBehaviour {
    NPCComponent owner;
    public GameObject LastDetectedSoundSource;
    public Transform SightOrigin;
    public float visionLength;

    // Use this for initialization
	void Start () {
        owner = gameObject.GetComponent<NPCComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnFixedUpdate()
    {
        RaycastHit2D vision = Physics2D.BoxCast(SightOrigin.position, new Vector2(16, 16), 0, owner.movement.facingDirection,visionLength);

        if(vision)
        {
            if(vision.collider.GetComponent<ConcealableObjectComponent>().isVisible == true)
            {
                OnObjectSeen(collider.gameObject);
            }
        }


    }

    void OnSoundDetected(GameObject soundSource)
    {
        Debug.Log("Distrubance detected");

        if(owner.currentState.StateName == "Patrol")
        {
            owner.SwitchState("Investigate");
            LastDetectedSoundSource = soundSource;
        }
            
    }

    void OnObjectSeen(GameObject objectSeen)
    {

    }




}
