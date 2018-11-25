using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public SolarSystem solarSystem;
    public Rigidbody rb;
	public LineRenderer Lr;

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

		//      Velocity = Math.Round(rb.velocity.magnitude  / Time.fixedUnscaledDeltaTime, 4);

		float Distance = Vector3.Distance(rb.transform.position, rb.transform.parent.transform.position);

		float thisRadius = rb.transform.localScale.x * 0.5f;
		float parentRadius = rb.transform.parent.transform.localScale.x * 0.5f;
		float asl = Distance - (thisRadius + parentRadius);

	    Altitude = Math.Round(asl, 4);


		//if (Altitude == 0)
		//{
		//	OrbitalVelocity = 0;
		//}
		//else
		//{
		//	OrbitalVelocity = Math.Round(Math.Sqrt(solarSystem.G * parent.mass / Distance), 4);
		//}


		//Debug.Log(rb.transform.parent.name);

		DrawAxis();
		
	}

	public double calcVelocity(double Mass, double Radius){
		return Math.Sqrt(solarSystem.G * Mass / Radius);
	}

	public double calcPeriod(double Radius, double Mass){
		return 2 * Math.PI * Math.Sqrt(Mathf.Pow((float)Radius, 3) / solarSystem.G * Mass);
	}


	private void DrawAxis()
	{
		
		

		Lr.SetPosition(0, rb.transform.position);
		Lr.SetPosition(1, rb.transform.parent.transform.position);

	

	}
}
