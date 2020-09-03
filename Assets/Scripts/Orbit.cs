using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public NewSystem solarSystem;

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
    public double Degrees;

    public float Orbits = 0f;

    public float thisRadius;
    public float focusRadius;

    private double CurrentAlt;
    private double Altitude; // 1 tick behind

    public Vector3 ae;
    public Vector3 pe;
    public Vector3 DescendingNode;
    public Vector3 AscendingNode;

    public float InititalVelocity;
    public float forceMult = 1;
    public float DistanceToPe;
    public float DistanceToAp;

    public GameObject nodePrefab;

    private void Start ()

    {

        Lr = rb.transform.GetComponent<LineRenderer> ();


        Vector3 targetVelocity = transform.rotation * Vector3.forward * (InititalVelocity * (float)solarSystem.DistanceScale);
        Vector3 force = targetVelocity * forceMult;
        rb.AddForce (force);

        Focus = FindOrbitalParent(solarSystem.Bodies, solarSystem.GetBodyFromTransform(transform)).Sphere;
        thisRadius = (transform.localScale.x / (float)solarSystem.DistanceScale) / 2 ;
        focusRadius = (Focus.transform.localScale.x  / (float)solarSystem.DistanceScale) / 2;

        if (Lr)
        {
            Lr.positionCount = 2;

        }

        // StartCoroutine(TrackAp());

        // StartCoroutine(TrackPe());

        
    }

    private void createNode(Vector3 position)
	{

        Instantiate(nodePrefab, position, Quaternion.identity);
    }

    private void FixedUpdate () {

        CurrentAlt = calcAltitude (); // Km

        ASL = calcAltitudeAboveSeaLevel (); // meters

        Velocity = calcVelocity (); // M/s

        OrbitalSpeed = calcSpeed (rb.mass, CurrentAlt); // Km/s

        Period = calcPeriod (MajorAxis > 0 ? MajorAxis / 2 : CurrentAlt, rb.mass);

        if (Altitude > 0) {
            if (CurrentAlt > Altitude && CurrentAlt > Apoapsis)
            {
                Apoapsis = CurrentAlt;
                ae = transform.position;
            }

            if (CurrentAlt < Altitude && CurrentAlt < Periapsis || Periapsis == 0) {
                Periapsis = CurrentAlt;
                pe = transform.position;
            }
        }

        Altitude = CurrentAlt;

        MajorAxis = Apoapsis + Periapsis;

        DrawAxis();

        Vector3 DifferenceAp = ae - transform.position;
        DistanceToAp = DifferenceAp.magnitude;

        Vector3 DifferencePe = pe - transform.position;
        DistanceToPe = DifferencePe.magnitude;


    }

    private bool AtAp()
	{
        if (Apoapsis > 0 && Periapsis > 0)
        {
            return Math.Floor(DistanceToAp) == 0;
        }
        return false;

    }

    private bool AtPe()
    {
        if (Apoapsis > 0 && Periapsis > 0)
        {
            return Math.Floor(DistanceToPe) == 0;
        }
        return false;

    }
    IEnumerator TrackAp()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Tracking");
        yield return new WaitUntil(AtAp);
        Debug.Log("At AP");
        if (ae == new Vector3(0, 0, 0))
        {

            createNode(transform.position);
        }
        StartCoroutine(TrackAp());

    }

    IEnumerator TrackPe()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Tracking");
        yield return new WaitUntil(AtPe);
        Debug.Log("At Pe");
        if (pe == new Vector3(0, 0, 0))
        {

            createNode(transform.position);
        }
        Orbits++;
        StartCoroutine(TrackPe());

    }
    public double calcVelocity () {
        return Math.Round ((rb.velocity.magnitude / solarSystem.DistanceScale), 4);
    }
    public double calcAltitude () {
        return Math.Round ((Vector3.Distance (transform.position, Focus.position) / solarSystem.DistanceScale), 4);
    }

    public double calcAltitudeAboveSeaLevel () {
        return Math.Round (Altitude - (thisRadius + focusRadius), 4);
    }

    public double calcSpeed (double Mass, double Radius) {
        return Math.Round (Math.Sqrt (solarSystem.Gravity.G * (Mass / solarSystem.MassScale) / Radius), 4);
    }

    public double calcPeriod (double Radius, double Mass) {
        return Math.Round (2 * Math.PI * Math.Sqrt (Mathf.Pow ((float) Radius, 3) / solarSystem.Gravity.G * (Mass / solarSystem.MassScale)), 2);
    }

    public double calcCircumference (double semiMajor, double semiMinor) {
        // C = 2 x π x √((a2 + b2) ÷ 2)
        return 2 * Math.PI * Math.Sqrt ((semiMajor * semiMajor + semiMinor * semiMinor) + 2);
    }

    public double radiansToDeg(double rads)
	{
        return (180 / Math.PI) * rads;
	}

    //float elapsed = 0f;
    private void DrawAxis () {
		if (Lr)
		{
            Lr.SetPosition(0, ae);
            Lr.SetPosition(1, pe);
        }
        

        //elapsed += Time.deltaTime;
        //if (elapsed >= 1f) {
          //  elapsed = elapsed % 1f;
           // if (Lr) {
                
             //   Lr.SetPosition(0, transform.position);
               // Lr.SetPosition(1, Focus.transform.position);
                //if(positionA && positionB)
                //{
                //float baseDistance = Vector3.Distance(positionA, positionB);
                //Math.Tan * (2 * CurrentAlt) / baseDistance

                //}
                //Debug.Log(Time.deltaTime);
    //        }
     //   }

    }

    public Body FindOrbitalParent (List<Body> Bodies, Body Child) {
        if (Child.Focus == null) {
            List<Body> tmp;
            tmp = new List<Body> ();

            foreach (Body body in Bodies) {
                if (body != Child) {
                    tmp.Add (body);
                }

            }

            tmp.Sort (CompareMasses);
            return tmp[tmp.Count - 1];
        }

        return Child.Focus;
    }

    private float SunPolarStrikes = -0.5f;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "OrbitalPlane")
		{
            if(collider.gameObject.name == "Polar" && Focus.gameObject.name == "Sun")
			{
                SunPolarStrikes+= 0.5f;

                Orbits = (float)Math.Floor(SunPolarStrikes - 1 / 2);
                
                //Debug.Log(collider.transform.rotation.z - transform.localRotation.z);
			}

            if (collider.gameObject.name == "Equator" && Focus.gameObject.name == "Sun")
            {
                Vector3 Difference = collider.transform.position - transform.position;

                if(Difference.z < 0)
				{
                    if(DescendingNode == new Vector3(0, 0, 0))
					{
                        
                        createNode(transform.position);
                    }
                    Debug.Log("Descending Node");
                    DescendingNode = transform.position;
				}

                if (Difference.z > 0)
                {
                    if (AscendingNode == new Vector3(0, 0, 0))
                    {
                       
                        createNode(transform.position);
                    }
                    Debug.Log("Ascending Node");
                    AscendingNode = transform.position;
                }
            }
        }
        //Debug.Log(collider.gameObject.name);
        //Debug.Log(collider.gameObject.tag);

        //Debug.Log(Focus.gameObject.name);
    }



    static int CompareMasses (Body b1, Body b2) {
        return b1.Mass.CompareTo (b2.Mass);
    }
}