using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour {

    new Rigidbody2D rigidbody2D;

    public float WalkSpeed = 100.0f;
    public float WalkDeceleration = 50.0f;

    public bool Jumping;
    public float JumpVelocity = 240 ;
    public float CrouchJumpBonus = 60;
    bool crouchJumping = false;

    public LayerMask FloorMask;
    public bool onFloor;
    MoveObjectInDirectionComponent movingObject;

    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Walk(Vector2 InputDirection)
    {
        Vector3 velocity = rigidbody2D.velocity;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1),34, FloorMask);

        if (!crouchJumping)
        {
            velocity.x = (WalkSpeed * InputDirection.x);
        }
        else
        {
            velocity.x = ((WalkSpeed/2) * InputDirection.x);
        }

        if(velocity.x < 0)
            velocity.x += (WalkDeceleration * Time.deltaTime);
        else if (velocity.x > 0)
            velocity.x -= (WalkDeceleration * Time.deltaTime);

        if(hit)
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.GetComponent<MoveObjectInDirectionComponent>() || (hit.collider.gameObject.tag == "Ghost" && movingObject != null))
            {
                Debug.Log("On moving object");

                if (movingObject == null)
                {
                    movingObject = hit.collider.gameObject.GetComponent<MoveObjectInDirectionComponent>();
                }
                else
                {
                    transform.position += ((movingObject.Speed * movingObject.Direction*Time.deltaTime));

                    Debug.Log(velocity);
                }
            }

            if (crouchJumping)
                crouchJumping = false;
            if (Jumping)
                Jumping = false;


            onFloor = true;            
        }
        else
        {
            movingObject = null;
            onFloor = false;
        }



        rigidbody2D.velocity = velocity;
    }

    public void DoJump(bool crouchJump)
    {
        Vector3 velocity = rigidbody2D.velocity;

        if(!Jumping && !crouchJump)
        {
            // Start Jump
            velocity.y = JumpVelocity;
            Jumping = true;
        }
        else if(!Jumping && crouchJump)
        {
            velocity.y = JumpVelocity + CrouchJumpBonus;
            crouchJumping = true;
            Jumping = true;
        }

        rigidbody2D.velocity = velocity;
    }

    public void EndJump()
    {
        Vector3 velocity = rigidbody2D.velocity;

        if (Jumping)
        {
            // Start Jump
            velocity.y = velocity.y/2;
        }

        rigidbody2D.velocity = velocity;
    }

    public void StopMovement()
    {
        rigidbody2D.velocity = Vector2.zero;
    }
}
