using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditorInternal.VersionControl.ListControl;

public class EnemyScript : MonoBehaviour
{
    List<string> enemiesHit = new List<string>();
    private HealthScript healthScript;
    [SerializeField] private AudioClip enemyHurtSound;
    private EnemyState currentState;
    private EnemyState lastState;
    public GameObject player;
    public CharacterController controller;


    public float gravity = -9.81f;
    public float gravityMultiplier = 3.0f;
    private float velocity;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    public enum EnemyState
    {
        Idle,
        Attack,
        Chase,
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        if (currentState == EnemyState.Idle)
        {

        }
        if (currentState == EnemyState.Attack)
        {

        }
        if (currentState == EnemyState.Chase)
        {

        }
    }

    public void TakeDamage(float damageTaken)
    {
        //SoundFXManager.instance.PlaySoundFXClip(playerHurtSound, transform, 1f);
        var newHealth = healthScript.health - damageTaken;
        SoundFXManager.instance.PlaySoundFXClip(enemyHurtSound, transform, 1f);
        healthScript.SetHealth(newHealth);
        print("Damage taken");



    }
    private void StateChange(EnemyState targetState)
    {
        ExitedState(currentState);
        lastState = currentState;
        currentState = targetState;
        EnteredState(currentState);
    }

    private void EnteredState(EnemyState enteredState)
    {
        if (enteredState == EnemyState.Idle)
        {
        }
        if (enteredState == EnemyState.Attack)
        {
        }
        if (enteredState == EnemyState.Chase)
        {
        }

    }

    private void ExitedState(EnemyState exitedState)
    {
        if (exitedState == EnemyState.Idle)
        {
        }
        if (exitedState == EnemyState.Attack)
        {
        }
        if (exitedState == EnemyState.Chase)
        {
        }

    }
    private void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position);
        direction.y = 0f; // Only rotate on Y axis
        direction = direction.normalized;

        // Reset moveDirection
        Vector3 moveDirection = Vector3.zero;

        // Apply gravity
        moveDirection += new Vector3(0, ApplyGravity(), 0);

        if (direction.magnitude >= 0.1f) // Only rotate/move if player is not directly on top
        {
            // Calculate rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Rotate towards player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move towards player
            moveDirection += (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward) * speed * Time.deltaTime;
        }

        // Move enemy
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
}




