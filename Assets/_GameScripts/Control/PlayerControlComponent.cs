using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlComponent : MonoBehaviour {

    enum EControlState
    {
        PLAY,
        PAUSED,
        DIALOGUE
    }


    PlayerMovementComponent movement;

    EControlState state = EControlState.PLAY;
    Vector2 InputAxis;

	// Use this for initialization
	void Start () {
        movement = GetComponent<PlayerMovementComponent>();

	}
	
	// Update is called once per frame
	void Update () {
        InputAxis.x = Input.GetAxis("Horizontal");

        if(state == EControlState.PLAY && movement)
        {
            movement.Walk(InputAxis);

            if(Input.GetButtonDown("Jump"))
            {
                movement.DoJump();
            }

            if(Input.GetButtonUp("Jump"))
            {
                movement.EndJump();
            }
        }
       
	}

}
