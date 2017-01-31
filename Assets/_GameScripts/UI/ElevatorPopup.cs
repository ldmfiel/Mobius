using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPopup : MonoBehaviour 
{
	//VARIABLES
	public bool menuOpen; //is the menu open?
	ElevatorDataComponent elevatorComponent; //data
	Vector2 boxPosition; //where the box should be drawn on screen
	Vector2 textStartPos; //where the text should be drawn on screen
	float textOffset = 5; //offset for each line of text
	int numberOfLines, currentLine; //number of lines needed and the current line we are on when drawing
	Camera worldCamera; //reference to the camera, so we can convert to screenspace
	//Vector3 screenPosition; //the screen position of the gameobject in world
	int currentOption = 1; //the current option we have selected
    RoomController roomControl;
    

	// Set up all variables
	void Start () 
	{
        //find camera
        worldCamera = Camera.main;

		//find the data
		elevatorComponent = this.gameObject.GetComponent<ElevatorDataComponent> ();

		//find out how many rooms we can access
		foreach (ElevatorDataComponent.ElevatorData room in elevatorComponent.DataArray) 
		{
			if (room.CanAccess == true) 
			{
				numberOfLines++;
			}
		}

        roomControl = GameObject.Find("RoomController").GetComponent<RoomController>();

        //Debug.Log (elevatorComponent.DataArray.Length);
        //OpenMenu ();
	}

	void Update()
	{
        // Moved this here to update gui in realtime when i make changes in editor play.
        if(menuOpen)
        {
            //set screen position depending on objects world position
            //screenPosition = worldCamera.WorldToScreenPoint(transform.position);
            boxPosition.x = Screen.width / 2 - elevatorComponent.BoxWidth / 2;
            boxPosition.y = 15;
            //Debug.Log(screenPosition);
            //draw box with the offsets
            //boxPosition.x = screenPosition.x + elevatorComponent.PopupOffsetX;
            //boxPosition.y = screenPosition.y + elevatorComponent.PopupOffsetY;

            //make sure text starts drawing where the box is, with an offset
            textStartPos.x = boxPosition.x + textOffset;
            textStartPos.y = boxPosition.y;
        }
    }
	


	//Function to call when you want the popup menu to appear
	public void OpenMenu ()
	{
		menuOpen = true;
	}

	//Change selected option in list via controller or keyboard input
	public void ChangeOption(bool down)
	{
		if (down == false) 
		{
			if (currentOption != 1) 
			{
				currentOption--;
			} 
			else if (currentOption == 1) 
			{
				currentOption = numberOfLines;
			}
		} 
		else if (down == true) 
		{
			if (currentOption != numberOfLines) 
			{
				currentOption++;
			} 
			else if (currentOption == numberOfLines) 
			{
				currentOption = 1;
			}
		}

		//Debug.Log (currentOption);
	}

	//OPEN UP THE SELECTED ROOM
	public void GoToRoom()
	{
        if(roomControl)
        {
            roomControl.SwitchRoom(elevatorComponent.DataArray[currentOption - 1].SceneName,"ElevatorSpawn");
        }
	}

	//checks which line we are on in list and adds an offset for each line
	void CheckLine()
	{
		currentLine++;
		textOffset = textOffset + 15;

		if (currentLine >= numberOfLines) 
		{
			currentLine = 0;
			textOffset = 5;
		}
	}

	//Handles drawing the popup menu to the screen
	void OnGUI ()
	{
		//if the menu is open, drawn the box and text
		if (menuOpen == true) 
		{
            GUI.skin = elevatorComponent.menuSkin;
            
            //draw the background box at specified width and to the correct height depending on amount of available rooms
			GUI.Box(new Rect(boxPosition.x, boxPosition.y, elevatorComponent.BoxWidth, 15 + (15 * numberOfLines)), Texture2D.blackTexture);

            //draw the arrow on the screen
            //GUI.Box(new Rect(boxPosition.x + elevatorComponent.BoxWidth / 6 * 4, boxPosition.y - 5 + elevatorComponent.ArrowOffset * currentOption, 20, 10), Texture2D.blackTexture);
            GUI.DrawTexture(new Rect(boxPosition.x + elevatorComponent.BoxWidth / 6 * 4, boxPosition.y - 5 + elevatorComponent.ArrowOffset * currentOption, 20, 10), elevatorComponent.Arrow);

            //draw each room name in order
            foreach (ElevatorDataComponent.ElevatorData room in elevatorComponent.DataArray)
			{
				//if the room is accessible, draw on screen
				if (room.CanAccess == true) 
				{
					GUI.Label (new Rect (textStartPos.x + 5, textStartPos.y + textOffset, 350, 40), room.RoomName);
					CheckLine ();
				}
			}
		}
	}

}
