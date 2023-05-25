using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    public DeathCounter dc;
    public static int preyDeaths=0;
    public static int predatorDeaths =0;
    public static int superPredatorDeaths =0;
    // Start is called before the first frame update
    void Start()
    {
        if(dc == null)
        {
            dc = this.gameObject.GetComponent<DeathCounter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void TrackDeath(string tag)
    {
        if(tag == "Prey")
        {
            preyDeaths++;
        }
        else if(tag == "Predator"){
            predatorDeaths++;
        }
        else if(tag == "SuperPredator"){
            superPredatorDeaths++;
        }
    }

    private void PrintDeaths()
    {
        Debug.Log("Prey Killed" + preyDeaths);
        Debug.Log("Predator Killed" + predatorDeaths);
        Debug.Log("SuperPredator Killed" + superPredatorDeaths);

    }
}
