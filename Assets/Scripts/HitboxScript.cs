using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    public HitboxManagerScript hitboxManagerScript;
    public string enemyTag;
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
            print("Balls");
        }
        
    }
}

