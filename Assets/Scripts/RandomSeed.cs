using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeed : MonoBehaviour {

	public List<GameObject> Bodies;
	public GameObject body;
	public GameObject parent;

	public double G;
	public bool GravityEnabled;
	public double GravityMultiplier;
	public int NoOfBodies;

	// Use this for initialization
	void Start () {

		G = 6.67384 * Math.Pow(10, -11);
		//GravityMultiplier = 1000000000000000;
		GravityEnabled = true;

		//Time.timeScale = Time.timeScale * 0.2f;
		//Time.fixedDeltaTime = Time.fixedDeltaTime / Time.timeScale;


		
		Bodies = new List<GameObject>();

		for (int i = 0; i < NoOfBodies; i++)
		{

			float RandSize = UnityEngine.Random.Range(0.001f, 100f);
			float RandMass = UnityEngine.Random.Range(0.01f, 100000000f);
			Vector3 Scale = new Vector3(RandSize, RandSize, RandSize);
			Vector3 Position = new Vector3(UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(-100f, 100f));

		
			Bodies.Add(Instantiate(body, Position, Quaternion.identity, parent.transform));
			Bodies[i].transform.localScale = Scale;
			Bodies[i].GetComponent<Rigidbody>().mass = RandMass;
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (GameObject body in Bodies)
		{

			Vector3 CurrentPosition = body.transform.position;
			float thisMass = body.GetComponent<Rigidbody>().mass;

			foreach (GameObject bbody in Bodies)
			{
				if (body != bbody)
				{

					Vector3 Difference = CurrentPosition - bbody.transform.position;
					float Distance = Difference.magnitude;
					float GravityForce = (float)(G * GravityMultiplier) * (bbody.GetComponent<Rigidbody>().mass * thisMass) / (Distance * Distance);
					Vector3 GravityVector = (Difference.normalized * GravityForce);

					bbody.GetComponent<Rigidbody>().AddForce(GravityVector, ForceMode.Force);
				}
			}
		}
	}
}
