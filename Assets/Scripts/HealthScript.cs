using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HealthScript : MonoBehaviour
{

    [HideInInspector]public float health;
    [HideInInspector] public bool isAlive = true;
    [HideInInspector] public float greyHealth;
    public float maxHealth;
    public Slider healthBar;
    public Slider greyHealthBar;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        if(greyHealthBar != null)
        {
            greyHealthBar.value = maxHealth;
            greyHealthBar.maxValue = maxHealth;
        }
        else
        {
            print("No greyhealth slider attatched to object");
        }
        
    }

    public void SetHealth(float newHealth)
    {
        
        health = Mathf.Clamp(newHealth, 0, maxHealth);
        
        healthBar.value = health;
    }
    public void SetGreyHealth(float newGreyHealth)
    {
        greyHealth = Mathf.Clamp(newGreyHealth, 0, maxHealth - health);
        UpdateGreyHealthUI();
    }

    public void UpdateGreyHealthUI()
    {
        greyHealthBar.value = health + greyHealth;
    }
   
}
