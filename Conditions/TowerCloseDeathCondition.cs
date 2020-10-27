using ActionGameFramework.Health;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Affectors;
using TowerDefense.Targetting;
using TowerDefense.Towers;
using UnityEngine;

public class TowerCloseDeathCondition : TowerEffectCondition
{
    public override bool CheckCondition()
    {
        Targetter targetter = null;
        List<Targetable> targets = new List<Targetable>();
        Tower tower = activeUnit as Tower;

        foreach (Affector affector in tower.currentTowerLevel.Affectors)
        {
            var attack = affector as AttackAffector;
            if (attack != null && attack.damagerProjectile != null)
            {
                targets = attack.targetter.GetAllTargets();
                targetter = attack.targetter;
            }
        }

        if (targetter)
        {
            targetter.targetEntersRange += Targetter_targetEntersRange;
            targetter.targetExitsRange += Targetter_targetExitsRange;
        }

        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].died += TowerCloseDeathCondition_died;
        }

        return false;
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
        throw new System.NotImplementedException();
    }
}
