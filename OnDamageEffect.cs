using ActionGameFramework.Health;
using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Affectors;
using UnityEngine;

public class OnDamageEffect : ActiveUnitEffect
{
    public override void SetupEffect()
    {
        AttackAffector attack = activeUnit.projectileHandler as AttackAffector;
        if (attack && attack.damagerProjectile is Damager)
        {
            attack.HasDamaged += Projectile_HasDamagedTarget;            
        }
    }

    private void Projectile_HasDamagedTarget(Targetable target)
    {
        if (!target.isDead)
        {
            ActivateEffect(target);
        }        
    }
}
