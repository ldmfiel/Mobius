using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTriggerComponent : MonoBehaviour {

    bool canActivate = false;
    GameObject playerObject;
    ElevatorPopup elevatorPopup;
    
	// Use this for initialization
	void Start () {
        elevatorPopup = GetComponent<ElevatorPopup>();
        		
	}
	
	// Update is called once per frame
	void Update () {


        if(elevatorPopup.menuOpen)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                elevatorPopup.menuOpen = false;
                playerObject.SendMessage("SwitchState", EControlState.PLAY);
            }

            if (Input.GetButtonDown("Up"))
            {
                elevatorPopup.ChangeOption(false);
            }

            if (Input.GetButtonDown("Down"))
            {
                elevatorPopup.ChangeOption(true);
            }

            if (Input.GetButtonDown("Accept") && canActivate && elevatorPopup.menuOpen)
            {
                elevatorPopup.GoToRoom();

            }
        }
        else
        {
            if (Input.GetButtonDown("Submit") && canActivate && !elevatorPopup.menuOpen)
            {
                elevatorPopup.OpenMenu();
                playerObject.SendMessage("SwitchState", EControlState.UI);
                playerObject.SendMessage("StopMovement");
            }
        }



    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canActivate = true;
            playerObject = col.gameObject;

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canActivate = false;
            playerObject = null;
        }
    }
}
