using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour
{
    public Image healthBar;
    public Transform player;
    public float maxHealth = 100;
    private float currentHealth;

	void Start ()
    {
        currentHealth = maxHealth;
	}
	
	void Update ()
    {
        //keeps the healthbar facing player
        healthBar.transform.LookAt(player);

        //adjusts size equal to current health
        healthBar.fillAmount = currentHealth / maxHealth;

        //removes enemy if health reaches 0
		if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
	}

    public void takeDamage(float amount)
    {
        currentHealth -= amount;
    }
}
