using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame_Point : MonoBehaviour
{
    [SerializeField] private ParticleSystem left;
    [SerializeField] private ParticleSystem right;

    private void Start()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {  
            left.Play();
            right.Play();
        }
    }

}
