using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingObjectComponent : MonoBehaviour {

    bool bCanWrap = true;
    public bool bWrapVert;
    public bool bWrapHoriz;
    public bool bLosesCollison;

    Camera cameraObject;
    Vector3 screenBottomLeft, screenTopRight;

    public float screenWidth, screenHeight;
    GameObject[] ghosts = new GameObject[8];

    public GameObject ghostPref;

	// Use this for initialization
    void Start () {
        int ghostRequirement = 0;

        if(bWrapHoriz)
        {
            ghostRequirement += 2;
        }

        if(bWrapVert && bWrapHoriz)
        {
            ghostRequirement += 6;
        }
        else if(bWrapVert)
        {
            ghostRequirement += 8;
        }

        cameraObject = Camera.main;

        screenBottomLeft = cameraObject.ViewportToWorldPoint(new Vector3(0,0,transform.position.z));
        screenTopRight = cameraObject.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.y - screenBottomLeft.y;

        CreateGhosts(ghostRequirement);
        PositionGhosts(ghostRequirement);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent == null)
        {
            SwapGhosts();            
        }
	}

    //Create Ghost Images

    void CreateGhosts(int ghostRequirement)
    {
        ghosts = new GameObject[ghostRequirement];

        for(int i = 0; i < ghostRequirement; i++)
        {
            ghosts[i] = Instantiate(ghostPref, Vector3.zero, Quaternion.identity) as GameObject;
            ghosts[i].transform.parent = transform;
            ghosts[i].transform.position = transform.position;

            switch(i)
            {
                case 2:
                    ghosts[i].name = "Top Clone";
                    break;
                case 3:
                    ghosts[i].name = "Bottom Clone";
                    break; 
            }
        }
    }

    //Position Ghosts so that they will be on the screen as the main objects leaves the screen.
    void PositionGhosts(int ghostRequirement)
    {       

        // All ghost positions will be relative to the owner transform,
        Vector3 ghostPosition = transform.position;

        // We're positioning the ghosts clockwise behind the edges of the screen.
        
        if(ghostRequirement == 2 || ghostRequirement == 8)
        {
            //right.
            ghostPosition.x = transform.position.x + screenWidth;
            ghostPosition.y = transform.position.y;
            ghosts[0].transform.position = ghostPosition;

            // Left
            ghostPosition.x = transform.position.x - screenWidth;
            ghostPosition.y = transform.position.y;
            ghosts[1].transform.position = ghostPosition;
        }

        if (ghostRequirement == 8)
        {

            // Top
            ghostPosition.x = transform.position.x;
            ghostPosition.y = transform.position.y + screenHeight;
            ghosts[2].transform.position = ghostPosition;

            // Bottom
            ghostPosition.x = transform.position.x;
            ghostPosition.y = transform.position.y - screenHeight;
            ghosts[3].transform.position = ghostPosition;

            // Bottom-right
            ghostPosition.x = transform.position.x + screenWidth;
            ghostPosition.y = transform.position.y - screenHeight;
            ghosts[4].transform.position = ghostPosition;

            // Bottom-left
            ghostPosition.x = transform.position.x - screenWidth;
            ghostPosition.y = transform.position.y - screenHeight;
            ghosts[5].transform.position = ghostPosition;

            // Top-left
            ghostPosition.x = transform.position.x - screenWidth;
            ghostPosition.y = transform.position.y + screenHeight;
            ghosts[6].transform.position = ghostPosition;

            // Top-right
            ghostPosition.x = transform.position.x + screenWidth;
            ghostPosition.y = transform.position.y + screenHeight;
            ghosts[7].transform.position = ghostPosition;
        }




        // All ghosts should have the same rotation as the main ship
        for (int i = 0; i < ghostRequirement; i++)
        {
            ghosts[i].transform.rotation = transform.rotation;
        }


    }

    void SwapGhosts()
    {
        foreach (var ghost in ghosts)
        {
            if (ghost)
            {


                if (ghost.GetComponent<SpriteRenderer>() != null && bCanWrap)
                {
                    if (ghost.GetComponent<SpriteRenderer>().isVisible && gameObject.GetComponent<SpriteRenderer>().isVisible != true)
                    {
                        transform.position = ghost.transform.position;
                        bCanWrap = false;
                        Invoke("ResetWrapping", 0.1f);

                        if(bLosesCollison)
                            GetComponent<BoxCollider2D>().isTrigger = true;

                        break;
                    }
                }
            }

        }
    }

    void ResetWrapping()
    {
        bCanWrap = true;
    }


}
