using ActionGameFramework.Health;
using System;
using System.Collections.Generic;
using TowerDefense.Towers;
using UnityEngine;

public abstract class ActiveUnitEffect : MonoBehaviour
{
    public List<ActiveEffect> effects;

    public List<TowerEffectCondition> activationConditions;

    public ActiveUnit activeUnit;

    protected event Action OnConditionsMet;

    protected Dictionary<ActiveEffect, EffectActivator> activatedEffects = new Dictionary<ActiveEffect, EffectActivator>();

    private void Start()
    {
        SetupEffect();
    }

    protected List<Targetable> GetTargets(ActiveEffect effect, TargetType targetType)
    {
        List<Targetable> targets = new List<Targetable>();

        if (targetType == TargetType.Self)
        {
            return new List<Targetable> { activeUnit };
        }
        if (targetType == TargetType.LinkedTowers)
        {
            Tower tower = activeUnit as Tower;
            if (tower != null)
            {
                List<Tower> connectedTowers = tower.connectedTowers;
                foreach (Tower connectedTower in connectedTowers)
                {
                    targets.Add(connectedTower);
                }
            }
        }

        if (targetType == TargetType.SingleEnemy)
        {
            return new List<Targetable> { activeUnit.targetter.GetTarget() };
        }
        if (targetType == TargetType.AllTargets)
        {
            return activeUnit.targetter.GetAllTargets();
        }
        if (targetType == TargetType.MultipleTargets)
        {
            ParameterModiferValue multishotValue = effect.parameterValues[Modifier.MultishotTargets];            
            return activeUnit.targetter.GetTargets(Mathf.RoundToInt(multishotValue.value));
        }

        return targets;
    }

    public abstract void SetupEffect();

    private bool CheckConditions()
    {
        foreach (ActiveEffect activeEffect in effects)
        {
            ParameterModiferValue triggerValue = activeEffect.parameterValues[Modifier.TriggerChance];
            int randomValue = UnityEngine.Random.Range(0, 100);
            if (triggerValue.value < randomValue)
            {
                return false;
            }
        }

        foreach (TowerEffectCondition condition in activationConditions)
        {
            if (!condition.CheckCondition())
            {
                return false;
            }
        }

        return true;
    }

    protected void ActivateEffect(Targetable target)
    {
        foreach (ActiveEffect activeEffect in effects)
        {
            if (!CheckConditions())
            {
                continue;
            }

            List<Targetable> targets;
            if (activeEffect.targetType == TargetType.SingleTarget)
            {
                targets = new List<Targetable> { target };
            } else
            {
                targets = GetTargets(activeEffect, activeEffect.targetType);
            }

            if (activatedEffects.ContainsKey(activeEffect))
            {
                activatedEffects[activeEffect].Initialize(targets);
            }
            else
            {
                EffectActivator effectActivator = Instantiate(activeEffect.effectActivator, transform);
                effectActivator.Initialize(targets);

                activatedEffects.Add(activeEffect, effectActivator);
            }
        }
    }

    public void DeactivateEffect(Targetable target)
    {
        foreach (KeyValuePair<ActiveEffect, EffectActivator> effect in activatedEffects)
        {
            List<Targetable> targets;
            if (effect.Key.targetType == TargetType.SingleTarget)
            {
                targets = new List<Targetable> { target };
            }
            else
            {
                targets = GetTargets(effect.Key, effect.Key.targetType);
            }

            foreach (var t in targets)
            {
                effect.Value.DeactivateEffect(t);
            }
        }        
    }
}