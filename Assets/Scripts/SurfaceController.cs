using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceController : MonoBehaviour {

    public GameObject player;
    private GameObject ps;
    public bool bombInstantiated = false;


    private void Start()
    {
        
    }

    private void Update()
    {
        if (bombInstantiated)
        {
            //Debug.Log("Se ha creado el misil");
            ps = GameObject.FindGameObjectWithTag("Splash");
            ps.active = false;
            bombInstantiated = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            player.GetComponent<CharController>().bombFired = false;
            Debug.Log("bombFired reset");
            //cargar particle system
            ps.active = true;
            Destroy(ps, 1f);

        }
    }


    void Explode(Collider other, ParticleSystem ps)
    {
        
            ParticleSystem exp = Instantiate(ps, other.transform.position, other.transform.rotation) as ParticleSystem;
            exp.Play();
            Destroy(exp, exp.duration);
            //Destroy(collision.gameObject);
    }
}
