using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public SolarSystem solarSystem;
    public Rigidbody rb;
	private LineRenderer Lr;

	public double OrbitalVelocity;
	public double RelativeVelocity;
	public double Velocity;
	public double Period;
	public double Apoapsis;
	public double Periapsis;
	public double SemiMajorAxis;
	public double Altitude;

	private void Start()
	{
		Lr = rb.transform.GetComponent<LineRenderer>();
		
	}

	private void FixedUpdate()
	{

		Velocity = Math.Round(rb.velocity.magnitude  / Time.fixedUnscaledDeltaTime, 4);

		float Distance = Vector3.Distance(rb.transform.position, rb.transform.parent.transform.position);

		float thisRadius = rb.transform.localScale.x * 0.5f;
		float parentRadius = rb.transform.parent.transform.localScale.x * 0.5f;
		float asl = Distance - (thisRadius + parentRadius);

	    Altitude = Math.Round(asl, 4);

		//DrawAxis();
		
	}

	public double calcVelocity(double Mass, double Radius){
		return Math.Sqrt(solarSystem.Gravity.G * Mass / Radius);
	}

	public double calcPeriod(double Radius, double Mass){
		return 2 * Math.PI * Math.Sqrt(Mathf.Pow((float)Radius, 3) / solarSystem.Gravity.G * Mass);
	}


	private void DrawAxis()
	{
		
		

		Lr.SetPosition(0, rb.transform.position);
		Lr.SetPosition(1, rb.transform.parent.transform.position);

	

	}
}
