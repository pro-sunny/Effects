using ActionGameFramework.Health;
using Core.Utilities;
using Core.Utilities.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Level;
using TowerDefense.Towers;
using UnityEngine;

public abstract class EffectActivator : MonoBehaviour
{
    public EffectVisualizer visualizer; // changeTargetMaterial, PlayParticles, AddEffectIcon       

    public ActiveEffect activeEffect;
    public Dictionary<Modifier, ParameterModiferValue> effectValues;

    protected List<Targetable> targets;

    protected void SetupActivator(List<Targetable> targets)
    {
        this.targets = targets;
        // If this is parent activator, it can have ampty vizualizer
        visualizer?.Initialize(activeEffect);
    }

    public virtual void Initialize(List<Targetable> targets)
    {
        SetupActivator(targets);

        ActivateEffect();
    }

    public virtual void Initialize(Dictionary<Modifier, ParameterModiferValue> effectValue, List<Targetable> targets)
    {
        SetupActivator(targets);

        this.effectValues = effectValue;        
        ActivateEffect();
    }

    public virtual void Initialize(ActiveEffect effect, List<Targetable> targets)
    {
        SetupActivator(targets);

        this.activeEffect = effect;
        this.effectValues = effect.parameterValues;
        ActivateEffect();
    }

    public abstract void ActivateEffect();

    public void ApplyEffect(Targetable target)
    {
        Dictionary<Modifier, ParameterModiferValue> effectParameters = GetEffectValue(activeEffect, target);

        // Debug.Log("Effect applied: " + activeEffect + " ------ target = " + target);

        foreach (KeyValuePair<Modifier, ParameterModiferValue> parameter in effectParameters)
        {
            if (parameter.Key == Modifier.Damage)
            {
                target.TakeDamage(parameter.Value.value, target.transform.position, LevelManager.instance.playerAlignment.GetInterface());
            }
            else
            {
                ActiveUnit activeUnit = target as ActiveUnit;
                ParameterModifier param = new ParameterModifier(parameter.Value.value, parameter.Value.modType);
                activeUnit.parameters.UpdateParameter(parameter.Key, param, this.gameObject);
            }
        }
    }

    public void DeactivateEffect(Targetable target)
    {
        ActiveUnit activeUnit = target as ActiveUnit;
        activeUnit.parameters.ClearParameters(this.gameObject);
    }

    public void ApplyVisualization(Targetable target)
    {
        // PoolManager.instance.GetPoolable(effect.visualization);
        // TODO: this should work from pool NO IT SHOULDN"T !!!
        if (visualizer == null)
        {
            return;
        }
        if (visualizer.gameObject.TryGetComponent(out Poolable p))
        {
            var poolable = Poolable.TryGetPoolable<EffectVisualizer>(visualizer.gameObject);
            if (poolable != null)
            {
                poolable.transform.position = target.transform.position;
                poolable.Visualize();
            }
        } else
        {
            // visualizer.transform.position = target.transform.position;
            visualizer?.Visualize();
        }
    }

    private Dictionary<Modifier, ParameterModiferValue> GetEffectValue(ActiveEffect effect, Targetable target)
    {
        Dictionary<Modifier, ParameterModiferValue> finalValues = new Dictionary<Modifier, ParameterModiferValue>();
        Tower tower = target as Tower;
        
        foreach (KeyValuePair<Modifier, ParameterModiferValue> paramValue in effect.parameterValues)
        {
            ParameterModiferValue paramMod = paramValue.Value;
            float value = effect.parameterValues[paramValue.Key].value;
            if (tower)
            {
                if (effect.rankUpParameterValues.ContainsKey(paramValue.Key))
                {
                    float rank = tower.towerInfo.currentRank;
                    value += effect.rankUpParameterValues[paramValue.Key].value * rank;
                }
            }

            finalValues.Add(paramValue.Key, new ParameterModiferValue(paramMod.modType, value));
        }

        return finalValues;
    }
}