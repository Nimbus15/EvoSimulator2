using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO LIST
//Confine To Environment //* Jitterwandering
//Move the Camera using "WASD" keys//** Input system
//Health or instant death //** health
//Duplicate Preys **
//Creature Fill Needs **

//Food - grass **

//Individual //if together in range > 3 then flock
//Flocks - SAP
//Procreate //if > 1 in range and flock and wait 5mins then duplicate smaller
//Create Predator


public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D myCollider;
    public BoxCollider2D environmentBoundary;
    private Rigidbody2D myRigidbody;

    public float randDirectionFloat;
    const float angularVelocity = 60.0f;
    void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
       // StartCoroutine(RotateCreature());
    }

    // Update is called once per frame
    void Update()
    {
       // MoveCreature();
    }

    //WANDER
    public void MoveCreature()
    {
        Vector3 newPosition = JitterWander.Calculate(this.transform);
        float randForwardFloat = Random.Range(0.2f, 1.0f);

        transform.eulerAngles += new Vector3(0, 0, randDirectionFloat * Time.deltaTime * angularVelocity);
        myRigidbody.velocity = newPosition + transform.up * randForwardFloat;
    } 

    public IEnumerator RotateCreature()
    {
        randDirectionFloat = Random.Range(-1.0f, 1.0f);
        yield return new WaitForSeconds(2);
        StartCoroutine(RotateCreature());
    }

    private float chaseSpeed = 2.00f;

    //CHASE
    public void Chase(Collider2D targetToChase)
    {
        Vector2 targetDirection = targetToChase.transform.position - transform.position;
        myRigidbody.velocity = targetDirection * chaseSpeed;
        transform.up = myRigidbody.velocity;
    }


    //Steering CHASE
    protected float rotationSpeedRadian = 0.1f;
    public void SteeringChase(Collider2D targetToChase)
    {
        rotationSpeedRadian = Mathf.Abs(targetToChase.GetComponent<Movement>().randDirectionFloat);
        Vector2 rotatedTowardsDirection = Vector3.RotateTowards(transform.up, targetToChase.transform.position - transform.position,
            rotationSpeedRadian * Time.deltaTime, 0);
        myRigidbody.velocity = rotatedTowardsDirection * chaseSpeed;
        transform.up = myRigidbody.velocity;
    }

    float elaspedTime = 0.0f;
    //ATTACK
    public void Attack(Collider2D targetToKill)
    {
        elaspedTime += Time.deltaTime;
        if(elaspedTime > 0.5f)
        {
            Destroy(targetToKill.gameObject);
            elaspedTime = 0.0f;
        }
    }

    //REST
    public void Rest()
    {
        myRigidbody.velocity = Vector2.zero;
    }

    private Vector2 Position
    {
        get
        {
            return gameObject.transform.position;
        }
        set
        {
            gameObject.transform.position = value;
        }
    }
    public void WrapAround()
    {
       
        if (Position.x < -14) Position = new Vector2(14, Position.y);
        if (Position.y < -8) Position = new Vector2(Position.x, 8);
        if (Position.x > 14) Position = new Vector2(-14, Position.y);
        if (Position.y > 8) Position = new Vector2(Position.x, -8);
    }
}
