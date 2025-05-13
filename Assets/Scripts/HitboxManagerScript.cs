using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HitboxManagerScript : MonoBehaviour
{
    public List<Collider> entitiesHit = new List<Collider>();
    [SerializeField] private BoxCollider hitboxes;

    public GameObject entity;
    private HealthScript healthScript;
    public PlayerStates playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
          
    }
    public void ClearEntitiesHitArray()
    {
        entitiesHit.Clear();
    }
    public void EnemyHit(float damage)
    {
        GreyHealthHeal(damage);
    }
    private void GreyHealthHeal(float damage)
    {
        if (healthScript.greyHealth > 0)
        {
            float greyHealthHealing = Mathf.Clamp(damage * (playerScript.greyHealthHealingRate / 100), 0, healthScript.greyHealth);

            float newGreyHealth = healthScript.greyHealth - greyHealthHealing;
            float newHealth = healthScript.health + greyHealthHealing;
            print("Newhealth: " + newHealth + " Greyhealthhealing: " + greyHealthHealing);
            healthScript.SetHealth(newHealth);
            healthScript.SetGreyHealth(newGreyHealth);
        }
    }
   
    public void DisableAllHitboxes()
    {

        //hitboxes.enabled = false;
        print("Obselete");
        
    }


}
