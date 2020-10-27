using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectVisualizer : MonoBehaviour
{    
    public float duration = 0f;

    protected ActiveEffect effect;

    public void Initialize(ActiveEffect effect)
    {
        this.effect = effect;
    }

    public abstract void Visualize();

    public abstract void StopVisualizer();

}