using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModifierSequencesCondition : TowerEffectCondition
{
    public Modifier modifier;
    public int sequenceLength;
    public int occurrencesCount;

    public override bool CheckCondition()
    {
        float valueOfTarget = activeUnit.parameters.GetParameterValue(modifier);

        if (Mathf.Approximately(valueOfTarget, 1))
        {
            occurrencesCount++;
        }
        if (occurrencesCount == sequenceLength)
        {
            occurrencesCount = 0;
            return true;
        }
        return false;
    }
}
