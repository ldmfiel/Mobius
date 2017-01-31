using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableSoundComponent : MonoBehaviour {

    public string name = "Sensable";
    public float maxDistance = 300f;

    float timer;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SoundMade(string nameOfSound)
    {
        if(nameOfSound == name)
        {
            GameObject[] NPCS = GameObject.FindGameObjectsWithTag("NPC");

            foreach (GameObject g in NPCS)
            {
                if (Vector3.Distance(g.transform.position, transform.position) < maxDistance)
                {
                    g.SendMessage("OnSoundDetected", gameObject);
                }

            }
        }

    }
}
