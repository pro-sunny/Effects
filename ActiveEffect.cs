using ActionGameFramework.Health;
using Core.Utilities.Parameters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TowerDefense.Affectors;
using TowerDefense.Level;
using TowerDefense.Targetting;
using TowerDefense.Towers;
using UnityEngine;

[Serializable]
public struct ParameterModiferValue
{    
    public ParameterModType modType;
    public float value;

    public ParameterModiferValue(ParameterModType valueModType, float value)
    {
        this.modType = valueModType;
        this.value = value;
    }
}

[CreateAssetMenu(menuName = "TowerDefense/Active Effect")]
public class ActiveEffect : SerializedScriptableObject
{
    public string effectName;
    public string UID;

    [TextArea(5, 8)]
    public string description;

    public TargetType targetType;

    public EffectActivator effectActivator;

    public Dictionary<Modifier, ParameterModiferValue> parameterValues;

    public Dictionary<Modifier, ParameterModiferValue> rankUpParameterValues;
}


public enum TargetType
{
    Self,
    SingleTarget,
    MultipleTargets,
    AllTargets,
    LinkedTowers,    
    SingleEnemy,
    MultipleEnemiesInRange,
    AllEnemiesInRange
}

