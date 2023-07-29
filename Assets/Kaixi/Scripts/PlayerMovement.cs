using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public Transform groundCheckPt;
    public float groundCheckRadius;
    public LayerMask groundCheckLayerMask;
    public float gravity = -9.81f;
    bool isRunning;
    bool isJumping;
    bool isGround;
    Animator animator;
    //PickUpStick pickUpStick;
    public Transform model;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Rigidbody rig;
    GameManagement gameManagement;
    private void Start()
    {
        //gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        animator = GetComponentInChildren<Animator>();
        //pickUpStick = GetComponent<PickUpStick>();
        rig = GetComponent<Rigidbody>();
        //walkSpeed = gameManagement.getPlayerSpeed();
    }

    //void ChangeAnimationWeight()
    //{
    //    if (pickUpStick.isHoldingStick)
    //    {
    //        animator.SetLayerWeight(1, 1);
    //    }
    //    else
    //    {
    //        animator.SetLayerWeight(1, 0);
    //    }

    //}

    //for debug use
    //public GameObject go;
    //private void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, -transform.up, out hit, 100))
    //    {
    //        go = hit.transform.gameObject;
    //    }

    //}


    float horizontalInput;
    float verticalInput;

    private void Update()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isJumping = Input.GetKeyDown(KeyCode.Space);
        //ChangeAnimationWeight();

        // Get input axes for horizontal and vertical movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //animation
        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("Run", isRunning);
            animator.SetBool("Walk", !isRunning);

            if (verticalInput < 0)
            {
                animator.SetBool("ReverseRun", isRunning);
                animator.SetBool("ReverseWalk", !isRunning);
            }
            else
            {
                animator.SetBool("ReverseRun", false);
                animator.SetBool("ReverseWalk", false);
            }
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }
    }

    private void FixedUpdate()
    {
        //jump
        isGround = Physics.CheckSphere(groundCheckPt.position, groundCheckRadius,groundCheckLayerMask);
        if (isGround && isJumping) Jump();

        if (!isGround)
        {
            Vector3 velocity = rig.velocity;
            velocity.y  += gravity * Time.deltaTime;
            Debug.Log(velocity.y);
            rig.velocity = velocity;
        }


        //move
        if (horizontalInput == 0 && verticalInput == 0 && isGround)
        {
            rig.velocity = Vector3.zero;
        }
        else
        {
            // Calculate movement direction based on input axes
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            //ector3 movementDirection = (transform.forward * horizontalInput + transform.right * verticalInput).normalized;

            // Normalize movement direction to prevent faster diagonal movement
            //if (movementDirection.magnitude > 1f)
            //{
            //    movementDirection.Normalize();
            //}

            //change animation facing
            if (movementDirection.magnitude >= 0.1f)
            {
                float facingAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, facingAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                //model.transform.rotation = Quaternion.LookRotation(movementDirection);

                //// Apply movement to transform (CANNOT BE USED IN THIS STATUE)
                //transform.position += movementDirection * speed * Time.deltaTime;
                Vector3 moveDir = Quaternion.Euler(0, facingAngle, 0) * Vector3.forward;
                //rig.velocity = movementDirection * speed * Time.deltaTime;
                rig.velocity = moveDir.normalized * walkSpeed * Time.deltaTime;
                //Debug.Log(rig.velocity);
            }
        }

    }

  

    void Jump()
    {
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //Vector3 vel = rig.velocity;
        //vel.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        //rig.velocity = vel;
        animator.SetTrigger("Jump");
        isGround = false;
    }

    private void OnDrawGizmos()
    {
        //ground check visualize
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPt.position, groundCheckRadius);
    }
}
