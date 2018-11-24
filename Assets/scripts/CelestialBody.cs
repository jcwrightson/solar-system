using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody {

    public Rigidbody Rb;
    public double ScaleFactor, MassScaleFactor;
    public double G;

    public double ScaledMass, ScaledRadius, ScaledDistanceToParent;

    public CelestialBody(double scaleFactor, double Gravity)
    {
        ScaleFactor = scaleFactor;
        G = Gravity; 
    }

    public CelestialBody (Rigidbody rb) {
        Rb = rb;
        MassScaleFactor = 0.0000000000000000000001;
        ScaleFactor = 0.00001;
        //G = 6.67384 * Math.Pow(10, -11);
    }

    public CelestialBody()
    {
        MassScaleFactor = 0.0000000000000000000001;
        ScaleFactor = 0.00001;
        //G = 6.67384 * Math.Pow(10, -11);
    }

    public void PositionAndScale(Double Mass, Double Radius, Double DistanceToParent)
    {
        ScaledMass = Mass * MassScaleFactor;
        ScaledRadius = (Radius * 2) * ScaleFactor;
        ScaledDistanceToParent = DistanceToParent * ScaleFactor;

        Rb.mass = (float)ScaledMass;
        Rb.transform.localScale = new Vector3((float)ScaledRadius, (float)ScaledRadius, (float)ScaledRadius);
        Rb.transform.localPosition = new Vector3(-(float)ScaledDistanceToParent, 0, 0);
    }

    public void AddVelocity(Double Speed)
    {
        double F = Rb.mass * (Speed * ScaleFactor);


        //Rb.AddForce(0, 0, (float)F, ForceMode.VelocityChange);

        Rb.transform.Translate(Vector3.forward * ((float)Speed * (float)ScaleFactor));
        
        Debug.Log(F);
    }


}
