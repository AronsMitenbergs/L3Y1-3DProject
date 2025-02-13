using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crashing2 : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            MetalCrash.Play();
        }
    }

    public AudioSource MetalCrash;
}
