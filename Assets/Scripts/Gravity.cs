using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gravity {

	public double G;
	public bool ShowForceLines = true;
	private List<LineRenderer> Lines;

	public Gravity () {
		G = 6.67408f * Math.Pow(10, -11);
		Lines = new List<LineRenderer>();
	}

	public Gravity(float gexp){

		G = (6.67408f * Math.Pow(10, -11)) * Math.Pow(10, gexp);
		Lines = new List<LineRenderer>();

	}

	public Gravity(float g, int gexp){

		G = g * Mathf.Exp(gexp);
		Lines = new List<LineRenderer>();

	}

	public double GravityForce(double M1, double M2, float Distance){
	
		return G * (M1 * M2) / Math.Pow(Distance, 2);

	}

	public void Newtonize(List<Body> Bodies) {

		if (Bodies.Count < 2){
			return;
		}


		if (ShowForceLines)
		{
			if (Lines.Count == 0)
			{
				AttachLines(Bodies);
			}
			else
			{
				UpdateLineOrigin(Bodies);
			}
		}


		int i = 0;
		foreach (Body body1 in Bodies){

			
			foreach (Body body2 in Bodies) {

				if (ShowForceLines)
				{
					UpdateLineOrigin(Bodies);
				}

				if (body1 != body2){

					Vector3 Difference = body1.Sphere.transform.position - body2.Sphere.transform.position;
					float Distance = Difference.magnitude;
					double Gf = GravityForce(body1.Sphere.GetComponent<Rigidbody>().mass, body2.Sphere.GetComponent<Rigidbody>().mass, Distance);
					Vector3 Gv = (Difference.normalized * (float)Gf);

					body2.Sphere.GetComponent<Rigidbody>().AddForce(Gv, ForceMode.Force);

					if (ShowForceLines) { 

						UpdateLineVector(body2.Sphere.transform.position, i, (float)Gf, Distance);
					}

				
					i++;
				}
			}
		}
	}

	private void AttachLines(List<Body> Bodies)
	{
		foreach(Body body in Bodies)
		{

			foreach (Body bbody in Bodies){

				if(bbody != body)
				{
					LineRenderer Line = new GameObject().AddComponent<LineRenderer>();
					Line.material = new Material(Shader.Find("Sprites/Default"));
					Line.useWorldSpace = true;
					Line.SetPosition(0, body.Sphere.position);
					Line.SetPosition(1, body.Sphere.position);
					Lines.Add(Line);
				}

			}

		}
	}

	private void UpdateLineOrigin(List<Body> Bodies){

		int i = 0;
		int	x = 1;

		foreach (Body body in Bodies){
			int LinesPerBody = Bodies.Count - 1;

			while ( i < x * LinesPerBody)
			{
				Lines[i].SetPosition(0, body.Sphere.position);
				i++;
			}
			x++;
			
		}
	}

	private void UpdateLineVector(Vector3 gvpos, int i, float F, float D){

		float strength = (F / D) * 100;

		Lines[i].startWidth = 0;
		Lines[i].endWidth = strength;
		Lines[i].SetPosition(1, gvpos);
		Lines[i].startColor = Color.red;
		Lines[i].endColor = Color.clear;
	}

}
