using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject character;
    bool cameraAttached = true;

	// Use this for initialization
	void Start () {
        Vector3 charPosition = character.transform.position;
        this.gameObject.transform.position = new Vector3(charPosition.x, charPosition.y + 2.2f, charPosition.z + 4f); 
	}
	
	// Update is called once per frame
	void Update () {
        if (cameraAttached)
        {
            Vector3 charPosition = character.transform.position;
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,
                this.gameObject.transform.position.y, charPosition.z + 3f);
        }
	}

    public void AdjustCamera(float xValue)
    {
        this.gameObject.transform.Translate(xValue, 0, 0);
    }

    public void DetachCamera()
    {
        cameraAttached = false;
    }

    
}
