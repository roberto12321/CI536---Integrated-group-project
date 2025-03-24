using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStates : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private HealthScript healthScript;
    private HitboxManagerScript hitboxManagerScript;
    public GameObject player;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int initialState;
    [HideInInspector] public PlayerState currentState;
    [HideInInspector] public PlayerState lastState;

    //Combat
    public bool isBlocking;
    public bool isPerfectBlocking;



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
            if (attack.action.WasPressedThisFrame() && currentState != PlayerState.Attack)
            {
                StateChange(PlayerState.Attack);
          
            }
            if (block.action.IsPressed() && currentState != PlayerState.Block)
            {
                StateChange(PlayerState.Block);

            }
        }
        if (currentState == PlayerState.Attack)
        {
            
        }
        if (currentState == PlayerState.Block)
        {
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
            hitboxManagerScript.entitiesHit.Clear();
            animator.SetInteger("Animation", 1);
        }
        if (enteredState == PlayerState.Block)
        {
            print("Entered Block");
            playerMovement.canMove = false;
            isBlocking = true;
            animator.SetInteger("Animation", 2);
        }
        if (enteredState == PlayerState.Dash)
        {
            print("Entered Dash");
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
            playerMovement.canMove = true;
        }
        if (exitedState == PlayerState.Dash)
        {
            print("Exited Dash");
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
    }

    public void TakeDamage(float damageTaken, HitboxType hitboxType, bool facingHitbox)
    {

        if(hitboxType == HitboxType.standard)
        {
            if(isBlocking && facingHitbox)
            {
                print("Blocked");
            }
            else
            {
                var newHealth = healthScript.health - damageTaken;
                healthScript.SetHealth(newHealth);
                print("Damage taken");
            }
            
        }

        
    }
    


}
