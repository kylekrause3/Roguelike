using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region vars
    //Health
    public HealthBar healthBar;
    public float maxHealth = 100;
    public float currenthealth;

    //Inventory
    Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region init
        //Health
        currenthealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //Inventory
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(20, 2, 20), new Item { itemType = Item.Type.SpeedBoost, amt = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(-20, 2, 20), new Item { itemType = Item.Type.AttackBoost, amt = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(20, 2, -20), new Item { itemType = Item.Type.JumpBoost, amt = 1 });
        #endregion
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
