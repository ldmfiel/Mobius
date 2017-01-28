using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EControlState
{
    PLAY,
    PAUSED,
    DIALOGUE,
    UI
}


public class PlayerControlComponent : MonoBehaviour {



    PlayerMovementComponent movement;
    PlayerThrowComponent throwing;

    EControlState state = EControlState.PLAY;
    Vector2 InputAxis;
    public Vector2 FacingDirection = Vector2.right;

    public GameObject[] Teleporters;
    int MaxTeleporters = 1;

    SpriteRenderer spriteRender;

	// Use this for initialization
	void Start () {
        Teleporters = new GameObject[MaxTeleporters];
        movement = GetComponent<PlayerMovementComponent>();
        throwing = GetComponent<PlayerThrowComponent>();
        spriteRender = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        InputAxis.x = Input.GetAxis("Horizontal");
        InputAxis.y = Input.GetAxis("Vertical");

        /* Begin Play State Controls 
        * Movement
        * Jumping
        * Teleporting
        * Combat
        */
        if(state == EControlState.PLAY)
        {
            if (movement)
            {
                movement.Walk(InputAxis);

                if(InputAxis.x < 0)
                {
                    FacingDirection = Vector2.left;
                    spriteRender.flipX = true;
                }
                else if (InputAxis.x > 0)
                {
                    FacingDirection = Vector2.right;
                    spriteRender.flipX = false;
                }

                if (Input.GetButtonDown("Jump") && movement.onFloor)
                {   
                    // Enable crouch jump here if needed.
                    movement.DoJump(false);
                }

                if (Input.GetButtonUp("Jump"))
                {
                    movement.EndJump();
                }
            }

            if(throwing)
            {
                // Throw Object based on input direction, will be re-writing this when weapons are added
                if(InputAxis.y == 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (Teleporters[0] != null && Teleporters[0].GetComponent<TeleporterObjectComponent>().CanTeleportTo)
                            Destroy(Teleporters[0]);
                        if (Teleporters[0] == null)
                            Teleporters[0] = throwing.QuickThrow(FacingDirection,256);                           
                    }
                }
                else if(InputAxis.y > 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (Teleporters[0] != null && Teleporters[0].GetComponent<TeleporterObjectComponent>().CanTeleportTo)
                            Destroy(Teleporters[0]);
                        if (Teleporters[0] == null)
                            Teleporters[0] = throwing.QuickThrow(Vector2.up,225);

                    }
                }
                else if(InputAxis.y < 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (Teleporters[0] != null && Teleporters[0].GetComponent<TeleporterObjectComponent>().CanTeleportTo)
                            Destroy(Teleporters[0]);
                        if(Teleporters[0] == null)
                            Teleporters[0] = throwing.QuickThrow(Vector2.down, 150);
                    }
                }

                // Teleport to the teleporter
                if(Input.GetButtonDown("Fire2") && Teleporters[0])
                {
                    if(!Teleporters[0].GetComponent<BoxCollider2D>().isTrigger && Teleporters[0].GetComponent<TeleporterObjectComponent>().CanTeleportTo)
                    {
                        Debug.Log(Teleporters[0].transform.position);
                        transform.position = Teleporters[0].transform.position + new Vector3(0,33);
                        Destroy(Teleporters[0]);
                    }
                    else if(Teleporters[0].GetComponent<Rigidbody2D>().gravityScale == 0)
                    {
                        Teleporters[0].GetComponent<Rigidbody2D>().gravityScale = 1;
                    }
                }
            }
        }       
	}

    public void SwitchState(EControlState newState)
    {
        switch (state)
        {
            default:
                break;
        }

        state = newState;      
    }


}
