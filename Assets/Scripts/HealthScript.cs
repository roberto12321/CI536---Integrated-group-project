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
        greyHealthBar.value = maxHealth;
        greyHealthBar.maxValue = maxHealth;
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
   
}
