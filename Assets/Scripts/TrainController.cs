using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

    public bool moving = false;
    public float speed = 1f;

    private bool signaledmove = false;

    public GameObject surface;
    public Rigidbody bombRB;
    public Transform bombSpawn;

    private void FixedUpdate()
    {
        if (moving && signaledmove)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.back * 250 * -speed);
        }

        if (this.gameObject.transform.position.z < -520 ||
            this.gameObject.transform.position.y < -20)
            Destroy(this.gameObject);
    }


    public void SignalToMove()
    {
        signaledmove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Bomb")
        {
            player.GetComponent<CharController>().bombFired = true;
            Debug.Log("fire!");
            other.gameObject.transform.localScale = new Vector3(1, 1, 1);
            other.GetComponent<Rigidbody>().useGravity = true;
        }*/

        if (other.gameObject.tag == "FirePlace")
        {
            Debug.Log("fire!");

            Rigidbody clone;
            clone = Instantiate(bombRB, bombSpawn.position, bombSpawn.rotation) as Rigidbody;
            clone.useGravity = true;

            surface.GetComponent<SurfaceController>().bombInstantiated = true;
        }

        if (other.gameObject.tag == "AudioPlace")
        {
            AudioManager.PlaySound("airplane");
        }
    }


}
