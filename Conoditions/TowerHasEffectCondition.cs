using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHasEffectCondition : TowerEffectCondition
{
    public ActiveEffect effect;

    public override bool CheckCondition()
    {
        if (activeUnit.TryGetComponent(out EffectActivator appliedEffect))
        {
            return appliedEffect.activeEffect.UID == effect.UID;
        }
        return false;
    }
}
