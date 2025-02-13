using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crashing4 : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            PlasticCrash.Play();
        }
    }
    
    public AudioSource PlasticCrash;
}
