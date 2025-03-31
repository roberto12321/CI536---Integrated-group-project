using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    List<string> enemiesHit = new List<string>();
    private HealthScript healthScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageTaken)
    { 
        //SoundFXManager.instance.PlaySoundFXClip(playerHurtSound, transform, 1f);
        var newHealth = healthScript.health - damageTaken;
        healthScript.SetHealth(newHealth);
        print("Damage taken");
            

        
    }
}
