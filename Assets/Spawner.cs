using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prey;
    public GameObject poisonPrey;
    public GameObject predator;
    public GameObject superpredator;

    public int preysTotal = 5;
    public int predatorsTotal = 5;
    public int superPredatorsTotal = 5;

    public int spawnPreyRate = 10;
    public int spawnPredatorRate = 5;
    public int spawnSuperpredatorRate = 1;

    public float spawnRadius = 20.0f;

    public void OnEnable()
    {
        StartCoroutine(SpawnPrey());
        StartCoroutine(SpawnPredator());
        StartCoroutine(SpawnSuperPredator());
    }

    IEnumerator SpawnSuperPredator()
    {
        while (true)
        {
            GameObject[] superPredators = GameObject.FindGameObjectsWithTag("SuperPredator");
            if (superPredators.Length < superPredatorsTotal)
            {
                GameObject sp = GameObject.Instantiate(superpredator);
                Vector2 position = Random.insideUnitCircle * spawnRadius;
                sp.transform.position = new Vector2(position.x, position.y);
                sp.transform.parent = this.transform;
                yield return new WaitForSeconds(5.0f / (float)spawnSuperpredatorRate);
            }
            else
            {
                yield return 0;
            }
            
        }
    }


    IEnumerator SpawnPredator()
    {
        while (true)
        {
            GameObject[] predators = GameObject.FindGameObjectsWithTag("Predator");
            if (predators.Length < predatorsTotal)
            {
                GameObject p = GameObject.Instantiate(predator);
                Vector2 position = Random.insideUnitCircle * spawnRadius;
                p.transform.position = new Vector2(position.x, position.y);
                p.transform.parent = this.transform;
                yield return new WaitForSeconds(5.0f / (float)spawnPredatorRate);
            }
            else
            {
                yield return 0;
            }
        }
    }

    IEnumerator SpawnPrey()
    {
        while(true)
        {
            GameObject[] preys = GameObject.FindGameObjectsWithTag("Prey");
            if (preys.Length < preysTotal)
            {
                int RNG = Random.Range(0, 11);
                Vector2 position = Random.insideUnitCircle * spawnRadius;
                if (RNG == 5)
                {
                    GameObject pp = GameObject.Instantiate(poisonPrey);
                    pp.transform.position = new Vector2(position.x, position.y);
                    pp.transform.parent = this.transform;
                    preysTotal++;
                }
               
                GameObject p = GameObject.Instantiate(prey);
                p.transform.position = new Vector2(position.x, position.y);
                p.transform.parent = this.transform;
                yield return new WaitForSeconds(5.0f / (float)spawnPreyRate);

            }
            else
            {
                yield return 0;
            }



            // yield return new WaitForSeconds( 5.0f / (float) spawnRate);
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
