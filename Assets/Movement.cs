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

    int randDirectionInt;
    const float angularVelocity = 60;
    void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(RotateCreature());
    }

    // Update is called once per frame
    void Update()
    {
        MoveCreature();
    }

    void MoveCreature()
    {
        float randForwardInt = Random.Range(0.2f, 1);

        transform.eulerAngles += new Vector3(0, 0, randDirectionInt * Time.deltaTime * angularVelocity);
        myRigidbody.velocity = transform.up * randForwardInt;
    } 

    IEnumerator RotateCreature()
    {
        randDirectionInt = Random.Range(-1, 2);
        yield return new WaitForSeconds(2);
        StartCoroutine(RotateCreature());
    }
}
