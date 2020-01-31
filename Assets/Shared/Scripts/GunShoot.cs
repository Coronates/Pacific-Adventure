using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GunShoot : MonoBehaviour
{

    public float fireRate = 0.25f;                                      // Number in seconds which controls how often the player can fire
    public float weaponRange = 20f;                                     // Distance in Unity units over which the player can fire

    public Transform gunEnd;
    public ParticleSystem muzzleFlash;
    public ParticleSystem cartridgeEjection;

    public GameObject metalHitEffect;
    public GameObject sandHitEffect;
    public GameObject stoneHitEffect;
    public GameObject waterLeakEffect;
    public GameObject waterLeakExtinguishEffect;
    public GameObject[] fleshHitEffects;
    public GameObject woodHitEffect;

    public GameObject boss;

    private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing
    private Animator anim;
    private GunAim gunAim;


    private Animator bossAnim;

    void Start()
    {
        anim = GetComponent<Animator>();
        bossAnim = boss.GetComponent<Animator>();
        gunAim = GetComponentInParent<GunAim>();

        

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && !gunAim.GetIsOutOfBounds())
        {
            nextFire = Time.time + fireRate;
            muzzleFlash.Play();
            cartridgeEjection.Play();
            anim.SetTrigger("Fire");

            //Debug.Log("Shot");

            Vector3 rayOrigin = gunEnd.position;
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, gunEnd.forward, out hit, weaponRange))
            {
                HandleHit(hit);
            }
            AudioManager.PlaySound("gunshot");


        }
    }

    void HandleHit(RaycastHit hit)
    {
        if (hit.collider.sharedMaterial != null)
        {
            bossAnim.SetBool("PlayerDetected", false);

            string materialName = hit.collider.sharedMaterial.name;

            switch (materialName)
            {
                case "Metal":
                    SpawnDecal(hit, metalHitEffect);
                    break;
                case "Sand":
                    SpawnDecal(hit, sandHitEffect);
                    break;
                case "Stone":
                    SpawnDecal(hit, stoneHitEffect);
                    break;
                case "WaterFilled":
                    SpawnDecal(hit, waterLeakEffect);
                    SpawnDecal(hit, metalHitEffect);
                    break;
                case "Wood":
                    SpawnDecal(hit, woodHitEffect);
                    break;
                case "Meat":
                    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    Debug.Log("hit!");
                   // bossAnim.SetBool("bossHit", true);

                    var health = boss.gameObject.GetComponent<Health>();
                    if(health != null)
                    {
                        health.TakeDamage(10);
                    }

                    break;
                case "Character":
                    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
                case "WaterFilledExtinguish":
                    SpawnDecal(hit, waterLeakExtinguishEffect);
                    SpawnDecal(hit, metalHitEffect);
                    break;
            }
        }
    }

    void SpawnDecal(RaycastHit hit, GameObject prefab)
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnedDecal.transform.SetParent(hit.collider.transform);
    }
}