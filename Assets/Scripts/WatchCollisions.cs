using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchCollisions : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        // Debug.Log(collider.gameObject.name);
        if(collider.gameObject.name == "Polar")
		{
           // Orbit.orbits+= 0.5f;
		}
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }
}
