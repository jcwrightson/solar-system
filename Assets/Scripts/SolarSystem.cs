using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{

	public List<Transform> Spheres;
	public List<Body> Bodies;
	public List<Planet> Planets;

	public Gravity Gravity;

	public double MassScale;
	public double SizeScale;
	public double DistanceScale;


	private void Awake()
	{
		Time.timeScale = 1f;
		Gravity = new Gravity(3);
		CreateSolarSystem();

	}

	private void CreateSolarSystem()
	{

		MassScale = 0.0000000000000000000001;
		SizeScale = 0.00000001;
		DistanceScale = 0.000000001;

		foreach (Transform sphere in transform)
		{

			Spheres.Add(sphere);

			foreach (Transform moon in sphere.transform)
			{
				Spheres.Add(moon);
			}
		}

		Planets.Add(new Planet("Sun", new Body(1.98847 * Math.Pow(10, 30), 696342 * Math.Pow(10, 3), 0, Spheres[0]))); // I know right...
		Planets.Add(new Planet("Mercury", new Body(3.285 * Math.Pow(10, 23), 2439 * Math.Pow(10, 3), 70 * Math.Pow(10, 6) * 1000, 1370f, Spheres[1])));
		Planets.Add(new Planet("Venus", new Body(4.867 * Math.Pow(10, 24), 6052 * Math.Pow(10, 3), 108.2 * Math.Pow(10, 6) * 1000, Spheres[2])));

		Planets.Add(new Planet("Earth", new Body(5.972 * Math.Pow(10, 24), 6371 * Math.Pow(10, 3), 149.6 * Math.Pow(10, 6) * 1000, Spheres[3])));
		Planets[3].CreateMoon(new Body(0.073 * Math.Pow(10, 24), 1737.5 * Math.Pow(10, 3), 384400 * 1000, Spheres[4], Planets[3].Body));

		Planets.Add(new Planet("Mars", new Body(0.642 * Math.Pow(10, 24), 3396 * Math.Pow(10, 3), 227.9 * Math.Pow(10, 6) * 1000, Spheres[5])));
		Planets.Add(new Planet("Jupiter", new Body(1.89813 * Math.Pow(10, 27), 71492 * Math.Pow(10, 3), 778.6 * Math.Pow(10, 6) * 1000, Spheres[6])));
		Planets.Add(new Planet("Saturn", new Body(568 * Math.Pow(10, 24), 60268 * Math.Pow(10, 3), 1433.5 * Math.Pow(10, 6) * 1000, Spheres[7])));
		Planets.Add(new Planet("Uranus", new Body(86.8 * Math.Pow(10, 24), 25559 * Math.Pow(10, 3), 2872.5 * Math.Pow(10, 6) * 1000, Spheres[8])));
		Planets.Add(new Planet("Neptune", new Body(102 * Math.Pow(10, 24), 24764 * Math.Pow(10, 3), 4495.1 * Math.Pow(10, 6) * 1000, Spheres[9])));
		Planets.Add(new Planet("Pluto", new Body(0.0146 * Math.Pow(10, 24), 1185 * Math.Pow(10, 3), 5906.4 * Math.Pow(10, 6) * 1000, Spheres[10])));

		foreach (Planet planet in Planets)
		{
			planet.Body.PositionAndScale(SizeScale, DistanceScale, MassScale);
			Bodies.Add(planet.Body);

			if (planet.Moons != null)
			{

				foreach (Body moon in planet.Moons)
				{
					moon.PositionAndScale(SizeScale, DistanceScale, MassScale);
					Bodies.Add(moon);
				}
			}
		}

	}

	public Body GetBodyFromTransform(Transform transform)
	{
		foreach (Body body in Bodies)
		{
			if (body.Sphere == transform)
			{
				return body;
			}
		}

		return null;
	}

	private void FixedUpdate()
	{

		Gravity.Newtonize(Bodies);
	}
}