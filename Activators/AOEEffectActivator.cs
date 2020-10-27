using ActionGameFramework.Health;
using Core.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEEffectActivator : EffectActivator
{
    public LayerMask enemyMask = -1;

    public EffectActivator effectActivator;

    private static readonly Collider[] s_Enemies = new Collider[64];

    private Dictionary<Targetable, List<Targetable>> newTargets = new Dictionary<Targetable, List<Targetable>>();

    public override void Initialize(List<Targetable> targets)
    {
        newTargets = new Dictionary<Targetable, List<Targetable>>();

        ParameterModiferValue rangeValue = activeEffect.parameterValues[Modifier.Range];
        float attackRange = rangeValue.value;

        foreach (Targetable target in targets)
        {
            int number = Physics.OverlapSphereNonAlloc(target.transform.position, attackRange, s_Enemies, enemyMask);

            newTargets.Add(target, new List<Targetable>());
            for (int index = 0; index < number; index++)
            {
                Collider enemy = s_Enemies[index];
                var damageable = enemy.GetComponent<Targetable>();
                if (damageable != null)
                {
                    newTargets[target].Add(damageable);
                }
            }            
        }
        ActivateEffect();
    }

    public override void ActivateEffect()
    {
        foreach (var target in newTargets)
        {
            ApplyVisualization(target.Key);
            effectActivator.Initialize(target.Value);
        }
    }
}