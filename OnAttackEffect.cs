using ActionGameFramework.Health;
using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Affectors;
using UnityEngine;

public class OnAttackEffect : ActiveUnitEffect
{
    public override void SetupEffect()
    {
        AttackAffector attack = activeUnit.projectileHandler as AttackAffector;
        if (attack)
        {
            attack.BeforeFireProjectile += Attacker_BeforeFireProjectile;
            attack.AfterFireProjectile += Attacker_AfterFireProjectile;
        }
    }

    private void Attacker_AfterFireProjectile(Targetable target)
    {
        DeactivateEffect(target);
    }

    private void Attacker_BeforeFireProjectile(Targetable target)
    {
        ActivateEffect(target);
    }
}