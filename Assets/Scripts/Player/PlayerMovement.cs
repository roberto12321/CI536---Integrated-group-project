using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;


    public float gravity = -9.81f;
    public float gravityMultiplier = 3.0f;
    private float velocity;
    public float speed = 6f;

    private Vector3 moveDirection;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    public InputActionReference move;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveInput = move.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(moveInput.x , 0 , moveInput.y).normalized;

        moveDirection = new Vector3(0,0,0);
        moveDirection += new Vector3(0, ApplyGravity(),0);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection += (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * speed * Time.deltaTime;
            
        }
        print(moveDirection);
        controller.Move(moveDirection);
        
        
    }
    private float ApplyGravity()
    {
        if (controller.isGrounded)
        {
            velocity = -1f;
            print("Grounded: " + velocity);

        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
            print("Airborne: " + velocity);

        }
        return velocity;
        
    }
}
