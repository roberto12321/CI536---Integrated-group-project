using System.Collections.Generic;
using UnityEngine;

public class HitboxManagerScript : MonoBehaviour
{
    public List<Collider> entitiesHit = new List<Collider>();
    public GameObject entity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          
    }
    public void ClearEntitiesHitArray()
    {
        entitiesHit.Clear();
    }
}
