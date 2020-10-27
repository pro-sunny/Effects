using ActionGameFramework.Health;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StacksEffectActivator : EffectActivator
{
    [ReadOnly]
    public int currentStacks;
    public int maxStacks;    

    public EffectActivator OnEveryStackEffect;
    public EffectActivator OnMaxStacksEffect;

    public event Action maxStacksReached;

    public override void Initialize(List<Targetable> targets)
    {
        foreach (Targetable target in targets)
        {
            ActiveUnit unit = target as ActiveUnit;

            EffectActivator appliedEffect = null;
            foreach (EffectActivator effect in unit.appliedEffects)
            {
                if (effect.activeEffect.UID == activeEffect.UID )
                {
                    appliedEffect = effect;
                }
            }
            
            if (appliedEffect != null)
            {
                appliedEffect.ActivateEffect();
            } else
            {                
                EffectActivator newEffect = Instantiate(activeEffect.effectActivator, target.transform.position, Quaternion.identity, target.transform);
                unit.appliedEffects.Add(newEffect);

                StacksEffectActivator effect = newEffect.gameObject.GetComponent<StacksEffectActivator>();                
                effect.targets = new List<Targetable> { target };
                effect.ActivateEffect();
            }
        }
    }

    public override void ActivateEffect()
    {
        currentStacks++;
        
        if (currentStacks == maxStacks)
        {
            if (OnMaxStacksEffect != null)
            {
                OnMaxStacksEffect.Initialize(targets);

                maxStacksReached?.Invoke();
            }            
            currentStacks = 0;
        } else
        {
            OnEveryStackEffect.Initialize(targets);
        }
    }
}