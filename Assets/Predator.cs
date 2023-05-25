using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PredatorState
{
    WANDER,
    CHASE,
    ATTACK,
    REST
}


public class Predator : MonoBehaviour
{
   // [Range(0, 10)]
   // public float moveSpeed;
    protected Movement movementScript;

    protected PredatorState predatorState;

    protected float detectionRadius=2.0f;

    public LayerMask preyLayer;

    protected Collider2D[] creaturesInRange;



    // Start is called before the first frame update
    void Start()
    {
        predatorState = PredatorState.WANDER;
        movementScript = GetComponent<Movement>();
        StartCoroutine(movementScript.RotateCreature());
    }

    // Update is called once per frame
    void Update()
    {
        DetectPrey();
        switch (predatorState)
        {
            case PredatorState.WANDER:
                WanderPredator();
                break;
            case PredatorState.CHASE:
                ChasePrey();
                break;
            case PredatorState.ATTACK:
                AttackPrey();
                break;
            case PredatorState.REST:
                StartCoroutine(RestPredator());
                break;
            default:
                break;
        }
        movementScript.WrapAround();


    }

    void DetectPrey()
    {
        creaturesInRange = Physics2D.OverlapCircleAll(transform.position, detectionRadius, preyLayer);
        if (creaturesInRange.Any() && predatorState == PredatorState.WANDER) 
            predatorState = PredatorState.CHASE;
    }
    void WanderPredator()
    {
        movementScript.MoveCreature();
    }

    protected const float MAXDISTANCE = 999.00f;
    protected Collider2D targetCollider2D;
    protected virtual void ChasePrey()
    {
        //if (!overlappedCircle.Any()) return;
        //overlappedCircle.Select(o => o.transform.position )
        float minDistance = MAXDISTANCE;
        foreach (var collider2D in creaturesInRange)
        {
            float distance = Vector2.Distance(transform.position, collider2D.transform.position);
            if (distance  < minDistance)
            {
                minDistance = distance;
                targetCollider2D = collider2D;
            }
        }
        movementScript.Chase(targetCollider2D);
    }

    void AttackPrey()
    {
        if(targetCollider2D != null)
            movementScript.Attack(targetCollider2D);
        if(targetCollider2D == null)
            predatorState = PredatorState.REST; 
    }

    IEnumerator RestPredator()
    {
        movementScript.Rest();
        yield return new WaitForSeconds(5.0f);
        predatorState = PredatorState.WANDER;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == targetCollider2D)
        {
            predatorState = PredatorState.ATTACK;
        }
    }
}
