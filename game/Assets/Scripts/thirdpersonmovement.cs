using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdpersonmovement : MonoBehaviour
{
    #region vars
    public GameObject model;

    public CharacterController controller;
    public Transform cam;

    Vector3 movevert;
    public float jumpheight;

    public float speed;
    float activespeed;

    public float gravity;
    bool grounded;
    public Transform groundCheck;
    float groundCheckSize = .1f;
    public LayerMask groundMask;


    bool paused = false;
    #endregion
    void Awake()
    {
        
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    public thirdpersonmovement(float jumpheight, float speed, float gravity, CharacterController controller, Transform cam, Transform groundCheck, LayerMask groundMask)
    {
        this.jumpheight = jumpheight;
        this.speed = speed;
        this.gravity = gravity * -1f;

        this.controller = controller;
        this.cam = cam;
        this.groundCheck = groundCheck;
        this.groundMask = groundMask;


        activespeed = speed * 1f;
    }

    void Update()
    {
        /*if (model.transform.position.y <= -50f) Death();
        else Movement();*/
    }

    public void Movement()
    {
        #region GroundCheck

        //grounded = Physics.CheckBox(groundCheck.position, new Vector3(model.transform.localScale.x - .1f, groundCheckSize, model.transform.localScale.z - .1f), Quaternion.Euler(0f, model.transform.rotation.y, 0f), groundMask);
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckSize, groundMask);
        if (grounded && movevert.y < 0)
        {
            movevert.y = -2f;
        }

        #endregion

        #region Actual Movement

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        //SPRINT
        //maybe use this in a state
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            activespeed = speed * 2f;
        }
        else
        {
            activespeed = speed * 1f;
        }
        //

        #endregion

        #region Jumping

        //probably should use this in a state
        bool prevgrounded = grounded;
        //grounded = Physics.CheckBox(groundCheck.position, new Vector3(model.transform.localScale.x - .1f, groundCheckSize + .5f, model.transform.localScale.z - .1f), Quaternion.Euler(0f, model.transform.rotation.y, 0f), groundMask);
        //getkeydown to check only once on down
        if (Input.GetButtonDown("Jump") && grounded)
        {
            movevert.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }
        grounded = prevgrounded;
        #endregion

        /*GRAVITY*/
        movevert.y += gravity * Time.deltaTime; 

        controller.Move((move.normalized * activespeed + movevert) * Time.deltaTime);
    }

    bool Menu()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                Cursor.lockState = CursorLockMode.None;
                return true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                return false;
            }
        }
        else return paused;
    }
}
