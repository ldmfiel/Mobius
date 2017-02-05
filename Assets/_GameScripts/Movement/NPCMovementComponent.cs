using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementComponent : MonoBehaviour {

    new Rigidbody2D rigidBody2D;
    SpriteRenderer sprite;
    public float WalkSpeed;
    public Vector2 facingDirection;

    //Patrol Variables
    bool A = true;

	// Use this for initialization
	void Start () {
        rigidBody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

		if(rigidBody2D.velocity.x < -1)
        {
            facingDirection = Vector2.left;
        }
        else if (rigidBody2D.velocity.x > 1)
        {
            facingDirection = Vector2.right;
        }

        if(facingDirection.x < 0) { sprite.flipX = true; };
        if(facingDirection.x > 0) { sprite.flipX = false; };
	}

    public void Walk(Vector3 dir)
    {
        rigidBody2D.velocity = new Vector2(dir.x * WalkSpeed, rigidBody2D.velocity.y);
    }

    public void Patrol(Vector3 posA, Vector3 posB)
    {
        //Get distance
        float Distance = Mathf.Round(Vector3.Distance(posA, posB));
        float distanceA = Mathf.Round(Vector3.Distance(posA, transform.position));
        float distanceB = Mathf.Round(Vector3.Distance(posB, transform.position));
        Vector3 direction = new Vector3();
        
        if(A)
        {
            //Debug.Log("Approaching A" + distanceB);
            direction = (posA - transform.position).normalized;
            
            if(distanceB > Distance || rigidBody2D.velocity.x == 0)
            {
                A = false;
            }
        }
        else
        {
            //Debug.Log("Approaching B" + distanceA);
            direction = (posB - transform.position).normalized;

            if (distanceA > Distance || rigidBody2D.velocity.x == 0)
            {
                A = true;
            }
        }


        Walk(direction);

    }

    public Vector3 GetDirectionToVector(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;

        return dir;
    }

    public void StopMoving()
    {
        rigidBody2D.velocity = Vector2.zero;
    }
}

