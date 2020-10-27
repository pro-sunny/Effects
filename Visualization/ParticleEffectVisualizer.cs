using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectVisualizer : EffectVisualizer
{
    public ParticleSystem ps;

    public override void Visualize()
    {        
        if (!Mathf.Approximately(duration, 0))
        {
            ParticleSystem.MainModule main = ps.main;
            main.duration = duration;
        }

        ps.Play();
    }

    public override void StopVisualizer()
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
