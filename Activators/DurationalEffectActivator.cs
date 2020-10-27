using ActionGameFramework.Health;
using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationalEffectActivator : EffectActivator
{
    public float duration;
    public float bonusDuration;
    public ParameterModiferValue bonusValue;

    [HideInInspector]
    public Targetable target;

    public EffectActivator OnStartEffect;
    public EffectActivator OnEverySecondEffect;
    public EffectActivator OnEndEffect;

    public OnEffectDuplication onEffectDuplication;

    private SteppingTimer timer;

    private Action onTimerStarts;
    private Action onEveryStep;
    private Action onTimerEnds;

    public float timerValue;

    protected virtual void Update()
    {
        if (timer == null)
        {
            return;
        }
        timer.Tick(Time.deltaTime);
    }

    public override void Initialize(List<Targetable> targets)
    {
        foreach (Targetable target in targets)
        {
            ActiveUnit unit = target as ActiveUnit;

            EffectActivator appliedEffect = null;
            foreach (EffectActivator effect in unit.appliedEffects)
            {
                if (effect.activeEffect.UID == activeEffect.UID)
                {
                    appliedEffect = effect;
                }
            }

            if (appliedEffect != null)
            {
                // Do the check if effect got reapplied, do different things !!!
                if (onEffectDuplication == OnEffectDuplication.IncreaseDuration)
                {                    
                    DurationalEffectActivator durationalEffectActivator = appliedEffect as DurationalEffectActivator;
                    durationalEffectActivator.IncreaseDuration(bonusDuration);
                }
                if (onEffectDuplication == OnEffectDuplication.IncreaseEffect)
                {
                    /*
                    foreach (var item in ParameterModiferValue)
                    {
                        durationEffect.values += bonusValue;
                    }
                    */
                }
            }
            else
            {
                EffectActivator newEffect = Instantiate(activeEffect.effectActivator, target.transform.position, Quaternion.identity, target.transform);
                unit.appliedEffects.Add(newEffect);

                DurationalEffectActivator durationalEffect = newEffect.gameObject.GetComponent<DurationalEffectActivator>();
                durationalEffect.targets = new List<Targetable> { target }; 
                durationalEffect.ActivateEffect();
            }
        }
    }

    public override void ActivateEffect()
    {
        timer = new SteppingTimer(duration, OnEffectStarts, OnEverySecond, OnEffectEnds);
        visualizer.Visualize();
    }

    private void OnEffectStarts()
    {
        OnStartEffect.Initialize(targets);
    }

    private void OnEverySecond()
    {
        OnEverySecondEffect.Initialize(targets);
    }
    private void OnEffectEnds()
    {
        OnEndEffect.Initialize(targets);        
        visualizer.StopVisualizer();
        timer = null;
    }

    private void IncreaseDuration(float duration)
    {
        timer.IncreaseTimerDuration(duration);
    }
}

public enum OnEffectDuplication
{
    Skip,
    IncreaseDuration,
    IncreaseEffect
}