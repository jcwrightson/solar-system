using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercury : MonoBehaviour
{

    public SolarSystem solarSystem;
    public Body body;
    public Rigidbody rb;

    void Start()
    {
        body = new Body(3.285 * Math.Pow(10, 23), 2.439 * Math.Pow(10, 6), 57.91 * Math.Pow(10, 6)* 1000);

        //body = new Body(0.33 * Math.Pow(10, 24), 2.439 * Math.Pow(10, 6), (5.791 * Math.Pow(10, 6) * 1000));
        solarSystem.PositionAndScale(body.Mass, body.Radius, body.DistanceToParent, rb);
    }

}
