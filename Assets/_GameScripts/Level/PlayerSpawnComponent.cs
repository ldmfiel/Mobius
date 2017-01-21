using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnComponent : MonoBehaviour {
    public GameObject playerPrefab;

    GameObject player;

    void Start()
    {

    }

    public void SpawnPlayer()
    {
        if (GameObject.Find("PlayerObject") == null)
        {
            player = Instantiate(playerPrefab, transform.position, new Quaternion(0, 0, 0, 0));
        }
    }
}
