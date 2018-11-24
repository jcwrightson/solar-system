using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venus : MonoBehaviour
{

    public SolarSystem SolarSystem;
    public Rigidbody rb;
    public Body body;

    public double Mass;
    public double Radius;
    public double Parent;
    public double DistanceToParent;
    public double Velocity;

    void Start()
    {

        body = new Body(4.867 * Math.Pow(10, 24), 6.0518 * Math.Pow(10, 6), 108.2 * Math.Pow(10, 6) * 1000);

        SolarSystem.PositionAndScale(body.Mass, body.Radius, body.DistanceToParent, rb);
    }
}
