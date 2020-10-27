public class TowerEffectModifierCondition : TowerEffectCondition
{
    public ComparisonType comparisonType;
    public Modifier modifier;
    public float value;

    public override bool CheckCondition()
    {
        float valueOfTarget = activeUnit.parameters.GetParameterValue(modifier);

        switch (comparisonType)
        {
            case ComparisonType.Equals: return valueOfTarget == value;
            case ComparisonType.Less: return valueOfTarget < value;
            case ComparisonType.LessOrEqual: return valueOfTarget <= value;
            case ComparisonType.More: return valueOfTarget > value;
            case ComparisonType.MoreOrEqual: return valueOfTarget >= value;
        }

        return false;
    }
}

public enum ComparisonType
{
    Equals,
    Less,
    LessOrEqual,
    More,
    MoreOrEqual
}