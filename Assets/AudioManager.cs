using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;
    public AudioSource ambience;
    public AudioSource eating;
    // Start is called before the first frame update
    void Start()
    {
        if(am == null)
        {
            am = this;
        }
        //PlayAmbience();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void PlayAmbience()
    {
        ambience.Play();
    }

    public void PlayEat()
    {
        //eating.Play();
    }
}
