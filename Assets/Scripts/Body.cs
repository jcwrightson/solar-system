using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Body {

    public Transform Sphere;

    public double Mass;
    public double Radius;
    public double DistanceToParent;

    public Body(double mass, double radius, double distanceToParent, Transform sphere){
        Mass = mass;
        Radius = radius;
        DistanceToParent = distanceToParent;
        Sphere = sphere;
    }

	public void PositionAndScale(double SizeScale, double DistanceScale, double MassScale)
	{
		Rigidbody Rb = Sphere.GetComponent<Rigidbody>();
		
		double ScaledMass = Math.Round(Mass * MassScale, 4);
		double ScaledRadius = Math.Round((Radius * 2) * SizeScale, 4);
		double ScaledDistanceToParent = Math.Round(DistanceToParent * DistanceScale, 4);

		Rb.mass = (float)ScaledMass;
		Rb.transform.localScale = new Vector3((float)ScaledRadius, (float)ScaledRadius, (float)ScaledRadius);
		Rb.transform.localPosition = new Vector3(-(float)ScaledDistanceToParent, 0, 0);
	}

}
