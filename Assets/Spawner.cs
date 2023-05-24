using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prey;
    public GameObject predator;
    public GameObject superpredator;

    public int preysTotal = 5;
    public int predatorsTotal = 5;
    public int superPredatorsTotal = 5;

    public int spawnRate = 2;
    public float spawnRadius = 20.0f;

    public void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            GameObject[] preys = GameObject.FindGameObjectsWithTag("Prey");
            if (preys.Length < preysTotal)
            {
                GameObject a = GameObject.Instantiate(prey);
                Vector2 position = Random.insideUnitCircle * spawnRadius;
                //a.transform.position = new Vector3(position.x, 0, position.y);
                a.transform.position = new Vector2(position.x, position.y);
                //a.transform.parent = this.transform;
              
            }
            GameObject[] predators = GameObject.FindGameObjectsWithTag("Predator");
            if (predators.Length < predatorsTotal)
            {
                GameObject p = GameObject.Instantiate(predator);
                Vector2 position = Random.insideUnitCircle * spawnRadius;
                p.transform.position = new Vector2(position.x, position.y);
                //p.transform.parent = this.transform;
                
            }
            GameObject[] superPredators = GameObject.FindGameObjectsWithTag("SuperPredator");
            if (superPredators.Length < superPredatorsTotal)
            {
                GameObject sp = GameObject.Instantiate(superpredator);
                Vector2 position = Random.insideUnitCircle * spawnRadius;
                sp.transform.position = new Vector2(position.x, position.y);
                //sp.transform.parent = this.transform;
                
            }

            yield return new WaitForSeconds( 5.0f / (float) spawnRate);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
