using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        tk2dTileMap TileMap = GetComponent<tk2dTileMap>();
        TileMap.renderData.transform.position = transform.position;
    }
}
