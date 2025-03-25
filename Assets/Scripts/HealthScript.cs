using UnityEngine;
using UnityEngine.UI;

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
        if(newHealth > maxHealth)
        {
            health = maxHealth;
        }
        else if(newHealth < 0) 
        {
            health = 0;
        }
        else
        {
            health = newHealth;
        }
        healthBar.value = health;
    }
    public void SetGreyHealth(float newGreyHealth)
    {
        if(newGreyHealth > maxHealth - health)
        {
            greyHealth = maxHealth - health;
        }
        else if(newGreyHealth < 0)
        {
            greyHealth = 0;
        }
        else
        {
            greyHealth = newGreyHealth;
        }
        UpdateGreyHealthUI();
    }

    public void UpdateGreyHealthUI()
    {
        greyHealthBar.value = health + greyHealth;
    }
   
}
