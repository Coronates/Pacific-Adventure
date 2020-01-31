using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAnimation : MonoBehaviour {

    private Transform player;
    private BossDetection BossDetection;
    private Animator anim;
    private AnimSetup animSetup;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        BossDetection = GetComponent<BossDetection>();
        anim = GetComponent<Animator>();

        animSetup = new AnimSetup(anim);
    }

    void Update()
    {
        AnimSetup();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu 1");
        }
    }


    public void AnimSetup()
    {
        float angle;

        if (BossDetection.playerDetected)
        {
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
           
        }
        else
        {
            angle = 0.0f;
        }

        animSetup.Setup(angle);
    }

    void OnAnimatorMove()
    {
        transform.position = anim.rootPosition;
        transform.rotation = anim.rootRotation;
    }

    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if(toVector == Vector3.zero)
        {
            return 0.0f;
        }

        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);

        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;

        return angle;
    }
}
