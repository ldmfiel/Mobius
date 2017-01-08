using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnComponent : MonoBehaviour {
    public GameObject playerPrefab;

    void Start()
    {
        if(GameObject.Find("PlayerObject") == null)
        {
            Instantiate(playerPrefab, transform.position, new Quaternion(0, 0, 0,0));
        }
    }
}
