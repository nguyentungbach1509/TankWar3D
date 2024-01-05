using System.Collections;
using System.Collections.Generic;
using Others;
using UnityEngine;

public class ExplodePooling : Singleton<ExplodePooling>
{
    [SerializeField] ParticleSystem exploding;
    ParticleSystem[] particles = new ParticleSystem[50];
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i] = Instantiate(exploding, transform);
            particles[i].gameObject.SetActive(false);
        }
    }

    public ParticleSystem Explode(Vector3 position)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            if (!particles[i].gameObject.activeInHierarchy)
            {
                particles[i].transform.position = position;
                particles[i].gameObject.SetActive(true);
                return particles[i];
            }
        }
        return null;
    }
}