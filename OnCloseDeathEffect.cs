using ActionGameFramework.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCloseDeathEffect : ActiveUnitEffect
{
    public override void SetupEffect()
    {
        activeUnit.targetter.targetEntersRange += Targetter_targetEntersRange;
        activeUnit.targetter.targetExitsRange += Targetter_targetExitsRange;
    }

    private void Targetter_targetEntersRange(Targetable obj)
    {
        obj.died += TowerCloseDeathCondition_died;
    }
    private void Targetter_targetExitsRange(Targetable obj)
    {
        obj.died -= TowerCloseDeathCondition_died;
    }

    private void TowerCloseDeathCondition_died(Core.Health.DamageableBehaviour obj)
    {
        ActivateEffect(obj as Targetable);
    }
}