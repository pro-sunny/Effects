using ActionGameFramework.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantEffectActivator : EffectActivator
{
    public override void ActivateEffect()
    {
        foreach (Targetable target in targets)
        {
            ApplyVisualization(target);
            ApplyEffect(target);
        }
    }
}
