using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public static DeathCounter dc;
    public static int preyDeaths=0;
    public static int predatorDeaths =0;
    public static int superPredatorDeaths =0;

    //Get Components
    public GameObject preyText;
    public GameObject predatorText;
    public GameObject superPredatorText;

    // Start is called before the first frame update
    void Start()
    {
        if (dc == null)
        {
            dc = this.gameObject.GetComponent<DeathCounter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PrintDeaths();
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

    void PrintDeaths()
    {
        string preyDeathsString = "Prey Deaths: " + preyDeaths.ToString();
        string predatorDeathsString = "Predator Deaths: " + predatorDeaths.ToString();
        string superPredatorDeathsString = "SuperPredator Deaths: " + superPredatorDeaths.ToString();

        preyText.GetComponent<TextMeshProUGUI>().text = preyDeathsString;
        predatorText.GetComponent<TextMeshProUGUI>().text = predatorDeathsString;
        superPredatorText.GetComponent<TextMeshProUGUI>().text = superPredatorDeathsString;
    }
}
