using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdpersonmovement : MonoBehaviour
{
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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        activespeed = speed * 1f;
    }


    void Update()
    {
        if (model.transform.position.y <= -50f) Death();
        else  Movement();
    }

    void Movement()
    {
        if (!Menu())
        {
            grounded = Physics.CheckBox(groundCheck.position, new Vector3(model.transform.localScale.x - .1f, groundCheckSize, model.transform.localScale.z - .1f), Quaternion.Euler(0f, model.transform.rotation.y, 0f), groundMask);

            if (grounded && movevert.y < 0)
            {
                movevert.y = -2f;
            }


            float horiz = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horiz, 0f, vert).normalized;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                activespeed = speed * 2f;
            }
            else
            {
                activespeed = speed * 1f;
            }



            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 movedir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(movedir.normalized * activespeed * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump") && grounded)
            {
                movevert.y = Mathf.Sqrt(jumpheight * -2f * gravity);
            }

            movevert.y += gravity * Time.deltaTime;  //TODO
            controller.Move(movevert * Time.deltaTime); //TODO
        }
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
