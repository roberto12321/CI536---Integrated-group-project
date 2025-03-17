using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStates : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public InputActionReference attack;
    public InputActionReference block;
    public InputActionReference dash;
    public InputActionReference swap;
    public InputActionReference heal;
    public InputActionReference ability1;
    public InputActionReference ability2;



    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.action.IsPressed()) // IsPressed() checks if the button is being held down
        {
            animator.SetInteger("Animation", 1); // Set animation to attack (1)
        }
        else
        {
            animator.SetInteger("Animation", 0); // Reset animation to idle (0) when not pressed
        }
    }
}
