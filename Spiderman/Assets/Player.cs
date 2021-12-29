using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region vars
    public HealthBar healthBar;
    public float maxHealth = 100;
    public float currenthealth;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(5f);
        }
    }

    void TakeDamage(float damage)
    {
        currenthealth -= damage;
        healthBar.SetHealth(currenthealth);
    }
}
