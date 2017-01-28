using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterObjectComponent : MonoBehaviour {

    public new Rigidbody2D rigidbody2D;
    BoxCollider2D box;

    public float InitialAngle;
    public LayerMask objectMask;

    RaycastHit2D[] raycasts;
    public bool CanTeleportTo = false;

    float distanceToTarget;
    float StartDuration;
    float Duration;
    Vector2 Target;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
	}
	

    void Update()
    {
        RaycastHit2D floorCheck = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y) + (Vector2.down * 17), objectMask);
        Duration -= Time.deltaTime;
        // While object has no collision check if it is in objects or falling down.
        if (box.isTrigger)
        {
            // Inside Object check
            raycasts = Physics2D.BoxCastAll(transform.position, box.size, 0, Vector2.zero, 1.0f, objectMask);

            bool inObject = raycasts.Length > 0;
            Debug.Log(inObject);

            distanceToTarget = Vector2.Distance(transform.position, Target);

            if (Duration <= (StartDuration/4))
            {
                rigidbody2D.gravityScale = 1;
            }

            if (rigidbody2D.velocity.y < 0 && !inObject)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
            }


        }
        //While objects collision is on
        else
        {
            // When object hits the floor let player teleport to object
            if (floorCheck && !CanTeleportTo)
            {
                if (floorCheck.collider.tag == "Player")
                {
                    Destroy(gameObject);
                }

                CanTeleportTo = true;
            }


        }

        
    }

    // Set initial to throw object to dir*newDistance+offset
    public void StartThrow(float newDistance,Vector2 dir, Vector3 offset)
    {
        Debug.Log(Vector3.right);
        Vector2 position = transform.position + offset;
        transform.position = position;
        rigidbody2D.velocity = CalculateInitialVelocityFrisbee(position + dir * newDistance,1,dir);
    }

    Vector2 CalculateInitialVelocityFrisbee(Vector2 target, float duration, Vector2 dir)
    {
        Vector2 finalVelocity = new Vector2();

        if(rigidbody2D)
        {
            Vector2 initialVelocity = new Vector2();

            if(dir.y == 0)
            {
                distanceToTarget = target.x - transform.position.x;

                initialVelocity.x = (distanceToTarget) / duration;
            }
            else
            {
                distanceToTarget = target.y - transform.position.y;

                initialVelocity.y = distanceToTarget;
            }
            Target = target;
            StartDuration = duration;
            Duration = duration;
            finalVelocity = initialVelocity;
        }

        return finalVelocity;
    }

    Vector2 CalculateInitialArcVelocity(Vector2 target, float duration)
    {
        Vector2 finalVelocity = new Vector2();

        if (rigidbody2D)
        {
            float gravity = Physics2D.gravity.magnitude;
            Vector2 initialVelocity = new Vector2();

            initialVelocity.x = (target.x - transform.position.x) / duration;
            initialVelocity.y = (target.y + 0.5f * gravity * duration * duration - transform.position.y)/duration;

            Debug.Log(initialVelocity);

            finalVelocity.x = initialVelocity.x * duration;
            finalVelocity.y = -0.5f * gravity * duration * duration + initialVelocity.y + transform.position.y;

            Debug.Log(finalVelocity);
        }

        return finalVelocity;
    }


}
