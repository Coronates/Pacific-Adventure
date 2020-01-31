using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetection : MonoBehaviour {

    public LayerMask ignoreRaycast;
    public float fieldOfView = 80.0f;
    public bool playerDetected;

    private Animator anim;
    private GameObject player;
    private SphereCollider col;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        col = GetComponent<SphereCollider>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("PlayerDetected", playerDetected);

       
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            //playerDetected = true;
            playerDetected = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            Debug.DrawRay(transform.position + transform.up /2, direction, Color.green);

            if(angle < fieldOfView)
            {
                RaycastHit hit;

                if(Physics.Raycast(transform.position + transform.up /2, direction, out hit, col.radius, ignoreRaycast))
                {
                    if(hit.collider.gameObject == player)
                    {
                        //Debug.Log("The monster has detected you!");
                        //anim.SetFloat("AngularSpeed", 0.8f);
                        
                        playerDetected = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerDetected = false;
            //Debug.Log("You're safe now!");
            //anim.SetFloat("AngularSpeed", 0.0f);
        }
    }
}
