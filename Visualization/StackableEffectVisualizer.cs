using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableEffectVisualizer : EffectVisualizer
{
    public List<EffectVisualizer> stacksVisualizers;

    private StacksEffectActivator towerStacksEffect;

    private void Start()
    {
        towerStacksEffect = gameObject.GetComponent<StacksEffectActivator>();
        towerStacksEffect.maxStacksReached += MaxStacksReached;
    }

    private void MaxStacksReached()
    {
        foreach (EffectVisualizer vfx in stacksVisualizers)
        {
            vfx.StopVisualizer();
        }
    }

    public override void Visualize()
    {
        if (gameObject.TryGetComponent(out StacksEffectActivator towerStacksEffect))
        {
            for (int i = 0; i < towerStacksEffect.currentStacks; i++)
            {
                if (stacksVisualizers.Count > i && stacksVisualizers[i] != null)
                {
                    stacksVisualizers[i].Visualize();
                }
            }
        }
    }

    public override void StopVisualizer()
    {
        foreach (EffectVisualizer vfx in stacksVisualizers)
        {
            vfx.StopVisualizer();
        }
    }
}
