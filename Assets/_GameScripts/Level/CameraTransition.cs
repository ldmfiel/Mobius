using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour {

    public GameObject CameraTarget;
    public GameObject PairedTransition;
    bool triggered;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(triggered)
        {
            float t = 0.0f;
            while(t < 3.0f)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, CameraTarget.transform.position, Time.deltaTime);

                if (Vector3.Distance(Camera.main.transform.position,CameraTarget.gameObject.transform.position) <= 0.1f)
                {
                    triggered = false;
                    PairedTransition.SetActive(true);
                    gameObject.SetActive(false);
                }

                t += Time.deltaTime;
            }

        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {

        triggered = true;

        Debug.Log("TRIGGERED");
        
    }

}
