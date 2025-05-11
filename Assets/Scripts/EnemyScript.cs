using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    private Animator animator;
    private HitboxManagerScript hitboxManagerScript;

    public string enemyTag;
    public float gravity = -9.81f;
    public float gravityMultiplier = 3.0f;
    private float velocity;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float rotationSpeed;

    float turnSmoothVelocity;
    public float chaseDistance;
    public float attackDistance;
    bool attacking = false;
    private bool facingPlayer;
    public bool pseudoBoss;
    public enum EnemyState
    {
        Idle,
        Attack,
        Chase,
        Rotating,
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        hitboxManagerScript = GetComponent<HitboxManagerScript>();
        healthScript = GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (currentState == EnemyState.Idle)
        {
            if (distance < chaseDistance)
            {
                StateChange(EnemyState.Chase);
            }

        }
        if (currentState == EnemyState.Attack)
        {
            if (distance > attackDistance && !attacking)
            {
                StateChange(EnemyState.Chase);
            }
            if(!facingPlayer && distance < attackDistance && !attacking)
            {
                StateChange(EnemyState.Rotating);
            }
        }
        if (currentState == EnemyState.Rotating)
        {
            if (distance > attackDistance && !attacking)
            {
                StateChange(EnemyState.Chase);
            }
            if (facingPlayer && distance < attackDistance)
            {
                StateChange(EnemyState.Attack);
            }
            RotateTowardsPlayer();
            
        }
        if (currentState == EnemyState.Chase)
        {
            if(distance > chaseDistance)
            {
                StateChange(EnemyState.Idle);
            }
            if(distance < attackDistance)
            {
                StateChange(EnemyState.Attack);
            }
            ChasePlayer();
        }
        //Have this so gravity always affects the enemy
        Gravity();

        //Health
        if (healthScript.health <= 0)
        {
            Destroy(gameObject);
            if (pseudoBoss)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
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
            animator.SetInteger("Animation", 0);
        }
        if (enteredState == EnemyState.Rotating)
        {
            animator.SetInteger("Animation", 0);
        }
        if (enteredState == EnemyState.Attack)
        {
            animator.SetInteger("Animation", 2);
            attacking = true;
        }
        if (enteredState == EnemyState.Chase)
        {
           
            animator.SetInteger("Animation", 1);
        }

    }

    private void ExitedState(EnemyState exitedState)
    {
        if (exitedState == EnemyState.Idle)
        {
        }
        if (exitedState == EnemyState.Attack)
        {
            print("Hitboxes disabled");
            hitboxManagerScript.DisableAllHitboxes();
        }
        if (exitedState == EnemyState.Chase)
        {
        }

    }
    private void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position);
        direction.y = 0f; 
        direction = direction.normalized;

        // Reset moveDirection
        Vector3 moveDirection = Vector3.zero;

       

        if (direction.magnitude >= 0.1f) 
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

           
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection += (Quaternion.Euler(0f, angle, 0f) * Vector3.forward) * speed * Time.deltaTime;
        }

        // Move enemy
        controller.Move(moveDirection);
    }
    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position);
        direction.y = 0f;
        direction = direction.normalized;

        // Reset moveDirection
        Vector3 moveDirection = Vector3.zero;

    
        
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);


        transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
        

     
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
    private void Gravity()
    {
        // Reset moveDirection
        Vector3 moveDirection = Vector3.zero;

        // Apply gravity
        moveDirection += new Vector3(0, ApplyGravity(), 0);
        controller.Move(moveDirection);
    }
    private void AttackEnd()
    {
        attacking = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            facingPlayer = true;
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            facingPlayer = false;

        }
    }
}




