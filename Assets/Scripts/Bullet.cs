using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {



    public ParticleSystem particles;
    private bool PSplayed = false;

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;

        if (collision.gameObject.tag == "Ship" && collision.gameObject != null) 
        {
            Destroy(gameObject);
            Explode(collision, particles);

        }
    }

    void Explode(Collision collision, ParticleSystem ps)
    {
        if (!PSplayed) { 
        ParticleSystem exp = Instantiate(ps, collision.transform.position, collision.transform.rotation) as ParticleSystem;
        exp.Play();
        AudioManager.PlaySound("bomb");
        Destroy(exp, exp.duration);
        Destroy(collision.gameObject);
        PSplayed = true;
    }
    }
}
