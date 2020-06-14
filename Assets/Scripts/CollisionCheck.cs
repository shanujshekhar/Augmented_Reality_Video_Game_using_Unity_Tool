using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public bool collided = false;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.name.EndsWith("Collider"))
        {
            collided = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name.EndsWith("Collider"))
        {
            collided = false;
        }
    }
}
