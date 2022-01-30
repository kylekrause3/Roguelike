using UnityEngine;

public class thirdpersonmovement : MonoBehaviour
{
    #region vars
    Player player;

    float activespeed,  gravity;

    CharacterController controller;
    Transform cam;

    Transform groundCheck;
    LayerMask groundMask;


    public GameObject model;

    Vector3 movevert;

    bool grounded;
    public float groundCheckSize = .5f;
    bool paused = false;

    #endregion


    public thirdpersonmovement(Player player, float speed, float gravity, CharacterController controller, Transform cam, Transform groundCheck, LayerMask groundMask)
    {
        /*this.jumpheight = jumpheight;
        this.speed = speed;*/
        this.gravity = gravity;

        this.controller = controller;
        this.cam = cam;
        this.groundCheck = groundCheck;
        this.groundMask = groundMask;

        this.player = player;

        movevert.y = -2f;
        activespeed = speed * 1f;
    }

    public void Movement(float speed, float jumpheight)
    {

        grounded = Physics.CheckSphere(groundCheck.position, groundCheckSize, groundMask);
        if (grounded && movevert.y < 0)
        {
            movevert.y = -2f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = player.transform.right * x + player.transform.forward * z;


        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            activespeed = speed * 2f;
        }
        else
        {
            activespeed = speed * 1f;
        }
        

        if (Input.GetButtonDown("Jump") && grounded)
        {
            movevert.y = Mathf.Sqrt(jumpheight * gravity);
        }
        movevert.y -= gravity * Time.deltaTime;


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
