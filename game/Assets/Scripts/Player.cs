using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region vars
    [Header("General Vars")]
    public GameObject playermodel;

    [Header("Health")]
    public HealthBar healthBar;
    public float maxHealth = 100;
    public float currenthealth;

    [Header("Inventory")]
    [SerializeField] private UI_Inventory uiInventory;
    Inventory inventory;


    [Header("Movement")]
    public float jumpheight = 3f;
    public float speed = 6f;
    public float gravity = 10f;

    public CharacterController charcontroller;
    public Transform camtransform;
    public Transform groundCheck;
    public LayerMask groundMask;
    thirdpersonmovement mvmt;


    #endregion
    void Awake()
    {
        currenthealth = maxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        #region init
        healthBar.SetMaxHealth(maxHealth);

        //Inventory
        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);

        //Movement
        mvmt = new thirdpersonmovement(jumpheight, speed, gravity, charcontroller, camtransform, groundCheck, groundMask);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(5f);
        }

        mvmt.Movement();
    }

    void TakeDamage(float damage)
    {
        currenthealth -= damage;
        healthBar.SetHealth(currenthealth);
    }

    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            //ENTER USE STUFF HERE, LIKE ADDING BUFFS
            //EX:
            case Item.Type.SpeedBoost:
                //movement.addspeed or whatever to apply the buff
                inventory.RemoveItem(new Item { itemType = Item.Type.SpeedBoost, amt  = 1 });
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        ItemWorld itemWorld = col.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            //touching the item
            inventory.AddItem(itemWorld.GetItem());
            //uiInventory.SetInventory(inventory);
            itemWorld.DestroySelf();
        }
    }

}
