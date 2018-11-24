using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour {

    public List<Transform> Spheres;
    public List<Body> Bodies;

    public double G;
    public double MassScale;
    public double SizeScale;

    [Range(1f,100f)]
    public float TimeWarp;

    private void Awake()
    {

        MassScale = 0.0000000000000000000001;
        SizeScale = 0.0000001;
        TimeWarp = 10;

        G = 6.67384 * Math.Pow(10, -11) / SizeScale;

        Time.timeScale = Time.timeScale * TimeWarp;


        foreach (Transform sphere in transform)
        {
            Spheres.Add(sphere);
        }


		
		//Mass, Radius, Distance From Parent, Transform, 
		Bodies.Add(new Body(1.98847 * Math.Pow(10, 30), 696.342 * Math.Pow(10, 6), 0, Spheres[0])); // Sun
		Bodies.Add(new Body(3.285 * Math.Pow(10, 23), 2.439 * Math.Pow(10, 6), 57.91 * Math.Pow(10, 6) * 1000, Spheres[1])); // Mercury
		Bodies.Add(new Body(4.867 * Math.Pow(10, 24), 6.0518 * Math.Pow(10, 6), 108.2 * Math.Pow(10, 6) * 1000, Spheres[2])); // Venus
		Bodies.Add(new Body(5.972 * Math.Pow(10, 24), 6.371 * Math.Pow(10, 6), 149.6 * Math.Pow(10, 6) * 1000, Spheres[3])); // Earth

		foreach( Body body in Bodies) {
		    this.PositionAndScale(body);
		}


	}

    public void PositionAndScale(Body body)
    {
        Rigidbody Rb =  body.Sphere.GetComponent<Rigidbody>();
        double ScaledMass = body.Mass * MassScale;
        double ScaledRadius = (body.Radius * 2) * SizeScale;
        double ScaledDistanceToParent = body.DistanceToParent * SizeScale;

        Rb.mass = (float)ScaledMass;
        Rb.transform.localScale = new Vector3((float)ScaledRadius, (float)ScaledRadius, (float)ScaledRadius);
        Rb.transform.localPosition = new Vector3(-(float)ScaledDistanceToParent, 0, 0);
    }

    private void FixedUpdate () { 
       

        foreach (Body body in Bodies)
        {
           
            Vector3 CurrentPosition = body.Sphere.transform.position;
            float thisMass = body.Sphere.GetComponent<Rigidbody>().mass;

            foreach (Body bbody in Bodies)
            {
              if (body != bbody)
               {

                   Vector3 Difference = CurrentPosition - bbody.Sphere.transform.position;
                   float Distance = Difference.magnitude;
                   Vector3 GravityDirection = Difference.normalized;

                   float GravityForce = (float)G * (bbody.Sphere.GetComponent<Rigidbody>().mass * thisMass) / (Distance * Distance);
                   Vector3 GravityVector = (GravityDirection * GravityForce);

                   bbody.Sphere.GetComponent<Rigidbody>().AddForce(GravityVector, ForceMode.Force);
                }
           }
        }
	}
}
