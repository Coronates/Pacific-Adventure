using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthBar;
    GameObject HealthBar;
    private Animator anim;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        HealthBar = GameObject.FindGameObjectWithTag("HealthBar");
        
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            anim.SetBool("CantDie", false);
            //this.gameObject.transform.Translate(0, -0.6f, 0);
            anim.SetBool("bossHit", true);
            Destroy(HealthBar, 1f);
            currentHealth = 0;
            Debug.Log("dead");
        }


        if(healthBar != null) { 
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        }
    }
}
