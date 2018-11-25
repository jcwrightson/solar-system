using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gravity {

	public double G;
	public List<LineRenderer> Lines;

	public Gravity () {
		G = 6.67408f * Math.Pow(10, -11);
		Lines = new List<LineRenderer>();
	}

	public Gravity(int gexp)
	{
		G = G * Mathf.Exp(gexp);
		Lines = new List<LineRenderer>();
	}

	public Gravity(float g, int gexp){
		G = g * Mathf.Exp(gexp);
		Lines = new List<LineRenderer>();
	}

	public void Newtonize(List<Body> Bodies) {

		if (Bodies.Count < 2){
			return;
		}

		if(Lines.Count == 0)
		{
			AttachLines(Bodies);
		}else
		{
			UpdateLineOrigin(Bodies);
		}


		int i = 0;
		foreach (Body body in Bodies){

			Vector3 CurrentPosition = body.Sphere.transform.position;
			float thisMass = body.Sphere.GetComponent<Rigidbody>().mass;

			foreach (Body bbody in Bodies) {

				UpdateLineOrigin(Bodies);

				if (body != bbody){


					Vector3 Difference = CurrentPosition - bbody.Sphere.transform.position;
					float Distance = Difference.magnitude;
					float GravityForce =  (float)G * 100000000 * (bbody.Sphere.GetComponent<Rigidbody>().mass * thisMass) / (Distance * Distance);
					Vector3 Gv = (Difference.normalized * GravityForce);

					bbody.Sphere.GetComponent<Rigidbody>().AddForce(Gv, ForceMode.Force);
					UpdateLineVector(bbody.Sphere.transform.position, i, GravityForce);

				
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

	private void UpdateLineVector(Vector3 gvpos, int i, float F){

		Lines[i].startWidth = 5f;
		Lines[i].endWidth = F;
		Lines[i].SetPosition(1, gvpos);
		Lines[i].startColor = Color.red;
		Lines[i].endColor = Color.blue;
	}

}
