using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStates : MonoBehaviour
{
    [Header("Components")]
   
    private Animator animator;
    private PlayerMovement playerMovement;
    private HealthScript healthScript;
    private HitboxManagerScript hitboxManagerScript;

    [Header("Components")]

    public GameObject player;
    public Transform cam;

    [Header("Audio")]

    [SerializeField] private AudioClip perfectBlockSound;
    [SerializeField] private AudioClip blockSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip dashSuccessSound;
    [SerializeField] private AudioClip playerHurtSound;

    [Header("Music")]

    public int initialState;

    [HideInInspector] public PlayerState currentState;
    [HideInInspector] public PlayerState lastState;
    
    //Combat
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public bool isPerfectBlocking;
    [HideInInspector] public bool isDashing;

    [Header("Combat Stats")]

    public float perfectBlockTime;
    public float greyHealthHealingRate;

    private float blockStartTime;


    [Header("Actions")]

    public InputActionReference attack;
    public InputActionReference block;
    public InputActionReference dash;
    public InputActionReference swap;
    public InputActionReference heal;
    public InputActionReference ability1;
    public InputActionReference ability2;

    public enum PlayerState
    {
        Idle,
        Attack,
        Block,
        Dash,
        Swap,
        Heal,
        Ability1,
        Ability2
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        healthScript = GetComponent<HealthScript>();
        hitboxManagerScript = GetComponent<HitboxManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerState.Idle)
        {
            if(playerMovement.isMoving)
            {
                animator.SetInteger("Animation", 4);
            }
            else
            {
                animator.SetInteger("Animation", 0);
            }

            if (attack.action.WasPressedThisFrame() && currentState != PlayerState.Attack)
            {
                StateChange(PlayerState.Attack);
          
            }
            if (block.action.IsPressed() && currentState != PlayerState.Block)
            {
                StateChange(PlayerState.Block);

            }
            if(dash.action.IsPressed() && playerMovement.direction.magnitude >= 0.1f && currentState != PlayerState.Dash)
            {
                StateChange(PlayerState.Dash);
            }
        }
        if (currentState == PlayerState.Attack)
        {
            
        }
        if (currentState == PlayerState.Block)
        {
            if(Time.time > blockStartTime + perfectBlockTime)
            {
                isPerfectBlocking = false;
            }


            if (!block.action.IsPressed())
            {
                StateChange(PlayerState.Idle);

            }
        }
        if (currentState == PlayerState.Dash)
        {
           
        }
        if (currentState == PlayerState.Swap)
        {

        }
        if (currentState == PlayerState.Heal)
        {

        }
        if (currentState == PlayerState.Ability1)
        {

        }
        if (currentState == PlayerState.Ability2)
        {

        }

    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.Idle)
        {

        }
        if (currentState == PlayerState.Attack)
        {

        }
        if (currentState == PlayerState.Block)
        {

        }
        if (currentState == PlayerState.Dash)
        {

        }
        if (currentState == PlayerState.Swap)
        {

        }
        if (currentState == PlayerState.Heal)
        {

        }
        if (currentState == PlayerState.Ability1)
        {

        }
        if (currentState == PlayerState.Ability2)
        {

        }
    }

    private void StateChange(PlayerState targetState)
    {
        ExitedState(currentState);
        lastState = currentState;
        currentState = targetState;
        EnteredState(currentState);

       
    }

    private void EnteredState(PlayerState enteredState)
    {
        if (enteredState == PlayerState.Idle)
        {
            print("Entered Idle");
            animator.SetInteger("Animation", 0);
        }
        if (enteredState == PlayerState.Attack)
        {
            print("Entered Attack");
            playerMovement.canMove = false;
            transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
            hitboxManagerScript.entitiesHit.Clear();
            animator.SetInteger("Animation", 1);
        }
        if (enteredState == PlayerState.Block)
        {
            print("Entered Block");
            playerMovement.canMove = false;
            isPerfectBlocking = true;
            isBlocking = true;
            blockStartTime = Time.time;
            transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
            animator.SetInteger("Animation", 2);
        }
        if (enteredState == PlayerState.Dash)
        {
            print("Entered Dash");
            playerMovement.canMove = false;
            playerMovement.isDashing = true;
            isDashing = true;
            animator.SetInteger("Animation", 3);
            SoundFXManager.instance.PlaySoundFXClip(dashSound, transform, 1f);
            playerMovement.SetDashDirection();

        }
        if (enteredState == PlayerState.Swap)
        {
            print("Entered Swap");
        }
        if (enteredState == PlayerState.Heal)
        {
            print("Entered Heal");
        }
        if (enteredState == PlayerState.Ability1)
        {
            print("Entered Ability1");
        }
        if (enteredState == PlayerState.Ability2)
        {
            print("Entered Ability2");
        }
    }

    private void ExitedState(PlayerState exitedState)
    {
        if(exitedState == PlayerState.Idle)
        {
            print("Exited Idle");
        }
        if (exitedState == PlayerState.Attack)
        {
            print("Exited Attack");
            playerMovement.canMove = true;
        }
        if (exitedState == PlayerState.Block)
        {
            print("Exited Block");
            isBlocking = false;
            isPerfectBlocking = false;
            playerMovement.canMove = true;
        }
        if (exitedState == PlayerState.Dash)
        {
            print("Exited Dash");
            playerMovement.canMove = true;
            playerMovement.isDashing = false;
            isDashing = false;
        }
        if (exitedState == PlayerState.Swap)
        {
            print("Exited Swap");
        }
        if (exitedState == PlayerState.Heal)
        {
            print("Exited Heal");
        }
        if (exitedState == PlayerState.Ability1)
        {
            print("Exited Ability1");
        }
        if (exitedState == PlayerState.Ability2)
        {
            print("Exited Ability2");
        }
    }

    public void AnimationEnd()
    {

        if (currentState == PlayerState.Attack)
        {
            StateChange(PlayerState.Idle);
        }
        if (currentState == PlayerState.Dash)
        {
            StateChange(PlayerState.Idle);
        }
    }

    public void TakeDamage(float damageTaken, HitboxType hitboxType, bool facingHitbox)
    {

        if (hitboxType == HitboxType.standard)
        {
            if (isPerfectBlocking && facingHitbox)
            {
                SuccessfulPerfectBlock(damageTaken);
            }
            else if (isBlocking && facingHitbox)
            {
                SuccessfulBlock(damageTaken);
            }
            else if(isDashing)
            {
                print("EZDODGELOL");
                SoundFXManager.instance.PlaySoundFXClip(dashSuccessSound, transform, 1f);
            }
            else
            {
                SoundFXManager.instance.PlaySoundFXClip(playerHurtSound, transform, 1f);
                var newHealth = healthScript.health - damageTaken;
                healthScript.SetHealth(newHealth);
                healthScript.UpdateGreyHealthUI();
                print("Damage taken");
            }

        }
    }
    public void SuccessfulPerfectBlock(float damageTaken)
    {
        print("Perfect blocked");
        
        SoundFXManager.instance.PlaySoundFXClip(perfectBlockSound, transform, 1f);
    }
    public void SuccessfulBlock(float damageTaken)
    {
        print("Blocked");
        
        SoundFXManager.instance.PlaySoundFXClip(blockSound, transform, 1f);

        float newGreyHealth = healthScript.greyHealth + (damageTaken * 0.5f);
        float newHealth = healthScript.health - (damageTaken * 0.5f);
        healthScript.SetHealth(newHealth);
        healthScript.SetGreyHealth(newGreyHealth);
    }
}
