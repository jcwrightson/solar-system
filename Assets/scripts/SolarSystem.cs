using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour {

    public GameObject[] celestialBodies;
    public double G;
    public double MassScale;
    public double SizeScale;

    [Range(1f,100f)]
    public float TimeWarp;

    private void Awake()
    {
        celestialBodies = GameObject.FindGameObjectsWithTag("Celestial");
        MassScale = 0.0000000000000000000001;
        SizeScale = 0.0000001;
        TimeWarp = 10;

        G = 6.67384 * Math.Pow(10, -11) * 100000000f;

        Time.timeScale = Time.timeScale * TimeWarp;
        //Time.fixedDeltaTime = Time.fixedDeltaTime / Time.timeScale;

    }

    public void PositionAndScale(Double Mass, Double Radius, Double DistanceToParent, Rigidbody Rb)
    {
        double ScaledMass = Mass * MassScale;
        double ScaledRadius = (Radius * 2) * SizeScale;
        double ScaledDistanceToParent = DistanceToParent * SizeScale;

        Rb.mass = (float)ScaledMass;
        Rb.transform.localScale = new Vector3((float)ScaledRadius, (float)ScaledRadius, (float)ScaledRadius);
        Rb.transform.localPosition = new Vector3(-(float)ScaledDistanceToParent, 0, 0);
    }

    private void FixedUpdate () {        

        foreach (GameObject Body in celestialBodies)
        {
           
            Vector3 CurrentPosition = Body.transform.position;
            float thisMass = Body.GetComponent<Rigidbody>().mass;

            foreach (GameObject BBody in celestialBodies)
            {
                if (Body != BBody)
                {

                    Vector3 Difference = CurrentPosition - BBody.transform.position;
                    float Distance = Difference.magnitude;
                    Vector3 GravityDirection = Difference.normalized;

                   
                    float GravityForce = (float)G * (BBody.GetComponent<Rigidbody>().mass * thisMass) / (Distance * Distance);
                    Vector3 GravityVector = (GravityDirection * GravityForce);

                    BBody.GetComponent<Rigidbody>().AddForce(GravityVector, ForceMode.Force);
                }
            }
        }
	}
}
