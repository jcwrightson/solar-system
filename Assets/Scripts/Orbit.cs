using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public SolarSystem solarSystem;
    public Rigidbody rb;
	public Transform Focus;
	private LineRenderer Lr;

	public double OrbitalVelocity;
	public double RelativeVelocity;
	public double Velocity;
	public double Period;
	public double Apoapsis;
	public double Periapsis;
	public double SemiMajorAxis;
	public double Altitude;

	public float thisRadius;
	public float focusRadius;

	private void Start()
	{
		Lr = rb.transform.GetComponent<LineRenderer>();

		Focus = FindOrbitalParent(solarSystem.Bodies, solarSystem.GetBodyFromTransform(transform)).Sphere;
		
	}

	private void Awake()
	{
		
	}

	private void FixedUpdate()
	{

		Velocity = Math.Round((rb.velocity.magnitude / solarSystem.DistanceScale)  / Time.fixedUnscaledDeltaTime, 4);

		//float Distance = Vector3.Distance(rb.transform.position, rb.transform.parent.position);

		thisRadius = transform.localScale.x / 2;
		focusRadius = Focus.transform.localScale.x / 2;
		//float asl = Distance - (thisRadius + parentRadius);

		//   Altitude = Math.Round(asl, 4);

		//DrawAxis();

		Altitude = Math.Floor((Vector3.Distance(transform.position, Focus.position) - (thisRadius + focusRadius)) / solarSystem.DistanceScale);
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

	public Body FindOrbitalParent(List<Body> Bodies, Body Child)
	{
		if (Child.Focus == null)
		{
			List<Body> tmp;
			tmp = new List<Body>();

			foreach (Body body in Bodies)
			{
				if (body != Child)
				{
					tmp.Add(body);
				}

			}

			tmp.Sort(CompareMasses);
			return tmp[tmp.Count - 1];
		}

		return Child.Focus;
	}

	static int CompareMasses(Body b1, Body b2)
	{
		return b1.Mass.CompareTo(b2.Mass);
	}
}
