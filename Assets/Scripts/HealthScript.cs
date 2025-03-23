using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{

    [HideInInspector]public float health;
    [HideInInspector] public bool isAlive = true;
    public float maxHealth;
    public Slider healthBar;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health; 
    }

    public void SetHealth(float newHealth)
    {
        if(newHealth < maxHealth)
        {
            health = maxHealth;
        }
        else if(newHealth > maxHealth) 
        {
            health = 0;
        }
        else
        {
            health = newHealth;
        }
        healthBar.value = health;
    }
   
}
