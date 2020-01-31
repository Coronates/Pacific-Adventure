using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioClip bomb, airplane, ship, shot, gunshot;
    public static AudioSource audioSrc;


	// Use this for initialization
	void Start () {

        bomb = Resources.Load<AudioClip>("bombExp");
        airplane = Resources.Load<AudioClip>("airplane");
        ship = Resources.Load<AudioClip>("ship");
        shot = Resources.Load<AudioClip>("shot");
        gunshot = Resources.Load<AudioClip>("gunshot");
        audioSrc = this.GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "bomb":
                audioSrc.PlayOneShot(bomb);
                break;
            case "airplane":
                audioSrc.PlayOneShot(airplane);
                break;
            case "ship":
                audioSrc.PlayOneShot(ship);
                break;
            case "shot":
                audioSrc.PlayOneShot(shot);
                break;
            case "gunshot":
                audioSrc.PlayOneShot(gunshot);
                break;
            
        }
    }
}
