using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    public SolarSystem SolarSystem;
    public Rigidbody rb;
    public Body body;

    public double Mass;
    public double Radius;
    public double Parent;
    public double DistanceToParent;
    public double Velocity;
  
    void Start () {

        body = new Body(1.98847 * Math.Pow(10, 30), 696.342 * Math.Pow(10, 6), 0);

        SolarSystem.PositionAndScale(body.Mass, body.Radius, body.DistanceToParent, rb);
    }
}
