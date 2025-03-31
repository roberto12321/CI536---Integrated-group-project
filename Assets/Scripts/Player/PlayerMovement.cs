using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public bool canMove;

    public float gravity = -9.81f;
    public float gravityMultiplier = 3.0f;
    private float velocity;
    public float speed = 6f;

    [HideInInspector]public Vector3 direction;
    private Vector3 moveDirection;

    private float dashAngle;
    private Vector3 dashDirection;
    [HideInInspector] public bool isDashing;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    public InputActionReference move;

    void Start()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveInput = move.action.ReadValue<Vector2>();
        direction = new Vector3(moveInput.x , 0 , moveInput.y).normalized;


        //Resets direction
        moveDirection = new Vector3(0,0,0);
        
        //Applies gravity
        moveDirection += new Vector3(0, ApplyGravity(),0);

        //Applies normal movement
        if (isDashing)
        { 
            moveDirection += (Quaternion.Euler(0f, dashAngle, 0f) * Vector3.forward).normalized * speed * 1.6f * Time.deltaTime;

        }

        //Applies normal movement
        if (direction.magnitude >= 0.1f && canMove)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection += (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * speed * Time.deltaTime;
            
        }


        //Using the controller to move
        controller.Move(moveDirection);
        
        
    }
    private float ApplyGravity()
    {
        if (controller.isGrounded)
        {
            velocity = -1f;
            //print("Grounded: " + velocity);

        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
            //print("Airborne: " + velocity);

        }
        return velocity;
        
    }
    public void SetDashDirection()
    {
        Vector2 moveInput = move.action.ReadValue<Vector2>();
        dashDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        dashAngle = Mathf.Atan2(dashDirection.x, dashDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, dashAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, dashAngle, 0f);
    }
}
