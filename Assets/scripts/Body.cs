using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Body {

    public Transform Sphere;

    public double Mass;
    public double Radius;
    public double DistanceToParent;

    public Body(double mass, double radius, double distanceToParent, Transform sphere){
        Mass = mass;
        Radius = radius;
        DistanceToParent = distanceToParent;
        Sphere = sphere;
    }

   

}
