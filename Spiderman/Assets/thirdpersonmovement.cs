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
    public float jumpheight = 3f;

    public float speed = 6f;
    float activespeed;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float gravity = -9.81f;
    bool grounded;
    public Transform groundCheck;
    public float groundCheckSize = .1f;
    public LayerMask groundMask;

    public Transform spawn;

    bool paused = false;
    #endregion

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        activespeed = speed * 1f;
    }


    void Update()
    {
        if (model.transform.position.y <= -50f) Death();
        else Movement();
    }

    void Movement()
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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            activespeed = speed * 2f;
        }
        else
        {
            activespeed = speed * 1f;
        }
        //

        /*if(move.magnitude != 0f)
            controller.Move(move.normalized * activespeed * Time.deltaTime);*/
        #endregion
        #region Jumping

        if (Input.GetButtonDown("Jump") && grounded)
        {
            movevert.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

            /*GRAVITY*/
        movevert.y += gravity * Time.deltaTime;  //BUG1
        controller.Move((move.normalized * activespeed + movevert) * Time.deltaTime); //BUG1

        #endregion
    }

    void Death()
    {
        movevert.y = -1f;
        transform.position = spawn.position;
        //transform.position = new Vector3(0f, 1f, 0f) ALSO WORKS, I want spawn in map so that i can send other stuff there easily
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
