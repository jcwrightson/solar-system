using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSystem : MonoBehaviour
{
    public List<Transform> Spheres;
    public List<Body> Bodies;
    public List<GameObject> Nodes;

    public Gravity Gravity;

    public double MassScale;
    public double SizeScale;
    public double DistanceScale;

    void Start()
    {

      

    }

    void Awake()
	{
        Gravity = new Gravity(10);
        Time.timeScale = 6f;
        MassScale = 1;
        SizeScale = 1;
        DistanceScale = 1;

        foreach (Transform sphere in transform)
        {

            Spheres.Add(sphere);
            Bodies.Add(new Body(sphere));


         //   foreach (Transform moon in sphere.transform)
         //   {
         //       Spheres.Add(moon);
         //       Bodies.Add(new Body(sphere));
         //   }
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        Gravity.Newtonize(Bodies);
    }
}
