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
    PlayerThrowComponent throwing;

    EControlState state = EControlState.PLAY;
    Vector2 InputAxis;
    public Vector2 FacingDirection = Vector2.right;

    public GameObject[] Teleporters;
    int MaxTeleporters = 1;

	// Use this for initialization
	void Start () {
        Teleporters = new GameObject[MaxTeleporters];
        movement = GetComponent<PlayerMovementComponent>();
        throwing = GetComponent<PlayerThrowComponent>();
	}
	
	// Update is called once per frame
	void Update () {
        InputAxis.x = Input.GetAxis("Horizontal");
        if(state == EControlState.PLAY)
        {
            if (movement)
            {
                movement.Walk(InputAxis);

                if(InputAxis.x < 0)
                {
                    FacingDirection = Vector2.left;
                }
                else if (InputAxis.x > 0)
                {
                    FacingDirection = Vector2.right;
                }

                if (Input.GetButtonDown("Jump") && movement.onFloor)
                {
                    movement.DoJump();
                }

                if (Input.GetButtonUp("Jump"))
                {
                    movement.EndJump();
                }
            }

            if(throwing)
            {
                if(Input.GetButtonDown("Fire1") && Teleporters[0] == null)
                {
                    Teleporters[0] = throwing.QuickThrow(FacingDirection);

                }

                if(Input.GetButtonDown("Fire2") && Teleporters[0])
                {
                    Debug.Log(Teleporters[0].transform.position);
                    transform.position = Teleporters[0].transform.position + new Vector3(-16*FacingDirection.x,16);
                    Destroy(Teleporters[0]);
                }
            }
        }       
       
   
	}


}
