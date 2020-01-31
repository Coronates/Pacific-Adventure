using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSetup {


    public float angularSpeedDampTime = 0.6f;
    public float angularResponsetime = 0.5f;
    public float angularSpeed;


    private Animator anim;

    public AnimSetup(Animator animator)
    {
        anim = animator;
    }

    public void Setup(float angle)
    {
        angularSpeed = angle / angularResponsetime;
        anim.SetFloat("AngularSpeed", angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }
}
