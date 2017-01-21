using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjectComponent : MonoBehaviour {

    public new Rigidbody2D rigidbody2D;
    BoxCollider2D box;

    public float InitialAngle;
    public LayerMask objectMask;

    RaycastHit2D[] raycasts;
    public bool CanTeleportTo = false;


	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
	}
	

    void Update()
    {
        RaycastHit2D floorCheck = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y) + (Vector2.down * 17), objectMask);

        // While object has no collision check if it is in objects or falling down.
        if (box.isTrigger)
        {
            // Inside Object check
            raycasts = Physics2D.BoxCastAll(transform.position, box.size, 0, Vector2.zero, 1.0f, objectMask);

            bool inObject = raycasts.Length > 0;
            Debug.Log(inObject);

            if (rigidbody2D.velocity.y < 0 && !inObject)
            {
                box.offset = new Vector2(0, 0);
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
        //While objects collision is on
        else
        {
            // When object hits the floor let player teleport to object
            if (floorCheck && !CanTeleportTo)
            {
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
        rigidbody2D.velocity = CalculateInitialVelocity(position + dir * newDistance,1.0f);
    }

    Vector3 CalculateInitialVelocity(Vector2 target, float duration)
    {
        Vector2 finalVelocity = new Vector3();

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
