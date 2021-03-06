﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planet {

	public string Name;
	public List<Body> Moons;
	public Body Body;

	public Planet(string name, Body planetBody) {
		Name = name;
		Body = planetBody;
		Moons = new List<Body>();
	}

	public void CreateMoon(Body moon)
	{
		Moons.Add(moon);
	}
}
