using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitComponent : MonoBehaviour {

    public string nextLevel;
    public string nextSpawnPoint;

    bool canActivate;
    RoomController roomControl;

	// Use this for initialization
	void Start () {
        roomControl = GameObject.Find("RoomController").GetComponent<RoomController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Submit") && canActivate)
        {
            roomControl.SwitchRoom(nextLevel,nextSpawnPoint);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            canActivate = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canActivate = false;
        }
    }

}
