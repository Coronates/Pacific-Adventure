using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour {

    bool alive = true;
    bool grounded = true;

    public float moveSpeed = 0.01f;
    public float attackSpeed =  1f;
    float coolDown;

    float momentum = 0;

    //El personaje solo salta si estpa en el suelo
    int jump = 0;

    int maxJumpCycles = 15;

    //tiempo para la distribución de la fuerza
    int left = 0;
    int right = 0;
    int maxLeftCycles = 30;
    int maxRightCycles = 30;


    public bool bombFired = false;


    public Camera mainCamer;

    public Material skybox;

    //public GameObject bulletPrefab;
    public Rigidbody bulletRB;
    public Transform bulletSpawn;
    private GameObject[] bombs;
    private GameObject[] planes;

    public ParticleSystem particles;


    // Use this for initialization
    void Start () {
        jump = maxJumpCycles;
        left = maxLeftCycles;
        right = maxRightCycles;


        bombs = GameObject.FindGameObjectsWithTag("Bomb");
        planes = GameObject.FindGameObjectsWithTag("Airplane");

        foreach (GameObject b in bombs)
        {
            b.GetComponent<Rigidbody>().transform.position = (new Vector3(this.gameObject.transform.position.x,
                b.gameObject.transform.position.y,
                b.gameObject.transform.position.z));
            
        }

        foreach (GameObject p in planes)
        {
            p.GetComponent<Rigidbody>().transform.position = (new Vector3(this.gameObject.transform.position.x,
                p.gameObject.transform.position.y,
                p.gameObject.transform.position.z));
        }

        AudioManager.PlaySound("ship");




    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            momentum += 0.0005f;
            right = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            momentum += 0.0005f;
            left = 0;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            momentum = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu 1");
        }


        if(Time.time >= coolDown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
                AudioManager.PlaySound("shot");
            }
        }

        ActivateTrainsInRange();
        checkIfDied();
        updateSkybox();


        //Debug.Log(right + " " + left + " " + jump + " ");
    }


    private void ActivateTrainsInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, 20f);
        foreach(Collider c in colliders)
        {
           
            if(c.gameObject.GetComponent<TrainController>() != null)
            {
                c.gameObject.GetComponent<TrainController>().SignalToMove();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            Debug.Log("the bomb hits you");
            Explode(other, particles);
            
        }
        if (other.gameObject.tag == "levelEnd")
        {
            Debug.Log("next level");
            SceneManager.LoadScene("main");

        }
    }

    void Explode(Collider other, ParticleSystem ps)
    {
       
            ParticleSystem exp = Instantiate(ps, other.transform.position, other.transform.rotation) as ParticleSystem;
            exp.Play();
            Destroy(exp, exp.duration);
            SceneManager.LoadScene("Menu 1");


    }

    void Fire()
    {
        Rigidbody clone;
        clone = Instantiate(bulletRB, bulletSpawn.position, bulletSpawn.rotation) as Rigidbody;
        clone.AddForce(bulletSpawn.forward * 500);
        //clone.velocity = clone.transform.forward * 16;

        //var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 16;

        //bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * 500);

        //NetworkServer.Spawn(bullet);

        //Destroy(bullet, 2.0f);
        Destroy(clone.gameObject, 1.5f);
        coolDown = Time.time + attackSpeed;
    }

    void checkIfDied()
    {
        if(this.gameObject.transform.position.y < -5)
        {
            SceneManager.LoadScene("main");
        }
    }


    void updateSkybox()
    {
        skybox.SetFloat("_AtmosphereThickness", Mathf.Repeat(Time.time / 20, 5f));
    }

    private void FixedUpdate()
    {
        if (alive)
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1);

        if(jump < maxJumpCycles)
        {
            this.GetComponent<Animator>().speed = 0;
            this.GetComponent<Rigidbody>().AddForce(0, 600, 500);
            jump++;
            grounded = false;
        }

        /*if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            resetCamera();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            resetCamera();
        }*/

        if (right < maxRightCycles)
        {

            this.GetComponent<Rigidbody>().transform.position = new Vector3(this.gameObject.transform.position.x - (moveSpeed+momentum),
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z);

            /*this.GetComponent<Rigidbody>().MovePosition(
                new Vector3(
                    this.gameObject.transform.position.x - (1f / maxRightCycles),
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z));*/

            mainCamer.SendMessage("AdjustCamera", +((moveSpeed + momentum)));


            if (!bombFired) { 

            foreach (GameObject p in planes)
            {
                p.GetComponent<Rigidbody>().transform.position = (new Vector3(this.gameObject.transform.position.x,
                    p.gameObject.transform.position.y,
                    p.gameObject.transform.position.z));
            }

            foreach (GameObject b in bombs)
            {
                b.GetComponent<Rigidbody>().transform.position = (new Vector3(this.gameObject.transform.position.x,
                    b.gameObject.transform.position.y,
                    b.gameObject.transform.position.z));
            }
        }

            right++;
        }

        if(left < maxLeftCycles)
        {

            this.GetComponent<Rigidbody>().transform.position = new Vector3(this.gameObject.transform.position.x + (moveSpeed + momentum),
                   this.gameObject.transform.position.y,
                   this.gameObject.transform.position.z); 

            /*this.GetComponent<Rigidbody>().MovePosition(
                new Vector3(
                    this.gameObject.transform.position.x + (1f / maxLeftCycles),
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z));*/

            mainCamer.SendMessage("AdjustCamera", -((moveSpeed + momentum)));

            if (!bombFired)
            {

                foreach (GameObject p in planes)
                {
                    
                    p.GetComponent<Rigidbody>().transform.position = new Vector3(this.gameObject.transform.position.x,
                        p.gameObject.transform.position.y,
                        p.gameObject.transform.position.z);
                }

                foreach (GameObject b in bombs)
                {
                    b.GetComponent<Rigidbody>().transform.position = new Vector3(this.gameObject.transform.position.x,
                        b.gameObject.transform.position.y,
                        b.gameObject.transform.position.z);
                }
            }

            left++;
        }

        



        //cc.resetCamera();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            SceneManager.LoadScene("Menu 1");

        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.GetComponent<Animator>().speed = 1;
            grounded = true;
        }

        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            alive = false;

            this.GetComponent<Animator>().speed = 0;
            Debug.Log("test");
            mainCamer.GetComponent<CameraController>().DetachCamera();
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            this.GetComponent<Rigidbody>().AddForce(1500f, 1500f, 1000f);
        }
    }*/
}
