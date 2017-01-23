using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour {

    [System.Serializable]
    public struct Room
    {
        public string RoomName;
        public string DefaultSpawn;
        public string[] spawnPoints;
    }

    public string CurrentRoomName;
    public Room CurrentRoom;
    public Room[] Rooms;

    Dictionary<string, Room> rooms;
    bool defaultSpawn = false;
    string currentSpawn;

	// Use this for initialization
	void Start () {

        //Store rooms structs
        rooms = new Dictionary<string, Room>();
		for(int i = 0; i < Rooms.Length; i++)
        {
            rooms.Add(Rooms[i].RoomName,Rooms[i]);

            if(Rooms[i].RoomName == CurrentRoomName)
            {
                CurrentRoom = Rooms[i];
            }
        }



        SceneManager.sceneLoaded += OnSceneLoad;

        DontDestroyOnLoad(this);
        OpenFirstRoom();

    }
	
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameObject playerSpawn;

        if(defaultSpawn)
        {
            Debug.Log(CurrentRoom.DefaultSpawn);
            playerSpawn = GameObject.Find(CurrentRoom.DefaultSpawn);
            playerSpawn.GetComponent<PlayerSpawnComponent>().SpawnPlayer();
        }
        else
        {
            playerSpawn = GameObject.Find(currentSpawn);
            playerSpawn.GetComponent<PlayerSpawnComponent>().SpawnPlayer();
        }
    }

	// Update is called once per frame
	void Update () {

	}

    //Open the default room
    void OpenFirstRoom()
    {
        Room newRoom;

        if (rooms.TryGetValue(CurrentRoomName, out newRoom))
        {
            defaultSpawn = true;
            SceneManager.LoadScene(CurrentRoomName,LoadSceneMode.Single);
        }

    }
    
    //Load level at specific spawn
    public void SwitchRoom(string name, string SpawnPoint)
    {
        SwitchRoom(name);
        SetPlayerSpawn(SpawnPoint);
    }

    // Load room
    public void SwitchRoom(string name)
    {
        Room newRoom;

        if(rooms.TryGetValue(name, out newRoom))
        {
            SceneManager.LoadScene(name);
        }
    }

    // Set the current spawn the player will appear at on level load.
    void SetPlayerSpawn(string name)
    {
        currentSpawn = name;

        if (defaultSpawn)
        {
            defaultSpawn = false;
        }
    }

    
}
