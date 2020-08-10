using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

	public SolarSystem solarSystem;
	public Rigidbody rb;
	public Transform Focus;
	private LineRenderer Lr;

	public double OrbitalVelocity;
	public double RelativeVelocity;
	public double OrbitalSpeed;
	public double Velocity;
	public double Period;
	public double Apoapsis;
	public double Periapsis;
	public double MajorAxis;
	public double ASL;

	public int Orbits = 0;

	public float thisRadius;
	public float focusRadius;

	private double CurrentAlt;
	private double Altitude;

	private Vector3 positionA;
	private Vector3 positionB;

	private int I;

	public Vector3 ap;
	public Vector3 pe;


	private void Start()
	{
		Lr = rb.transform.GetComponent<LineRenderer>();

		if (Lr)
		{
			Lr.positionCount = 360;
		}

		Focus = FindOrbitalParent(solarSystem.Bodies, solarSystem.GetBodyFromTransform(transform)).Sphere;
		rb.AddRelativeForce(new Vector3(0, 0, solarSystem.GetBodyFromTransform(transform).Shunt * rb.mass / Vector3.Distance(transform.position, Focus.position)));

	}

	private void Awake()
	{


	}

	private void FixedUpdate()
	{

		thisRadius = transform.localScale.x / 2;
		focusRadius = Focus.transform.localScale.x / 2;

		CurrentAlt = Math.Round((Vector3.Distance(transform.position, Focus.position) / solarSystem.DistanceScale) / 1000, 4);

		ASL = Math.Round((Vector3.Distance(transform.position, Focus.position) - (thisRadius + focusRadius)) / solarSystem.DistanceScale, 4);

		Velocity = Math.Round((rb.velocity.magnitude / solarSystem.DistanceScale) / Time.fixedUnscaledDeltaTime, 4); // M/s

		OrbitalSpeed = Math.Round(calcSpeed(rb.mass / solarSystem.MassScale, CurrentAlt), 4); // Km/s

		
		if (Altitude > 0)
		{
			if (CurrentAlt > Altitude && CurrentAlt > Apoapsis)
			{
				Apoapsis = CurrentAlt;
				ap = transform.position;
			}

			if (CurrentAlt < Altitude && CurrentAlt < Periapsis || Periapsis == 0)
			{
				Periapsis = CurrentAlt;
				pe = transform.position;
			}
		}

		Altitude = CurrentAlt;

		MajorAxis = Apoapsis + Periapsis;


		if (Apoapsis > 0 && Periapsis > 0)
		{

			if (Vector3.Distance(ap, transform.position) == 0)
			{
				//Debug.Log("AT AP");
				Orbits++;
			}

			if (Vector3.Distance(pe, transform.position) == 0)
			{
				//Debug.Log("AT PE");
			}
		}

	}

	public double calcSpeed(double Mass, double Radius)
	{
		return Math.Sqrt(solarSystem.Gravity.G * Mass / Radius);
	}

	public double calcPeriod(double Radius, double Mass)
	{
		return 2 * Math.PI * Math.Sqrt(Mathf.Pow((float)Radius, 3) / solarSystem.Gravity.G * Mass);
	}

	public double calcCircumfrence(double semiMajor, double semiMinor)
	{
		// C = 2 x π x √((a2 + b2) ÷ 2), 
		return 2 * Math.PI * Math.Sqrt((semiMajor * semiMajor + semiMinor * semiMinor) + 2);
	}

	float elapsed = 0f;
	private void DrawAxis() {

		elapsed += Time.deltaTime;
		if (elapsed >= 1f) {
         elapsed = elapsed % 1f;
			if (Lr)
			{
				//if(positionA && positionB)
				//{
					//float baseDistance = Vector3.Distance(positionA, positionB);
					//Math.Tan * (2 * CurrentAlt) / baseDistance

				//}
				//Debug.Log(Time.deltaTime);
			}
		}
        

	
		

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