using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public SolarSystem solarSystem;
    public Rigidbody rb;
    public Rigidbody parent;

    public double OrbitalVelocity;
    public double Velocity;
    public double Period;
    public double Apoapsis;
    public double Periapsis;
    public double SemiMajorAxis;
    public double AltitudeAboveSeaLevel;

    private void FixedUpdate()
    {
       
        Velocity = (rb.velocity.magnitude / solarSystem.SizeScale) / Time.fixedDeltaTime;

        Vector3 Difference = parent.transform.position - rb.transform.position;
        float Distance = Difference.magnitude;

        float thisRadius = rb.transform.localScale.x * 0.5f;
        float parentRadius = parent.transform.localScale.x * 0.5f;
        float asl = Distance - (thisRadius + parentRadius);

        AltitudeAboveSeaLevel = asl / solarSystem.SizeScale;


        OrbitalVelocity = (Math.Sqrt(solarSystem.G * parent.mass / Distance) / solarSystem.SizeScale);

    
    }
}
