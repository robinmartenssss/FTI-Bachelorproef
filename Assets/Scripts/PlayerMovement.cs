using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header ("Keybinds")]
    public string controllerJumpButton = "buttonSouth";

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    public Transform orientation;
    // public Transform StepStairs;

    float horizontalInput;
    float verticalInput;
    float VerticalOutOfBound;
    float HorizontalOutOfBounds;

    [Header("Step Check")]

    public GameObject StepRayLower; 
    public GameObject StepRayUpper; 
    public float stepHeight = 0.3f;
    public float stepSmooth = 0.1f;

    Vector3 dir;

    Vector3 moveDirection;
    Rigidbody rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        StepRayUpper.transform.position  = new Vector3(StepRayUpper.transform.position.x, stepHeight ,StepRayUpper.transform.position.x);

    }

    // Update is called once per frame
    
    private void Update()
    {

        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        //handle the drag
        if(grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        stepClimber();
    }



   private void MyInput()
{
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");
}

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // player ison ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // player in the air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void OnButtonRegular()
    {
        if(readyToJump){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

         Debug.Log("hello jumped");
        readyToJump = false;

        Invoke(nameof(ResetJump), jumpCooldown);
        } else if (!readyToJump){
            Debug.Log("you can't jump now");
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "SceneTransitionBasket") {
            GameBehaviour.Instance.sceneToMoveTo();
        }
         if(collision.gameObject.tag == "SceneTransitionGame2") {
            GameBehaviour.Instance.sceneToMoveToGame2();
        }
         if(collision.gameObject.tag == "SceneTransitionGame3") {
            GameBehaviour.Instance.sceneToMoveToGame3();
        }
         if(collision.gameObject.tag == "SceneTransitionGame4") {
            GameBehaviour.Instance.sceneToMoveToGame4();
        }
    }

   

    // public void stepClimber()
    // {
    //     RaycastHit hitLower;
    //     if(Physics.Raycast(StepStairs.transform.position, transform.TransformDirection(-Vector3.up), out hitLower, 2f, ~ignoreMe))
    //         {
    //             if(grounded && (StepStairs.transform.position.y - hitLower.point.y <= stepHeight))
    //             {
    //                 Vector3 targetVector = new Vector3(rb.position.x, hitLower.point.y, rb.position.z);
    //                 rb.position = Vector3.Lerp(rb.position, targetVector, Time.deltaTime / 0.1f);
    //                 rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    //             }
    //             if(!grounded && (StepStairs.transform.position.y - hitLower.point.y <= stepHeight))
    //             {
    //                 rb.position = new Vector3(rb.position.x, hitLower.point.y, rb.position.z);
    //                 rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    //             }
    //         }
    // }

    public void stepClimber()
        {
            RaycastHit hitLower;
            if(Physics.Raycast(StepRayLower.transform.position, transform.TransformDirection(Vector3.down), out hitLower, 0.1f))
            {
                Debug.Log("nummer1");
                Debug.DrawRay(StepRayLower.transform.position, transform.TransformDirection(Vector3.down) * 0.1f, Color.red, 2.0f);
                RaycastHit hitUpper;
                if(!Physics.Raycast(StepRayUpper.transform.position, transform.TransformDirection(Vector3.down), out hitUpper, 0.2f))
                {
                    Debug.Log("nummer2");
                    Debug.DrawRay(StepRayLower.transform.position, transform.TransformDirection(Vector3.down) * 0.1f, Color.green, 2.0f);
                    rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f); 
                }
            }



            // RaycastHit hitLower45;
            // if(Physics.Raycast(StepRayLower.transform.position, transform.TransformDirection(1.5f, 0.1), out hitLower45, 0.1f))
            // {
            //     RaycastHit hitUpper45;
            //     if(!Physics.Raycast(StepRayUpper.transform.position, transform.TransformDirection(1.5f, 0.1), out hitUpper45, 0.2f))
            //     {
            //         rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f); 
            //     }
            // }

            // RaycastHit hitLowerMinus45;
            // if(Physics.Raycast(StepRayLower.transform.position, transform.TransformDirection(-1.5f, 0.1), out hitLowerMinus45, 0.1f))
            // {
            //     RaycastHit hitUpperMinus45;
            //     if(!Physics.Raycast(StepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0.1), out hitUpperMinus45, 0.2f))
            //     {
            //         rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f); 
            //     }
            // }
        }
    
}
