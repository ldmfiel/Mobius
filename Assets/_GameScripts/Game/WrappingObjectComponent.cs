using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingObjectComponent : MonoBehaviour {

    bool bCanWrap = true;

    Camera cameraObject;
    Vector3 screenBottomLeft, screenTopRight;

    public float screenWidth, screenHeight;
    GameObject[] ghosts = new GameObject[8];

    public GameObject ghostPref;

	// Use this for initialization
    void Start () {
        cameraObject = Camera.main;

        screenBottomLeft = cameraObject.ViewportToWorldPoint(new Vector3(0,0,transform.position.z));
        screenTopRight = cameraObject.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.y - screenBottomLeft.y;

        CreateGhosts();
        PositionGhosts();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent == null)
        {
            SwapGhosts();            
        }
	}

    //Create Ghost Images

    void CreateGhosts()
    {
        for(int i = 0; i < 8; i++)
        {
            ghosts[i] = Instantiate(ghostPref, Vector3.zero, Quaternion.identity) as GameObject;
            ghosts[i].transform.parent = transform;
        }
    }

    //Position Ghosts so that they will be on the screen as the main objects leaves the screen.
    void PositionGhosts()
    {
        Vector2 velocity = new Vector2();
        Vector2 oldVelocity = new Vector2();

        if (GetComponent<Rigidbody2D>() != null)
        {
            oldVelocity = GetComponent<Rigidbody2D>().velocity;
            velocity = GetComponent<Rigidbody2D>().velocity;
            velocity.y = 0;

            GetComponent<Rigidbody2D>().velocity = velocity;
        }

        // All ghost positions will be relative to the owner transform,
        Vector3 ghostPosition = transform.position;

        // We're positioning the ghosts clockwise behind the edges of the screen.
        // Let's start with the far right.
        ghostPosition.x = transform.position.x + screenWidth;
        ghostPosition.y = transform.position.y;
        ghosts[0].transform.position = ghostPosition;


        // Bottom-right
        ghostPosition.x = transform.position.x + screenWidth;
        ghostPosition.y = transform.position.y - screenHeight;
        ghosts[1].transform.position = ghostPosition;

        // Bottom
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y - screenHeight;
        ghosts[2].transform.position = ghostPosition;

        // Bottom-left
        ghostPosition.x = transform.position.x - screenWidth;
        ghostPosition.y = transform.position.y - screenHeight;
        ghosts[3].transform.position = ghostPosition;

        // Left
        ghostPosition.x = transform.position.x - screenWidth;
        ghostPosition.y = transform.position.y;
        ghosts[4].transform.position = ghostPosition;

        // Top-left
        ghostPosition.x = transform.position.x - screenWidth;
        ghostPosition.y = transform.position.y + screenHeight;
        ghosts[5].transform.position = ghostPosition;

        // Top
        ghostPosition.x = transform.position.x;
        ghostPosition.y = transform.position.y + screenHeight;
        ghosts[6].transform.position = ghostPosition;

        // Top-right
        ghostPosition.x = transform.position.x + screenWidth;
        ghostPosition.y = transform.position.y + screenHeight;
        ghosts[7].transform.position = ghostPosition;

        // All ghosts should have the same rotation as the main ship
        for (int i = 0; i < 8; i++)
        {
            ghosts[i].transform.rotation = transform.rotation;
        }

        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().velocity = oldVelocity;
        }
    }

    void SwapGhosts()
    {
        foreach (var ghost in ghosts)
        {
            if (ghost.GetComponent<SpriteRenderer>() != null && bCanWrap)
            {
                if (ghost.GetComponent<SpriteRenderer>().isVisible)
                {
                    transform.position = ghost.transform.position;
                    bCanWrap = false;
                    Invoke("ResetWrapping", 0.1f);
                    break;
                }
            }

        }
    }

    void ResetWrapping()
    {
        bCanWrap = true;
    }


}
