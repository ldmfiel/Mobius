using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectInDirectionComponent : MonoBehaviour {

    public Vector3 Direction;
    public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPostion = transform.position;

        // Move object using direction.
        newPostion += ((Direction*Speed) * Time.deltaTime);

        transform.position = newPostion;
	}
}
