using UnityEngine;

public abstract class TowerEffectCondition : ScriptableObject
{
    public ActiveUnit activeUnit;

    public abstract bool CheckCondition();
}
