using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Steering high - predator - adapt turning angle

public class SuperPredator : Predator
{
    
    protected override void ChasePrey()
    {
        float minDistance = MAXDISTANCE;
        foreach (var collider2D in creaturesInRange)
        {
            float distance = Vector2.Distance(transform.position, collider2D.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetCollider2D = collider2D;
            }
        }
        movementScript.SteeringChase(targetCollider2D);
    }
}
