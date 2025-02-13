using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crashing3 : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            WoodCrash.Play();
        }
    }
  
   public AudioSource WoodCrash;
}
