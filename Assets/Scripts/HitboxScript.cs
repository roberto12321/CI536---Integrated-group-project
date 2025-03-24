using TMPro;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    public HitboxManagerScript hitboxManagerScript;
    public string enemyTag;
    public HealthScript healthScript;
    public float hitboxDamage;
    public HitboxType hitboxType;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag) && !hitboxManagerScript.entitiesHit.Contains(other))
        {
            hitboxManagerScript.entitiesHit.Add(other);
            if(enemyTag == "PlayerHurtbox")
            {
                print("Player hit");
                PlayerStates playerScript = other.gameObject.GetComponent<PlayerStates>();
                GameObject playerObject = playerScript.player;

                Vector3 blockDirection = playerObject.transform.forward; 
                Vector3 direction = (hitboxManagerScript.entity.transform.position - playerObject.transform.position).normalized;
                float dotProduct = Vector3.Dot(direction, blockDirection);
                bool facingHitbox;
                if (dotProduct > 0)
                {
                    facingHitbox = true;
                }
                else
                {
                    facingHitbox = false;
                }


                // Calculate the dot product between the direction and the block direction


                playerScript.TakeDamage(hitboxDamage, hitboxType, facingHitbox);

            }
            else
            {
                print("Enemy hit");
            }
            //healthScript = other.GetComponent<HealthScript>();
            //var newHealth = healthScript.health - hitboxDamage;
            //healthScript.SetHealth(newHealth);
            //print(healthScript.health);
        }
        
    }
}

