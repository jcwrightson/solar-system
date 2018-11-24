using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body {

    public double Mass;
    public double Radius;
    public double DistanceToParent;

    public Body(double mass, double radius, double distanceToParent){
        Mass = mass;
        Radius = radius;
        DistanceToParent = distanceToParent;
    }

}
