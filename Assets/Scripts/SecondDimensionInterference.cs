using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDimensionInterference : MonoBehaviour
{
    private ParticleSystem particle;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particle.isStopped)
            Destroy(this.gameObject);
    }

    public void Loop()
    {
        ParticleSystem.MainModule main = particle.main;
        main.loop = true;
    }
}
