using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D myCollider;
    public BoxCollider2D environmentBoundary;
    private Rigidbody2D myRigidbody;

    public int randDirectionInt;
    float angularVelocity = 60.0f;
    void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        angularVelocity = Random.Range(40.0f, 60.0f);
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

        transform.eulerAngles += new Vector3(0, 0, randDirectionInt * Time.deltaTime * angularVelocity);
        myRigidbody.velocity = newPosition + transform.up * randForwardFloat;
    } 

    public IEnumerator RotateCreature()
    {
        randDirectionInt = Random.Range(-1, 2);
        yield return new WaitForSeconds(2);
        StartCoroutine(RotateCreature());
    }

    private float chaseSpeed = 2.00f;

    //CHASE
    public void Chase(Collider2D targetToChase)
    {
        if (targetToChase == null)
            return;
        Vector2 targetDirection = targetToChase.transform.position - transform.position;
        myRigidbody.velocity = targetDirection * chaseSpeed;
        transform.up = myRigidbody.velocity;
    }

    float elaspedLiveTime = 0.0f;
    //Steering CHASE
    protected float rotationSpeed = 0.1f;

    public void SteeringChase(Collider2D targetToChase)
    {
        elaspedLiveTime += Time.deltaTime;
        if (targetToChase == null)
            return;
        //rotationSpeedRadian = Mathf.Abs(targetToChase.GetComponent<Movement>().randDirectionInt);
        Vector2 rotatedTowardsDirection = Vector3.RotateTowards(transform.up, targetToChase.transform.position - transform.position,
            rotationSpeed * Time.deltaTime * Mathf.Rad2Deg, 0);
        myRigidbody.velocity = rotatedTowardsDirection * chaseSpeed;
        transform.up = myRigidbody.velocity;
        if (elaspedLiveTime > 0.2f)
        {
            float targetAngularSpeed = Mathf.Abs(targetToChase.GetComponent<Movement>().angularVelocity);
            if (targetAngularSpeed > rotationSpeed)
            {
                rotationSpeed += 5;
            }
            else
            {
                rotationSpeed -= 5;
            }
        }
    }
    float elaspedTime = 0.0f;
    //ATTACK
    public void Attack(Collider2D targetToKill)
    {
        elaspedTime += Time.deltaTime;
        if(elaspedTime > 0.2f)
        {
            Debug.Log(this.gameObject.tag + " Killed: " + targetToKill.gameObject.tag);
            DeathCounter.TrackDeath(targetToKill.gameObject.tag);
            int randInt = Random.Range(0, 11);
            if(randInt == 5)
                AudioManager.am.PlayEat();
            if (targetToKill.tag == "PoisonPrey")
                Destroy(this.gameObject, 3.0f);
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
