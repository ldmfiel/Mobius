using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISense : MonoBehaviour {
    NPCComponent owner;
    public GameObject LastDetectedSoundSource;
    public GameObject LastDetectedEnemy;

    public Transform SightOrigin;
    public float visionLength;
    public LayerMask sightMask;


    //Prototype UI Objects
    public GameObject sightIcon;
    public GameObject soundIcon;

    // Use this for initialization
	void Start () {
        owner = gameObject.GetComponent<NPCComponent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        RaycastHit2D vision = Physics2D.BoxCast(SightOrigin.position, new Vector2(16, 16), 0, owner.movement.facingDirection,visionLength,sightMask);
        Debug.Log(vision);
        if (vision)
        {
            if(vision.collider.GetComponent<ConcealableObjectComponent>().isVisible == true)
            {
                OnObjectSeen(vision.collider.gameObject);
            }


        }
        else
        {
            sightIcon.SetActive(false);
            LastDetectedEnemy = null;
        }



    }

    void OnSoundDetected(GameObject soundSource)
    {
        if(owner.currentState.StateName == "Patrol")
        {
            Debug.Log("Distrubance detected");
            owner.SwitchState("Investigate");
            LastDetectedSoundSource = soundSource;
            SendMessage("InvestigateDisturbance");
            soundIcon.SetActive(true);
        }
            
    }

    void OnObjectSeen(GameObject objectSeen)
    {
        Debug.Log("Sighting detected");

        owner.SwitchState("Investigate");
        LastDetectedEnemy = objectSeen;
        SendMessage("InvestigateSighting");
        sightIcon.SetActive(true);
    }




}
