using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General Vars")]
    public GameObject playermodel;

    [Header("Health")]
    public HealthBar healthBar;
    public float maxHealth;
    public float currenthealth;
    public float regenerationAmount;
    public float regenerationTime;
    float lastTimeHit;
    int lastTimeHitSecs;


    void Awake()
    {
        currenthealth = maxHealth;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(5f);
        }

        //regen
        if (currenthealth < maxHealth)
            if ((int)(Time.time % 60) >= lastTimeHitSecs + regenerationTime)
                Heal(regenerationAmount * Time.deltaTime);

        //cleans up health
        if ((int)currenthealth == (int)maxHealth)
            currenthealth = maxHealth;
    }


    void TakeDamage(float damage)
    {
        currenthealth -= damage;
        healthBar.SetHealth(currenthealth);
        lastTimeHit = Time.time;
        lastTimeHitSecs = (int)(Time.time % 60);
    }

    public void Heal(float amt)
    {
        currenthealth += amt;
        healthBar.SetHealth(currenthealth);
    }
}
