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
    public float maxHealth;
    public float currenthealth;
    public float regenerationAmount;
    public float regenerationTime;
    float lastTimeHit;
    int lastTimeHitSecs;

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

    //[Header("Attacking")]
    Transform firepoint;
    Gun gun;


    #endregion
    void Awake()
    {
        currenthealth = maxHealth;
        firepoint = camtransform;
        //firepoint.position.z += 3f; TODO
    }
    // Start is called before the first frame update
    void Start()
    {
        
        healthBar.SetMaxHealth(maxHealth);

        //Inventory
        inventory = new Inventory(UseItem, this);
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);

        //Movement
        mvmt = new thirdpersonmovement(this, speed, gravity, charcontroller, camtransform, groundCheck, groundMask);

        //Attacking
        gun = new Gun(); //gun w/ damage = 10f, range = Mathf.Infinity, fireRate = 1f (per second)

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mvmt.Movement(speed, jumpheight);

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(5f);
        }
        if(Input.GetAxisRaw("Fire1") != 0 || Input.GetKeyDown(KeyCode.L))
        {
            gun.shoot(camtransform);
        }


        //regen
        if(currenthealth < maxHealth)
            if((int)(Time.time % 60) >= lastTimeHitSecs + regenerationTime)
                Heal(regenerationAmount * Time.deltaTime);
            
        
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

    public void AddBuff(Item item)
    {
        switch (item.itemType)
        {
            case Item.Type.JumpBoost: jumpheight += item.intensity; break;
            case Item.Type.SpeedBoost: speed += item.intensity; break;
            case Item.Type.RegenBoost: 
                regenerationTime -= item.intensity;
                regenerationAmount *= 1.5f;
                break;
            case Item.Type.AttackBoost: /* need to code in attacking */ break;
            default: break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        ItemWorld itemWorld = col.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            Item item = itemWorld.GetItem();
            inventory.AddItem(item);
            itemWorld.DestroySelf();
        }
    }

}
